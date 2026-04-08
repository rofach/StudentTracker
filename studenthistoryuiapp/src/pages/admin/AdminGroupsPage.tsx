import { useEffect, useMemo, useState } from "react"
import { getActiveGroups, getGroupComposition } from "../../api/groupsApi"
import { PageHeader } from "../../components/common/PageHeader"
import { PaginationControls } from "../../components/common/PaginationControls"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { ActiveGroupDto, GroupCompositionMemberDto, PagedResult } from "../../types/api"
import { formatDate } from "../../utils/format"

export function AdminGroupsPage() {
  const [date, setDate] = useState("")
  const [groups, setGroups] = useState<ActiveGroupDto[]>([])
  const [selectedGroupId, setSelectedGroupId] = useState<number | null>(null)
  const [compositionPage, setCompositionPage] = useState(1)
  const [compositionPageSize, setCompositionPageSize] = useState(20)
  const [composition, setComposition] = useState<PagedResult<GroupCompositionMemberDto> | null>(null)
  const [isGroupsLoading, setIsGroupsLoading] = useState(true)
  const [isCompositionLoading, setIsCompositionLoading] = useState(false)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    let isActive = true
    setIsGroupsLoading(true)
    setError(null)

    getActiveGroups(date || undefined)
      .then((result) => {
        if (!isActive) {
          return
        }

        setGroups(result)
        if (result.length > 0 && !selectedGroupId) {
          setSelectedGroupId(result[0].groupId)
        }
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити групи.")
      })
      .finally(() => {
        if (!isActive) {
          return
        }

        setIsGroupsLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [date, selectedGroupId])

  useEffect(() => {
    if (!selectedGroupId) {
      setComposition(null)
      return
    }

    let isActive = true
    setIsCompositionLoading(true)

    getGroupComposition(selectedGroupId, {
      date: date || undefined,
      page: compositionPage,
      pageSize: compositionPageSize,
    })
      .then((result) => {
        if (!isActive) {
          return
        }

        setComposition(result)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити склад групи.")
      })
      .finally(() => {
        if (!isActive) {
          return
        }

        setIsCompositionLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [selectedGroupId, date, compositionPage, compositionPageSize])

  const selectedGroup = useMemo(
    () => groups.find((group) => group.groupId === selectedGroupId) ?? null,
    [groups, selectedGroupId],
  )

  return (
    <div className="page-stack">
      <PageHeader
        title="Групи"
        description="Перегляд активних груп і їх складу на вибрану дату."
      />

      <section className="panel">
        <div className="filters-row">
          <label>
            Дата
            <input
              type="date"
              value={date}
              onChange={(event) => {
                setDate(event.target.value)
                setCompositionPage(1)
              }}
            />
          </label>
        </div>

      </section>

      {isGroupsLoading ? <Spinner label="Завантаження груп..." /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}

      {!isGroupsLoading && !error ? (
        <div className="content-grid content-grid--two-columns">
          <section className="panel">
            <h2>Активні групи</h2>
            {groups.length === 0 ? (
              <StatusState tone="info" message="Активні групи не знайдено." />
            ) : (
              <div className="table-wrap">
                <table>
                  <thead>
                    <tr>
                      <th>Код</th>
                      <th>Кафедра</th>
                      <th>Підрозділ</th>
                    </tr>
                  </thead>
                  <tbody>
                    {groups.map((group) => (
                      <tr
                        key={group.groupId}
                        className={selectedGroupId === group.groupId ? "row-selected" : ""}
                        onClick={() => setSelectedGroupId(group.groupId)}
                      >
                        <td>{group.groupCode}</td>
                        <td>{group.departmentName}</td>
                        <td>{group.academicUnitName}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </section>

          <section className="panel">
            <h2>Склад групи</h2>
            {selectedGroup ? (
              <>
                <div className="summary-grid summary-grid--compact">
                  <div>
                    <strong>Група:</strong> {selectedGroup.groupCode}
                  </div>
                  <div>
                    <strong>Кафедра:</strong> {selectedGroup.departmentName}
                  </div>
                  <div>
                    <strong>Підрозділ:</strong> {selectedGroup.academicUnitName}
                  </div>
                  <div>
                    <strong>Створено:</strong> {formatDate(selectedGroup.dateCreated)}
                  </div>
                </div>

                {isCompositionLoading ? <Spinner label="Завантаження складу..." /> : null}
                {!isCompositionLoading && composition && composition.items.length === 0 ? (
                  <StatusState tone="info" message="Для обраної групи склад не знайдено." />
                ) : null}

                {!isCompositionLoading && composition && composition.items.length > 0 ? (
                  <>
                    <div className="table-wrap">
                      <table>
                        <thead>
                          <tr>
                            <th>ПІБ</th>
                            <th>Email</th>
                            <th>Підгрупа</th>
                            <th>З</th>
                            <th>До</th>
                          </tr>
                        </thead>
                        <tbody>
                          {composition.items.map((member) => (
                            <tr key={`${member.studentId}-${member.dateFrom}`}>
                              <td>
                                {member.lastName} {member.firstName}
                              </td>
                              <td>{member.email ?? "—"}</td>
                              <td>{member.subgroupName ?? "—"}</td>
                              <td>{formatDate(member.dateFrom)}</td>
                              <td>{formatDate(member.dateTo)}</td>
                            </tr>
                          ))}
                        </tbody>
                      </table>
                    </div>

                    <PaginationControls
                      page={composition.page}
                      pageSize={composition.pageSize}
                      totalCount={composition.totalCount}
                      onPageChange={setCompositionPage}
                      onPageSizeChange={(nextPageSize) => {
                        setCompositionPageSize(nextPageSize)
                        setCompositionPage(1)
                      }}
                    />
                  </>
                ) : null}
              </>
            ) : (
              <StatusState tone="info" message="Оберіть групу у списку ліворуч." />
            )}
          </section>
        </div>
      ) : null}
    </div>
  )
}
