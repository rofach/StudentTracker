import { useEffect, useState } from "react"
import { getStudentTimeline } from "../../api/studentsApi"
import { PaginationControls } from "../../components/common/PaginationControls"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { EntityId, PagedResult, TimelineEventDto } from "../../types/api"
import { formatDate } from "../../utils/format"

type StudentHistoryPageProps = {
  studentId: EntityId
}

const DEFAULT_PAGE_SIZE = 20

export function StudentHistoryPage({ studentId }: StudentHistoryPageProps) {
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(DEFAULT_PAGE_SIZE)
  const [data, setData] = useState<PagedResult<TimelineEventDto> | null>(null)
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    let isActive = true
    setIsLoading(true)
    setError(null)

    getStudentTimeline(studentId, page, pageSize)
      .then((result) => {
        if (!isActive) return
        setData(result)
      })
      .catch((err: unknown) => {
        if (!isActive) return
        setError(err instanceof Error ? err.message : "Не вдалося завантажити історію.")
      })
      .finally(() => {
        if (!isActive) return
        setIsLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [studentId, page, pageSize])

  return (
    <section className="panel">
      <h2>Історія студента</h2>
      {isLoading ? <Spinner label="Завантаження історії..." /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}
      {!isLoading && !error && (!data || data.items.length === 0) ? (
        <StatusState tone="info" message="Історичні події відсутні." />
      ) : null}

      {!isLoading && !error && data && data.items.length > 0 ? (
        <>
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Тип події</th>
                  <th>Опис</th>
                  <th>Дата від</th>
                  <th>Дата до</th>
                  <th>Група</th>
                  <th>Кафедра</th>
                  <th>Підрозділ</th>
                </tr>
              </thead>
              <tbody>
                {data.items.map((item, index) => (
                  <tr key={`${item.eventType}-${item.dateFrom}-${index}`}>
                    <td>{item.eventType}</td>
                    <td>{item.description}</td>
                    <td>{formatDate(item.dateFrom)}</td>
                    <td>{formatDate(item.dateTo)}</td>
                    <td>{item.groupCode ?? "—"}</td>
                    <td>{item.departmentName ?? "—"}</td>
                    <td>{item.academicUnitName ?? "—"}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          <PaginationControls
            page={page}
            pageSize={pageSize}
            totalCount={data.totalCount}
            onPageChange={setPage}
            onPageSizeChange={(nextPageSize) => {
              setPageSize(nextPageSize)
              setPage(1)
            }}
          />
        </>
      ) : null}
    </section>
  )
}
