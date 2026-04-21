import { useDeferredValue, useEffect, useMemo, useState } from "react"
import { createDiscipline, deleteDiscipline, searchDisciplines, updateDiscipline } from "../../api/disciplinesApi"
import { useToast } from "../../components/common/ToastCenter"
import { PageHeader } from "../../components/common/PageHeader"
import { PaginationControls } from "../../components/common/PaginationControls"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { DisciplineSearchItemDto, EntityId, PagedResult } from "../../types/api"

const DEFAULT_PAGE_SIZE = 20

export function AdminDisciplinesPage() {
  const { pushToast } = useToast()
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(DEFAULT_PAGE_SIZE)
  const [nameFilter, setNameFilter] = useState("")
  const deferredNameFilter = useDeferredValue(nameFilter)
  const [data, setData] = useState<PagedResult<DisciplineSearchItemDto> | null>(null)
  const [selectedId, setSelectedId] = useState<EntityId | null>(null)
  const [newName, setNewName] = useState("")
  const [editName, setEditName] = useState("")
  const [isLoading, setIsLoading] = useState(true)
  const [isSaving, setIsSaving] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const loadItems = async () => {
    const result = await searchDisciplines({
      name: deferredNameFilter.trim() || undefined,
      page,
      pageSize,
    })

    setData(result)
    setSelectedId((current) => {
      if (current && result.items.some((item) => item.disciplineId === current)) {
        return current
      }

      return result.items[0]?.disciplineId ?? null
    })
  }

  useEffect(() => {
    let isActive = true
    setIsLoading(true)
    setError(null)

    loadItems()
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити предмети.")
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
  }, [page, pageSize, deferredNameFilter])

  useEffect(() => {
    setPage(1)
  }, [deferredNameFilter])

  const items = useMemo(() => data?.items ?? [], [data])
  const hasLoadedData = data !== null
  const isInitialLoading = isLoading && !hasLoadedData
  const isRefreshing = isLoading && hasLoadedData
  const selectedDiscipline = useMemo(
    () => items.find((item) => item.disciplineId === selectedId) ?? null,
    [items, selectedId],
  )

  useEffect(() => {
    if (selectedDiscipline) {
      setEditName(selectedDiscipline.disciplineName)
    } else {
      setEditName("")
    }
  }, [selectedDiscipline])

  const runAction = async (action: () => Promise<void>, successMessage: string) => {
    setIsSaving(true)
    setError(null)

    try {
      await action()
      await loadItems()
      pushToast({ tone: "info", title: "Успішно", message: successMessage })
    } catch (err: unknown) {
      const nextError = err instanceof Error ? err.message : "Не вдалося виконати дію з предметом."
      setError(nextError)
      pushToast({ tone: "error", message: nextError })
    } finally {
      setIsSaving(false)
    }
  }

  return (
    <div className="page-stack">
      <PageHeader title="Предмети" description="Пошук і керування переліком дисциплін." />

      <section className="panel">
        <h2>Новий предмет</h2>
        <div className="filters-row">
          <label>
            Назва
            <input type="text" value={newName} onChange={(event) => setNewName(event.target.value)} />
          </label>
          <button
            type="button"
            disabled={isSaving || newName.trim().length === 0}
            onClick={() =>
              void runAction(
                () => createDiscipline({ disciplineName: newName.trim() }).then(() => setNewName("")),
                "Предмет створено.",
              )
            }
          >
            {isSaving ? "Збереження..." : "Створити"}
          </button>
        </div>
      </section>

      <section className="panel">
        <h2>Пошук предметів</h2>
        <div className="filters-row">
          <label>
            Назва
            <input
              type="text"
              value={nameFilter}
              onChange={(event) => setNameFilter(event.target.value)}
              placeholder="Пошук за назвою"
            />
          </label>
        </div>
      </section>

      {isInitialLoading ? <Spinner label="Завантаження предметів..." /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}

      {!isInitialLoading && !error ? (
        <div className="content-grid content-grid--two-columns">
          <section className="panel">
            <div className="section-heading">
              <h2>Список предметів</h2>
              {isRefreshing ? <span className="loading-inline">Оновлення...</span> : null}
            </div>

            {items.length === 0 ? (
              <StatusState tone="info" message="Предметів не знайдено." />
            ) : (
              <>
                <div className="table-wrap">
                  <table>
                    <thead>
                      <tr>
                        <th>Назва</th>
                        <th>Використань у планах</th>
                      </tr>
                    </thead>
                    <tbody>
                      {items.map((item) => (
                        <tr
                          key={item.disciplineId}
                          className={selectedId === item.disciplineId ? "row-selected" : ""}
                          onClick={() => setSelectedId(item.disciplineId)}
                        >
                          <td>{item.disciplineName}</td>
                          <td>{item.planUsageCount}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>

                <PaginationControls
                  page={page}
                  pageSize={pageSize}
                  totalCount={data?.totalCount ?? 0}
                  onPageChange={setPage}
                  onPageSizeChange={(nextPageSize) => {
                    setPageSize(nextPageSize)
                    setPage(1)
                  }}
                />
              </>
            )}
          </section>

          <section className="panel">
            <h2>Деталі предмета</h2>
            {selectedDiscipline ? (
              <>
                <div className="summary-grid summary-grid--compact">
                  <div>
                    <strong>Використань у планах:</strong> {selectedDiscipline.planUsageCount}
                  </div>
                </div>

                <div className="form-grid">
                  <label>
                    Назва
                    <input type="text" value={editName} onChange={(event) => setEditName(event.target.value)} />
                  </label>
                </div>

                <div className="inline-actions">
                  <button
                    type="button"
                    disabled={isSaving || editName.trim().length === 0}
                    onClick={() =>
                      void runAction(
                        () =>
                          updateDiscipline(selectedDiscipline.disciplineId, {
                            disciplineName: editName.trim(),
                          }).then(() => undefined),
                        "Предмет оновлено.",
                      )
                    }
                  >
                    {isSaving ? "Оновлення..." : "Оновити"}
                  </button>
                  <button
                    type="button"
                    disabled={isSaving}
                    onClick={() =>
                      void runAction(
                        () =>
                          deleteDiscipline(selectedDiscipline.disciplineId).then(() => {
                            setSelectedId(null)
                            setEditName("")
                          }),
                        "Предмет видалено.",
                      )
                    }
                  >
                    {isSaving ? "Видалення..." : "Видалити"}
                  </button>
                </div>
              </>
            ) : (
              <StatusState tone="info" message="Оберіть предмет у списку." />
            )}
          </section>
        </div>
      ) : null}
    </div>
  )
}
