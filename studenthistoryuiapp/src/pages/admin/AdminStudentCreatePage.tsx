import { useState } from "react"
import { createStudent } from "../../api/studentsApi"
import { useToast } from "../../components/common/ToastCenter"
import { PageHeader } from "../../components/common/PageHeader"
import { StatusState } from "../../components/common/StatusState"
import type { StudentAccountPasswordDto, StudentCreateDto, StudentCreatedResultDto } from "../../types/api"

type AdminStudentCreatePageProps = {
  navigate: (path: string) => void
}

const emptyForm: StudentCreateDto = {
  firstName: "",
  lastName: "",
  patronymic: null,
  birthDate: null,
  email: null,
  phone: null,
}

function AccountPasswordPanel({
  account,
  studentId,
  navigate,
}: {
  account: StudentAccountPasswordDto
  studentId: string
  navigate: (path: string) => void
}) {
  return (
    <section className="panel">
      <h2>Обліковий запис створено</h2>
      <p>Збережіть ці дані зараз. Після переходу зі сторінки пароль більше не показуватиметься.</p>

      <div className="form-grid">
        <label>
          Логін
          <input type="text" readOnly value={account.login} />
        </label>
        <label>
          Пароль
          <input type="text" readOnly value={account.password} />
        </label>
      </div>

      <div className="inline-actions">
        <button type="button" onClick={() => navigate(`/admin/students/${studentId}`)}>
          Перейти до картки студента
        </button>
      </div>
    </section>
  )
}

export function AdminStudentCreatePage({ navigate }: AdminStudentCreatePageProps) {
  const { pushToast } = useToast()
  const [form, setForm] = useState<StudentCreateDto>(emptyForm)
  const [createdResult, setCreatedResult] = useState<StudentCreatedResultDto | null>(null)
  const [error, setError] = useState<string | null>(null)
  const [isSaving, setIsSaving] = useState(false)

  const handleCreate = async () => {
    setIsSaving(true)
    setError(null)

    try {
      const created = await createStudent(form)
      setCreatedResult(created)
      setForm(emptyForm)
      pushToast({ tone: "info", title: "Успішно", message: "Студента створено, акаунт згенеровано." })
    } catch (err: unknown) {
      const nextError = err instanceof Error ? err.message : "Не вдалося створити студента."
      setError(nextError)
      pushToast({ tone: "error", message: nextError })
    } finally {
      setIsSaving(false)
    }
  }

  return (
    <div className="page-stack">
      <PageHeader
        title="Новий студент"
        description="Створення картки студента."
        actions={
          <button type="button" onClick={() => navigate("/admin/students")}>
            Назад до списку
          </button>
        }
      />

      {createdResult ? (
        <AccountPasswordPanel
          account={createdResult.account}
          studentId={createdResult.student.studentId}
          navigate={navigate}
        />
      ) : null}

      <section className="panel">
        <h2>Базові дані</h2>
        <div className="form-grid">
          <label>
            Ім'я
            <input
              type="text"
              value={form.firstName}
              onChange={(event) => setForm((prev) => ({ ...prev, firstName: event.target.value }))}
            />
          </label>
          <label>
            Прізвище
            <input
              type="text"
              value={form.lastName}
              onChange={(event) => setForm((prev) => ({ ...prev, lastName: event.target.value }))}
            />
          </label>
          <label>
            По батькові
            <input
              type="text"
              value={form.patronymic ?? ""}
              onChange={(event) => setForm((prev) => ({ ...prev, patronymic: event.target.value || null }))}
            />
          </label>
          <label>
            Дата народження
            <input
              type="date"
              value={form.birthDate ?? ""}
              onChange={(event) => setForm((prev) => ({ ...prev, birthDate: event.target.value || null }))}
            />
          </label>
          <label>
            Email
            <input
              type="email"
              value={form.email ?? ""}
              onChange={(event) => setForm((prev) => ({ ...prev, email: event.target.value || null }))}
            />
          </label>
          <label>
            Телефон
            <input
              type="text"
              value={form.phone ?? ""}
              onChange={(event) => setForm((prev) => ({ ...prev, phone: event.target.value || null }))}
            />
          </label>
        </div>

        <div className="inline-actions">
          <button type="button" onClick={handleCreate} disabled={isSaving}>
            {isSaving ? "Створення..." : "Створити студента"}
          </button>
          <button type="button" onClick={() => navigate("/admin/students")}>
            Скасувати
          </button>
        </div>
      </section>

      {error ? <StatusState tone="error" message={error} /> : null}
    </div>
  )
}
