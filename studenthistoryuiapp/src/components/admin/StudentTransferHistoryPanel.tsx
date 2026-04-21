import { useEffect, useMemo, useState } from "react"
import { getStudentGroupTransferDetail, updateDifferenceItem } from "../../api/studentsApi"
import { Spinner } from "../common/Spinner"
import { StatusState } from "../common/StatusState"
import type {
  DifferenceItemStatus,
  EntityId,
  StudentGroupTransferDetailDto,
  StudentInternalTransferSummaryDto,
} from "../../types/api"
import { formatDate } from "../../utils/format"

type StudentTransferHistoryPanelProps = {
  studentId: EntityId
  transfers: StudentInternalTransferSummaryDto[]
}

type DifferenceDraft = {
  status: DifferenceItemStatus
  notes: string
}

const differenceStatuses: DifferenceItemStatus[] = ["Pending", "Completed", "Waived"]

const differenceStatusLabels: Record<DifferenceItemStatus, string> = {
  Pending: "Очікує",
  Completed: "Завершено",
  Waived: "Зараховано без виконання",
}

export function StudentTransferHistoryPanel({ studentId, transfers }: StudentTransferHistoryPanelProps) {
  const [summaries, setSummaries] = useState<StudentInternalTransferSummaryDto[]>(transfers)
  const [selectedTransferId, setSelectedTransferId] = useState<EntityId | null>(null)
  const [detail, setDetail] = useState<StudentGroupTransferDetailDto | null>(null)
  const [drafts, setDrafts] = useState<Record<string, DifferenceDraft>>({})
  const [isLoading, setIsLoading] = useState(false)
  const [isSavingItemId, setIsSavingItemId] = useState<EntityId | null>(null)
  const [error, setError] = useState<string | null>(null)
  const [message, setMessage] = useState<string | null>(null)

  useEffect(() => {
    setSummaries(transfers)
  }, [transfers])

  useEffect(() => {
    setSelectedTransferId((current) => {
      if (summaries.length === 0) {
        return null
      }

      if (current && summaries.some((transfer) => transfer.transferId === current)) {
        return current
      }

      return summaries[0].transferId
    })
  }, [summaries])

  useEffect(() => {
    if (!selectedTransferId) {
      setDetail(null)
      setDrafts({})
      return
    }

    let isActive = true
    setIsLoading(true)
    setError(null)
    setMessage(null)

    getStudentGroupTransferDetail(studentId, selectedTransferId)
      .then((result) => {
        if (!isActive) {
          return
        }

        setDetail(result)
        setDrafts(
          Object.fromEntries(
            result.differenceItems.map((item) => [
              item.differenceItemId,
              {
                status: item.status,
                notes: item.notes ?? "",
              },
            ]),
          ),
        )
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setDetail(null)
        setError(err instanceof Error ? err.message : "Не вдалося завантажити деталі переведення.")
      })
      .finally(() => {
        if (!isActive) {
          return
        }

        setIsLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [studentId, selectedTransferId])

  const selectedTransfer = useMemo(
    () => summaries.find((transfer) => transfer.transferId === selectedTransferId) ?? null,
    [summaries, selectedTransferId],
  )

  async function saveDifferenceItem(itemId: EntityId) {
    if (!detail) {
      return
    }

    const draft = drafts[itemId]
    if (!draft) {
      return
    }

    setIsSavingItemId(itemId)
    setError(null)
    setMessage(null)

    try {
      const previousItem = detail.differenceItems.find((item) => item.differenceItemId === itemId) ?? null
      const updatedItem = await updateDifferenceItem(studentId, detail.transferId, itemId, {
        status: draft.status,
        notes: draft.notes.trim() || null,
      })

      setDetail({
        ...detail,
        differenceItems: detail.differenceItems.map((item) =>
          item.differenceItemId === itemId ? updatedItem : item,
        ),
      })

      if (previousItem) {
        setSummaries((current) =>
          current.map((transfer) => {
            if (transfer.transferId !== detail.transferId) {
              return transfer
            }

            const pendingDelta = Number(updatedItem.status === "Pending") - Number(previousItem.status === "Pending")
            const completedDelta =
              Number(updatedItem.status === "Completed") - Number(previousItem.status === "Completed")
            const waivedDelta = Number(updatedItem.status === "Waived") - Number(previousItem.status === "Waived")

            return {
              ...transfer,
              differenceItemsPending: transfer.differenceItemsPending + pendingDelta,
              differenceItemsCompleted: transfer.differenceItemsCompleted + completedDelta,
              differenceItemsWaived: transfer.differenceItemsWaived + waivedDelta,
            }
          }),
        )
      }

      setMessage("Елемент академрізниці оновлено.")
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : "Не вдалося оновити елемент академрізниці.")
    } finally {
      setIsSavingItemId(null)
    }
  }

  if (summaries.length === 0) {
    return <StatusState tone="info" message="Внутрішніх переведень ще не було." />
  }

  return (
    <div className="page-stack">
      <section className="panel panel--inner">
        <h3>Історія переведень</h3>
        <div className="table-wrap table-wrap--compact">
          <table>
            <thead>
              <tr>
                <th>Дата</th>
                <th>З</th>
                <th>До</th>
                <th>Причина</th>
                <th>Академрізниця</th>
                <th>Деталі</th>
              </tr>
            </thead>
            <tbody>
              {summaries.map((transfer) => (
                <tr key={transfer.transferId} className={transfer.transferId === selectedTransferId ? "row-selected" : ""}>
                  <td>{formatDate(transfer.transferDate)}</td>
                  <td>{transfer.oldGroupCode}</td>
                  <td>{transfer.newGroupCode}</td>
                  <td>{transfer.reason}</td>
                  <td>
                    {transfer.differenceItemsPending}/{transfer.differenceItemsTotal} очікує
                  </td>
                  <td>
                    <button type="button" onClick={() => setSelectedTransferId(transfer.transferId)}>
                      Переглянути
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </section>

      <section className="panel panel--inner">
        <h3>Деталі переведення</h3>
        {selectedTransfer ? (
          <div className="summary-grid summary-grid--compact">
            <div>
              <strong>Дата:</strong> {formatDate(selectedTransfer.transferDate)}
            </div>
            <div>
              <strong>Маршрут:</strong> {selectedTransfer.oldGroupCode} -&gt; {selectedTransfer.newGroupCode}
            </div>
            <div>
              <strong>Причина:</strong> {selectedTransfer.reason}
            </div>
            <div>
              <strong>Академрізниця:</strong> {selectedTransfer.differenceItemsPending}/{selectedTransfer.differenceItemsTotal} очікує
            </div>
          </div>
        ) : null}

        {isLoading ? <Spinner label="Завантаження деталей переведення..." /> : null}
        {error ? <StatusState tone="error" message={error} /> : null}
        {message ? <StatusState tone="info" message={message} /> : null}

        {!isLoading && !error && detail && detail.differenceItems.length === 0 ? (
          <StatusState tone="info" message="Для цього переведення немає елементів академрізниці." />
        ) : null}

        {!isLoading && !error && detail && detail.differenceItems.length > 0 ? (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Предмет</th>
                  <th>Семестр</th>
                  <th>Статус</th>
                  <th>Нотатки</th>
                  <th>Зберегти</th>
                </tr>
              </thead>
              <tbody>
                {detail.differenceItems.map((item) => {
                  const draft = drafts[item.differenceItemId]

                  return (
                    <tr key={item.differenceItemId}>
                      <td>{item.disciplineName}</td>
                      <td>{item.semesterNo}</td>
                      <td>
                        <select
                          value={draft?.status ?? item.status}
                          onChange={(event) =>
                            setDrafts((current) => ({
                              ...current,
                              [item.differenceItemId]: {
                                status: event.target.value as DifferenceItemStatus,
                                notes: current[item.differenceItemId]?.notes ?? item.notes ?? "",
                              },
                            }))
                          }
                        >
                          {differenceStatuses.map((status) => (
                            <option key={status} value={status}>
                              {differenceStatusLabels[status]}
                            </option>
                          ))}
                        </select>
                      </td>
                      <td>
                        <textarea
                          rows={2}
                          value={draft?.notes ?? item.notes ?? ""}
                          onChange={(event) =>
                            setDrafts((current) => ({
                              ...current,
                              [item.differenceItemId]: {
                                status: current[item.differenceItemId]?.status ?? item.status,
                                notes: event.target.value,
                              },
                            }))
                          }
                        />
                      </td>
                      <td>
                        <button
                          type="button"
                          disabled={isSavingItemId === item.differenceItemId}
                          onClick={() => void saveDifferenceItem(item.differenceItemId)}
                        >
                          {isSavingItemId === item.differenceItemId ? "Збереження..." : "Зберегти"}
                        </button>
                      </td>
                    </tr>
                  )
                })}
              </tbody>
            </table>
          </div>
        ) : null}
      </section>
    </div>
  )
}
