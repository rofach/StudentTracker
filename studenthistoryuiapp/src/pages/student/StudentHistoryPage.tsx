import { useEffect, useMemo, useState } from "react"
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

const curatedEventLabels: Record<string, string> = {
  EnrollmentStart: "Зарахування",
  EnrollmentEnd: "Завершення зарахування",
  AcademicLeaveStart: "Початок академвідпустки",
  AcademicLeaveEnd: "Завершення академвідпустки",
  SubgroupChange: "Зміна підгрупи",
  GroupTransfer: "Переведення між групами",
  ExternalTransfer: "Зовнішнє переведення",
}

function getEventLabel(eventType: string): string {
  return curatedEventLabels[eventType] ?? eventType
}

function getCuratedDescription(item: TimelineEventDto): string {
  switch (item.eventType) {
    case "EnrollmentStart":
      return item.groupCode ? `Зараховано до групи ${item.groupCode}` : "Початок зарахування"
    case "AcademicLeaveStart":
      return "Початок академвідпустки"
    case "AcademicLeaveEnd":
      return item.description.includes(":")
        ? item.description.replace("Academic leave ended", "Повернення з академвідпустки")
        : "Повернення з академвідпустки"
    case "SubgroupChange":
      return item.description.replace("Subgroup changed to", "Переведено до підгрупи")
    case "GroupTransfer": {
      const [baseDescription] = item.description.split(". Academic difference:")
      return baseDescription.replace("Transferred from group", "Переведено з групи").replace(" to ", " до ")
    }
    case "ExternalTransfer":
      return item.description
    default:
      return item.description
  }
}

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
        if (!isActive) {
          return
        }

        setData(result)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити історію.")
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
  }, [studentId, page, pageSize])

  const visibleItems = useMemo(() => {
    if (!data) {
      return []
    }

    return data.items.filter((item) => item.eventType !== "EnrollmentEnd")
  }, [data])

  const hasLoadedData = data !== null
  const isInitialLoading = isLoading && !hasLoadedData
  const isRefreshing = isLoading && hasLoadedData

  return (
    <section className="panel">
      <div className="section-heading">
        <div>
          <h2>Історія студента</h2>
          <p className="note-text">Показано основні події.</p>
        </div>
        {isRefreshing ? <span className="loading-inline">Оновлення...</span> : null}
      </div>

      {isInitialLoading ? <Spinner label="Завантаження історії..." /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}
      {!isLoading && !error && (!data || visibleItems.length === 0) ? (
        <StatusState tone="info" message="Історичні події відсутні." />
      ) : null}

      {visibleItems.length > 0 ? (
        <>
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Подія</th>
                  <th>Опис</th>
                  <th>Дата від</th>
                  <th>Дата до</th>
                  <th>Група</th>
                  <th>Кафедра</th>
                  <th>Підрозділ</th>
                </tr>
              </thead>
              <tbody>
                {visibleItems.map((item, index) => (
                  <tr key={`${item.eventType}-${item.dateFrom}-${item.description}-${index}`}>
                    <td>{getEventLabel(item.eventType)}</td>
                    <td>{getCuratedDescription(item)}</td>
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
            totalCount={data?.totalCount ?? 0}
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
