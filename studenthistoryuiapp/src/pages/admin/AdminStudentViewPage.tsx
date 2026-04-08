import { useEffect, useState } from "react"
import { getStudentDetails } from "../../api/studentsApi"
import { PageHeader } from "../../components/common/PageHeader"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import { StudentClassmatesPage } from "../student/StudentClassmatesPage"
import { StudentHistoryPage } from "../student/StudentHistoryPage"
import { StudentOverviewPage } from "../student/StudentOverviewPage"
import { StudentSubjectsPage } from "../student/StudentSubjectsPage"
import type { StudentDetailDto } from "../../types/api"
import { formatStudentStatus } from "../../utils/status"

type StudentSection = "overview" | "subjects" | "classmates" | "history"

type AdminStudentViewPageProps = {
  studentId: number
  navigate: (path: string) => void
}

const sectionLabels: Record<StudentSection, string> = {
  overview: "Огляд",
  subjects: "Предмети",
  classmates: "Одногрупники",
  history: "Історія",
}

export function AdminStudentViewPage({ studentId, navigate }: AdminStudentViewPageProps) {
  const [selectedSection, setSelectedSection] = useState<StudentSection>("overview")
  const [student, setStudent] = useState<StudentDetailDto | null>(null)
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  useEffect(() => {
    let isActive = true
    setIsLoading(true)
    setError(null)

    getStudentDetails(studentId)
      .then((result) => {
        if (!isActive) {
          return
        }

        setStudent(result)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити дані студента.")
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
  }, [studentId])

  if (isLoading) {
    return <Spinner label="Завантаження картки студента..." />
  }

  if (error) {
    return <StatusState tone="error" message={error} />
  }

  if (!student) {
    return <StatusState tone="info" message="Дані студента відсутні." />
  }

  const currentEnrollment = student.enrollments.find((item) => item.dateTo === null) ?? student.enrollments[0] ?? null

  return (
    <div className="page-stack">
      <PageHeader
        title={`${student.lastName} ${student.firstName}`}
        description="Перегляд повної картки студента."
        actions={
          <div className="inline-actions">
            <button type="button" onClick={() => navigate("/admin/students")}>
              Назад до списку
            </button>
            <button type="button" onClick={() => navigate(`/admin/students/${studentId}/edit`)}>
              Редагувати
            </button>
            <button type="button" onClick={() => navigate(`/admin/students/${studentId}/operations`)}>
              Операції
            </button>
          </div>
        }
      />

      <section className="panel">
        <div className="summary-grid">
          <div>
            <strong>Статус:</strong> {formatStudentStatus(student.status)}
          </div>
          <div>
            <strong>Email:</strong> {student.email ?? "—"}
          </div>
          <div>
            <strong>Група:</strong> {currentEnrollment?.groupCode ?? "—"}
          </div>
          <div>
            <strong>Кафедра:</strong> {currentEnrollment?.departmentName ?? "—"}
          </div>
        </div>
      </section>

      <section className="panel">
        <div className="filters-row">
          {(Object.keys(sectionLabels) as StudentSection[]).map((section) => (
            <button
              key={section}
              type="button"
              className={selectedSection === section ? "quick-link quick-link--active" : "quick-link"}
              onClick={() => setSelectedSection(section)}
            >
              {sectionLabels[section]}
            </button>
          ))}
        </div>
      </section>

      <section className="panel panel--inner">
        {selectedSection === "overview" ? <StudentOverviewPage studentId={studentId} /> : null}
        {selectedSection === "subjects" ? <StudentSubjectsPage studentId={studentId} /> : null}
        {selectedSection === "classmates" ? <StudentClassmatesPage studentId={studentId} /> : null}
        {selectedSection === "history" ? <StudentHistoryPage studentId={studentId} /> : null}
      </section>
    </div>
  )
}
