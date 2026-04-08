import { useDeferredValue, useEffect, useMemo, useState } from "react"
import { getStudents } from "../../api/studentsApi"
import { PageHeader } from "../../components/common/PageHeader"
import { PaginationControls } from "../../components/common/PaginationControls"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { PagedResult, StudentDto } from "../../types/api"
import { formatDate } from "../../utils/format"
import { formatStudentStatus } from "../../utils/status"

type AdminStudentsPageProps = {
  navigate: (path: string) => void
}

export function AdminStudentsPage({ navigate }: AdminStudentsPageProps) {
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(20)
  const [searchText, setSearchText] = useState("")
  const [statusFilter, setStatusFilter] = useState("all")
  const deferredSearchText = useDeferredValue(searchText)

  const [data, setData] = useState<PagedResult<StudentDto> | null>(null)
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    let isActive = true

    setIsLoading(true)
    setError(null)

    getStudents(page, pageSize)
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

        setError(err instanceof Error ? err.message : "Не вдалося завантажити список студентів.")
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
  }, [page, pageSize])

  const filteredStudents = useMemo(() => {
    if (!data) {
      return []
    }

    const normalizedSearch = deferredSearchText.trim().toLowerCase()

    return data.items.filter((student) => {
      const fullName = `${student.lastName} ${student.firstName}`.toLowerCase()
      const matchesSearch = normalizedSearch.length === 0 || fullName.includes(normalizedSearch)
      const matchesStatus = statusFilter === "all" || student.status === statusFilter
      return matchesSearch && matchesStatus
    })
  }, [data, deferredSearchText, statusFilter])

  return (
    <div className="page-stack">
      <PageHeader
        title="Студенти"
        description="Керування студентами"
        actions={
          <button type="button" onClick={() => navigate("/admin/students/new")}>
            Створити студента
          </button>
        }
      />

      <section className="panel">
        <h2>Пошук і фільтри</h2>
        <div className="filters-row">
          <label>
            Пошук студента
            <input
              type="text"
              value={searchText}
              onChange={(event) => setSearchText(event.target.value)}
              placeholder="Прізвище або ім'я"
            />
          </label>

          <label>
            Статус
            <select value={statusFilter} onChange={(event) => setStatusFilter(event.target.value)}>
              <option value="all">Усі</option>
              <option value="Active">Активний</option>
              <option value="OnLeave">Академвідпустка</option>
              <option value="Expelled">Відрахований</option>
              <option value="Graduated">Випускник</option>
            </select>
          </label>
        </div>

      </section>

      <section className="panel">
        <h2>Список студентів</h2>
        {isLoading ? <Spinner label="Завантаження списку студентів..." /> : null}
        {error ? <StatusState tone="error" message={error} /> : null}
        {!isLoading && !error && filteredStudents.length === 0 ? (
          <StatusState tone="info" message="Студентів за заданими умовами не знайдено." />
        ) : null}

        {!isLoading && !error && filteredStudents.length > 0 ? (
          <>
            <div className="table-wrap">
              <table>
                <thead>
                  <tr>
                    <th>ПІБ</th>
                    <th>Статус</th>
                    <th>Email</th>
                    <th>Дата народження</th>
                    <th>Дії</th>
                  </tr>
                </thead>
                <tbody>
                  {filteredStudents.map((student) => (
                    <tr key={student.studentId}>
                      <td>
                        {student.lastName} {student.firstName}
                      </td>
                      <td>{formatStudentStatus(student.status)}</td>
                      <td>{student.email ?? "—"}</td>
                      <td>{formatDate(student.birthDate)}</td>
                      <td>
                        <div className="inline-actions">
                          <button type="button" onClick={() => navigate(`/admin/students/${student.studentId}`)}>
                            Відкрити
                          </button>
                          <button type="button" onClick={() => navigate(`/admin/students/${student.studentId}/edit`)}>
                            Редагувати
                          </button>
                          <button
                            type="button"
                            onClick={() => navigate(`/admin/students/${student.studentId}/operations`)}
                          >
                            Операції
                          </button>
                        </div>
                      </td>
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
    </div>
  )
}
