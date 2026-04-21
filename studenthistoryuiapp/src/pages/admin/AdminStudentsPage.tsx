import { useEffect, useMemo, useState } from "react"
import { getStudents, searchStudents } from "../../api/studentsApi"
import { PageHeader } from "../../components/common/PageHeader"
import { PaginationControls } from "../../components/common/PaginationControls"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import { useDebouncedValue } from "../../hooks/useDebouncedValue"
import type { PagedResult, StudentDto } from "../../types/api"
import { formatDate, fullName } from "../../utils/format"
import { formatStudentStatus } from "../../utils/status"

type AdminStudentsPageProps = {
  navigate: (path: string) => void
}

export function AdminStudentsPage({ navigate }: AdminStudentsPageProps) {
  const [page, setPage] = useState(1)
  const [pageSize, setPageSize] = useState(20)
  const [fullNameFilter, setFullNameFilter] = useState("")
  const [emailFilter, setEmailFilter] = useState("")
  const [statusFilter, setStatusFilter] = useState("all")
  const debouncedFullNameFilter = useDebouncedValue(fullNameFilter)
  const debouncedEmailFilter = useDebouncedValue(emailFilter)

  const [data, setData] = useState<PagedResult<StudentDto> | null>(null)
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    const normalizedFullName = debouncedFullNameFilter.trim()
    const normalizedEmail = debouncedEmailFilter.trim()
    const hasSearch = normalizedFullName.length > 0 || normalizedEmail.length > 0 || statusFilter !== "all"

    let isActive = true

    setIsLoading(true)
    setError(null)

    const request = hasSearch
      ? searchStudents({
          fullName: normalizedFullName || undefined,
          email: normalizedEmail || undefined,
          status: statusFilter === "all" ? undefined : statusFilter,
          page,
          pageSize,
        })
      : getStudents(page, pageSize)

    request
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
  }, [page, pageSize, debouncedFullNameFilter, debouncedEmailFilter, statusFilter])

  useEffect(() => {
    setPage(1)
  }, [debouncedFullNameFilter, debouncedEmailFilter, statusFilter])

  const students = useMemo(() => data?.items ?? [], [data])
  const hasLoadedData = data !== null
  const isInitialLoading = isLoading && !hasLoadedData
  const isRefreshing = isLoading && hasLoadedData

  return (
    <div className="page-stack">
      <PageHeader
        title="Студенти"
        description="Список студентів."
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
            ПІБ
            <input
              type="text"
              value={fullNameFilter}
              onChange={(event) => setFullNameFilter(event.target.value)}
              placeholder="Прізвище, ім'я, по батькові"
            />
          </label>

          <label>
            Email
            <input
              type="text"
              value={emailFilter}
              onChange={(event) => setEmailFilter(event.target.value)}
              placeholder="Пошта"
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
        <div className="section-heading">
          <h2>Список студентів</h2>
          {isRefreshing ? <span className="loading-inline">Оновлення...</span> : null}
        </div>

        {isInitialLoading ? <Spinner label="Завантаження списку студентів..." /> : null}
        {error ? <StatusState tone="error" message={error} /> : null}
        {!isLoading && !error && students.length === 0 ? (
          <StatusState tone="info" message="Студентів за заданими умовами не знайдено." />
        ) : null}

        {students.length > 0 ? (
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
                  {students.map((student) => (
                    <tr key={student.studentId}>
                      <td>{fullName(student.firstName, student.lastName, student.patronymic)}</td>
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
