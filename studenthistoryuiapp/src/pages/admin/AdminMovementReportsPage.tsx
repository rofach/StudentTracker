import { useDeferredValue, useEffect, useMemo, useState } from "react"
import { getActiveAcademicDifference, getInternalTransferJournal } from "../../api/reportsApi"
import { PageHeader } from "../../components/common/PageHeader"
import { PaginationControls } from "../../components/common/PaginationControls"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type {
  ActiveAcademicDifferenceDto,
  InternalTransferJournalItemDto,
  PagedResult,
} from "../../types/api"
import { formatDate, formatNullable } from "../../utils/format"

const DEFAULT_PAGE_SIZE = 20

export function AdminMovementReportsPage() {
  const [differencePage, setDifferencePage] = useState(1)
  const [differencePageSize, setDifferencePageSize] = useState(DEFAULT_PAGE_SIZE)
  const [differenceStudentFilter, setDifferenceStudentFilter] = useState("")
  const [differenceDisciplineFilter, setDifferenceDisciplineFilter] = useState("")
  const [differenceStatusFilter, setDifferenceStatusFilter] = useState("Pending")
  const [differenceDateFrom, setDifferenceDateFrom] = useState("")
  const [differenceDateTo, setDifferenceDateTo] = useState("")
  const deferredDifferenceStudent = useDeferredValue(differenceStudentFilter)
  const deferredDifferenceDiscipline = useDeferredValue(differenceDisciplineFilter)
  const [differenceData, setDifferenceData] = useState<PagedResult<ActiveAcademicDifferenceDto> | null>(null)
  const [differenceLoading, setDifferenceLoading] = useState(true)
  const [differenceError, setDifferenceError] = useState<string | null>(null)

  const [transferPage, setTransferPage] = useState(1)
  const [transferPageSize, setTransferPageSize] = useState(DEFAULT_PAGE_SIZE)
  const [transferStudentFilter, setTransferStudentFilter] = useState("")
  const [transferDateFrom, setTransferDateFrom] = useState("")
  const [transferDateTo, setTransferDateTo] = useState("")
  const [onlyWithPendingDifference, setOnlyWithPendingDifference] = useState(false)
  const deferredTransferStudent = useDeferredValue(transferStudentFilter)
  const [transferData, setTransferData] = useState<PagedResult<InternalTransferJournalItemDto> | null>(null)
  const [transferLoading, setTransferLoading] = useState(true)
  const [transferError, setTransferError] = useState<string | null>(null)

  useEffect(() => {
    let isActive = true
    setDifferenceLoading(true)
    setDifferenceError(null)

    getActiveAcademicDifference({
      studentName: deferredDifferenceStudent.trim() || undefined,
      disciplineName: deferredDifferenceDiscipline.trim() || undefined,
      status: differenceStatusFilter === "all" ? undefined : differenceStatusFilter,
      dateFrom: differenceDateFrom || undefined,
      dateTo: differenceDateTo || undefined,
      page: differencePage,
      pageSize: differencePageSize,
    })
      .then((result) => {
        if (!isActive) {
          return
        }

        setDifferenceData(result)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setDifferenceError(err instanceof Error ? err.message : "Не вдалося завантажити активну академрізницю.")
      })
      .finally(() => {
        if (!isActive) {
          return
        }

        setDifferenceLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [
    deferredDifferenceStudent,
    deferredDifferenceDiscipline,
    differenceStatusFilter,
    differenceDateFrom,
    differenceDateTo,
    differencePage,
    differencePageSize,
  ])

  useEffect(() => {
    setDifferencePage(1)
  }, [deferredDifferenceStudent, deferredDifferenceDiscipline, differenceStatusFilter, differenceDateFrom, differenceDateTo])

  useEffect(() => {
    let isActive = true
    setTransferLoading(true)
    setTransferError(null)

    getInternalTransferJournal({
      studentName: deferredTransferStudent.trim() || undefined,
      dateFrom: transferDateFrom || undefined,
      dateTo: transferDateTo || undefined,
      onlyWithPendingDifference,
      page: transferPage,
      pageSize: transferPageSize,
    })
      .then((result) => {
        if (!isActive) {
          return
        }

        setTransferData(result)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setTransferError(err instanceof Error ? err.message : "Не вдалося завантажити журнал внутрішніх переведень.")
      })
      .finally(() => {
        if (!isActive) {
          return
        }

        setTransferLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [deferredTransferStudent, transferDateFrom, transferDateTo, onlyWithPendingDifference, transferPage, transferPageSize])

  useEffect(() => {
    setTransferPage(1)
  }, [deferredTransferStudent, transferDateFrom, transferDateTo, onlyWithPendingDifference])

  const differenceItems = useMemo(() => differenceData?.items ?? [], [differenceData])
  const transferItems = useMemo(() => transferData?.items ?? [], [transferData])
  const hasDifferenceData = differenceData !== null
  const hasTransferData = transferData !== null
  const isDifferenceInitialLoading = differenceLoading && !hasDifferenceData
  const isDifferenceRefreshing = differenceLoading && hasDifferenceData
  const isTransferInitialLoading = transferLoading && !hasTransferData
  const isTransferRefreshing = transferLoading && hasTransferData

  return (
    <div className="page-stack">
      <PageHeader title="Рух студентів" description="Звіти по академрізниці та внутрішніх переведеннях." />

      <section className="panel">
        <div className="section-heading">
          <h2>Активна академрізниця</h2>
          {isDifferenceRefreshing ? <span className="loading-inline">Оновлення...</span> : null}
        </div>

        <div className="filters-row">
          <label>
            Студент
            <input
              type="text"
              value={differenceStudentFilter}
              onChange={(event) => setDifferenceStudentFilter(event.target.value)}
              placeholder="ПІБ студента"
            />
          </label>

          <label>
            Предмет
            <input
              type="text"
              value={differenceDisciplineFilter}
              onChange={(event) => setDifferenceDisciplineFilter(event.target.value)}
              placeholder="Назва дисципліни"
            />
          </label>

          <label>
            Статус
            <select value={differenceStatusFilter} onChange={(event) => setDifferenceStatusFilter(event.target.value)}>
              <option value="Pending">Pending</option>
              <option value="Completed">Completed</option>
              <option value="Waived">Waived</option>
              <option value="all">Усі</option>
            </select>
          </label>

          <label>
            Дата від
            <input type="date" value={differenceDateFrom} onChange={(event) => setDifferenceDateFrom(event.target.value)} />
          </label>

          <label>
            Дата до
            <input type="date" value={differenceDateTo} onChange={(event) => setDifferenceDateTo(event.target.value)} />
          </label>
        </div>

        {isDifferenceInitialLoading ? <Spinner label="Завантаження активної академрізниці..." /> : null}
        {differenceError ? <StatusState tone="error" message={differenceError} /> : null}
        {!differenceLoading && !differenceError && differenceItems.length === 0 ? (
          <StatusState tone="info" message="Записів не знайдено." />
        ) : null}

        {differenceItems.length > 0 ? (
          <>
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>Студент</th>
                    <th>Переведення</th>
                    <th>Предмет</th>
                    <th>Семестр</th>
                    <th>Статус</th>
                    <th>Примітка</th>
                  </tr>
                </thead>
                <tbody>
                  {differenceItems.map((item) => (
                    <tr key={item.differenceItemId}>
                      <td>{item.studentName}</td>
                      <td>
                        {item.oldGroupCode} → {item.newGroupCode}
                        <br />
                        <span className="note-text">{formatDate(item.transferDate)}</span>
                      </td>
                      <td>{item.disciplineName}</td>
                      <td>{item.semesterNo}</td>
                      <td>{item.status}</td>
                      <td>{formatNullable(item.notes)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>

            <PaginationControls
              page={differencePage}
              pageSize={differencePageSize}
              totalCount={differenceData?.totalCount ?? 0}
              onPageChange={setDifferencePage}
              onPageSizeChange={(nextPageSize) => {
                setDifferencePageSize(nextPageSize)
                setDifferencePage(1)
              }}
            />
          </>
        ) : null}
      </section>

      <section className="panel">
        <div className="section-heading">
          <h2>Журнал внутрішніх переведень</h2>
          {isTransferRefreshing ? <span className="loading-inline">Оновлення...</span> : null}
        </div>

        <div className="filters-row">
          <label>
            Студент
            <input
              type="text"
              value={transferStudentFilter}
              onChange={(event) => setTransferStudentFilter(event.target.value)}
              placeholder="ПІБ студента"
            />
          </label>

          <label>
            Дата від
            <input type="date" value={transferDateFrom} onChange={(event) => setTransferDateFrom(event.target.value)} />
          </label>

          <label>
            Дата до
            <input type="date" value={transferDateTo} onChange={(event) => setTransferDateTo(event.target.value)} />
          </label>

          <label className="checkbox-field">
            <span>Тільки з активною академрізницею</span>
            <input
              type="checkbox"
              checked={onlyWithPendingDifference}
              onChange={(event) => setOnlyWithPendingDifference(event.target.checked)}
            />
          </label>
        </div>

        {isTransferInitialLoading ? <Spinner label="Завантаження журналу переведень..." /> : null}
        {transferError ? <StatusState tone="error" message={transferError} /> : null}
        {!transferLoading && !transferError && transferItems.length === 0 ? (
          <StatusState tone="info" message="Записів не знайдено." />
        ) : null}

        {transferItems.length > 0 ? (
          <>
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>Студент</th>
                    <th>Маршрут</th>
                    <th>Дата</th>
                    <th>Причина</th>
                    <th>Академрізниця</th>
                  </tr>
                </thead>
                <tbody>
                  {transferItems.map((item) => (
                    <tr key={item.transferId}>
                      <td>{item.studentName}</td>
                      <td>
                        {item.oldGroupCode} → {item.newGroupCode}
                      </td>
                      <td>{formatDate(item.transferDate)}</td>
                      <td>{item.reason}</td>
                      <td>
                        усього: {item.differenceItemsTotal}
                        <br />
                        <span className="note-text">
                          pending: {item.differenceItemsPending}, completed: {item.differenceItemsCompleted}, waived: {item.differenceItemsWaived}
                        </span>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>

            <PaginationControls
              page={transferPage}
              pageSize={transferPageSize}
              totalCount={transferData?.totalCount ?? 0}
              onPageChange={setTransferPage}
              onPageSizeChange={(nextPageSize) => {
                setTransferPageSize(nextPageSize)
                setTransferPage(1)
              }}
            />
          </>
        ) : null}
      </section>
    </div>
  )
}
