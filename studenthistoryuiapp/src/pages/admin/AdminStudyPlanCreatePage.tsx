import { useState } from "react"
import { createStudyPlan } from "../../api/studyPlansApi"
import { PageHeader } from "../../components/common/PageHeader"
import { StatusState } from "../../components/common/StatusState"
import type { CreateStudyPlanDto } from "../../types/api"

type AdminStudyPlanCreatePageProps = {
  navigate: (path: string) => void
}

const emptyForm: CreateStudyPlanDto = {
  specialtyCode: "",
  planName: null,
  validFrom: "",
}

export function AdminStudyPlanCreatePage({ navigate }: AdminStudyPlanCreatePageProps) {
  const [form, setForm] = useState<CreateStudyPlanDto>(emptyForm)
  const [message, setMessage] = useState<string | null>(null)
  const [error, setError] = useState<string | null>(null)
  const [isSaving, setIsSaving] = useState(false)

  const handleCreate = async () => {
    setIsSaving(true)
    setMessage(null)
    setError(null)

    try {
      const created = await createStudyPlan(form)
      setMessage("Навчальний план створено.")
      navigate(`/admin/study-plans/${created.planId}`)
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : "Не вдалося створити навчальний план.")
    } finally {
      setIsSaving(false)
    }
  }

  return (
    <div className="page-stack">
      <PageHeader
        title="Новий навчальний план"
        description="Новий навчальний план."
        actions={
          <button type="button" onClick={() => navigate("/admin/study-plans")}>
            Назад до списку
          </button>
        }
      />

      <section className="panel">
        <h2>Шапка плану</h2>
        <div className="form-grid">
          <label>
            Код спеціальності
            <input
              type="text"
              value={form.specialtyCode}
              onChange={(event) => setForm((prev) => ({ ...prev, specialtyCode: event.target.value }))}
            />
          </label>
          <label>
            Назва плану
            <input
              type="text"
              value={form.planName ?? ""}
              onChange={(event) => setForm((prev) => ({ ...prev, planName: event.target.value || null }))}
            />
          </label>
          <label>
            Діє з
            <input
              type="date"
              value={form.validFrom}
              onChange={(event) => setForm((prev) => ({ ...prev, validFrom: event.target.value }))}
            />
          </label>
        </div>

        <div className="inline-actions">
          <button type="button" onClick={handleCreate} disabled={isSaving}>
            {isSaving ? "Збереження..." : "Створити план"}
          </button>
          <button type="button" onClick={() => navigate("/admin/study-plans")}>
            Скасувати
          </button>
        </div>
      </section>

      {message ? <StatusState tone="info" message={message} /> : null}
      {error ? <StatusState tone="error" message={error} /> : null}
    </div>
  )
}
