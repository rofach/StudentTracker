import { useEffect, useState } from "react"
import { getGroupSubgroups } from "../../api/groupsApi"
import { getInstitutions } from "../../api/institutionsApi"
import { createStudent, createTransferredStudent, getSelectableGroups } from "../../api/studentsApi"
import { useToast } from "../../components/common/ToastCenter"
import { PageHeader } from "../../components/common/PageHeader"
import { StatusState } from "../../components/common/StatusState"
import type {
  ActiveGroupDto,
  CreateTransferredStudentDto,
  EntityId,
  InstitutionDto,
  StudentAccountPasswordDto,
  StudentCreateDto,
  StudentCreatedResultDto,
  SubgroupDto,
} from "../../types/api"

type AdminStudentCreatePageProps = {
  navigate: (path: string) => void
}

type SubgroupOption = {
  subgroupId: EntityId
  subgroupName: string
}

const emptyForm: StudentCreateDto = {
  firstName: "",
  lastName: "",
  patronymic: null,
  birthDate: null,
  email: null,
  phone: null,
}

const emptyTransferredForm: CreateTransferredStudentDto = {
  ...emptyForm,
  institutionId: "",
  groupId: "",
  subgroupId: null,
  dateFrom: "",
  notes: null,
}

function mapSubgroups(items: SubgroupDto[]): SubgroupOption[] {
  return items
    .map((item) => ({ subgroupId: item.subgroupId, subgroupName: item.subgroupName }))
    .sort((left, right) => left.subgroupName.localeCompare(right.subgroupName))
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
  const [isTransferredIn, setIsTransferredIn] = useState(false)
  const [transferredForm, setTransferredForm] = useState<CreateTransferredStudentDto>(emptyTransferredForm)
  const [institutions, setInstitutions] = useState<InstitutionDto[]>([])
  const [groups, setGroups] = useState<ActiveGroupDto[]>([])
  const [subgroupOptions, setSubgroupOptions] = useState<SubgroupOption[]>([])
  const [createdResult, setCreatedResult] = useState<StudentCreatedResultDto | null>(null)
  const [error, setError] = useState<string | null>(null)
  const [isSaving, setIsSaving] = useState(false)
  const [isLoadingReferences, setIsLoadingReferences] = useState(true)

  useEffect(() => {
    let isActive = true

    Promise.all([getInstitutions(), getSelectableGroups()])
      .then(([institutionsResult, groupsResult]) => {
        if (!isActive) {
          return
        }

        setInstitutions(institutionsResult)
        setGroups(groupsResult)
        setTransferredForm((prev) => ({
          ...prev,
          institutionId: prev.institutionId || institutionsResult[0]?.institutionId || "",
          groupId: prev.groupId || groupsResult[0]?.groupId || "",
        }))
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити довідкові дані.")
      })
      .finally(() => {
        if (!isActive) {
          return
        }

        setIsLoadingReferences(false)
      })

    return () => {
      isActive = false
    }
  }, [])

  useEffect(() => {
    if (!isTransferredIn || !transferredForm.groupId) {
      setSubgroupOptions([])
      setTransferredForm((prev) => ({ ...prev, subgroupId: null }))
      return
    }

    let isActive = true

    getGroupSubgroups(transferredForm.groupId)
      .then((result) => {
        if (!isActive) {
          return
        }

        const options = mapSubgroups(result)
        setSubgroupOptions(options)
        setTransferredForm((prev) => ({
          ...prev,
          subgroupId:
            prev.subgroupId && options.some((option) => option.subgroupId === prev.subgroupId)
              ? prev.subgroupId
              : null,
        }))
      })
      .catch(() => {
        if (!isActive) {
          return
        }

        setSubgroupOptions([])
        setTransferredForm((prev) => ({ ...prev, subgroupId: null }))
      })

    return () => {
      isActive = false
    }
  }, [isTransferredIn, transferredForm.groupId])

  const handleCreate = async () => {
    setIsSaving(true)
    setError(null)

    try {
      const created = isTransferredIn
        ? await createTransferredStudent({ ...transferredForm, ...form })
        : await createStudent(form)

      setCreatedResult(created)
      setForm(emptyForm)
      setTransferredForm(emptyTransferredForm)
      setIsTransferredIn(false)
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

        <div className="form-grid">
          <label className="checkbox-field">
            <input
              type="checkbox"
              checked={isTransferredIn}
              onChange={(event) => setIsTransferredIn(event.target.checked)}
            />
            Створити студента як переведеного з іншого університету
          </label>
        </div>

        {isTransferredIn ? (
          <div className="form-grid">
            <label>
              Заклад, з якого переведено
              <select
                value={transferredForm.institutionId}
                onChange={(event) =>
                  setTransferredForm((prev) => ({ ...prev, institutionId: event.target.value }))
                }
              >
                <option value="">Оберіть заклад</option>
                {institutions.map((institution) => (
                  <option key={institution.institutionId} value={institution.institutionId}>
                    {institution.institutionName}
                  </option>
                ))}
              </select>
            </label>
            <label>
              Група для зарахування
              <select
                value={transferredForm.groupId}
                onChange={(event) =>
                  setTransferredForm((prev) => ({
                    ...prev,
                    groupId: event.target.value,
                    subgroupId: null,
                  }))
                }
              >
                <option value="">Оберіть групу</option>
                {groups.map((group) => (
                  <option key={group.groupId} value={group.groupId}>
                    {group.groupCode} ({group.departmentName})
                  </option>
                ))}
              </select>
            </label>
            <label>
              Підгрупа
              <select
                value={transferredForm.subgroupId ?? ""}
                onChange={(event) =>
                  setTransferredForm((prev) => ({ ...prev, subgroupId: event.target.value || null }))
                }
              >
                <option value="">Без підгрупи</option>
                {subgroupOptions.map((subgroup) => (
                  <option key={subgroup.subgroupId} value={subgroup.subgroupId}>
                    {subgroup.subgroupName}
                  </option>
                ))}
              </select>
            </label>
            <label>
              Дата зарахування
              <input
                type="date"
                value={transferredForm.dateFrom}
                onChange={(event) =>
                  setTransferredForm((prev) => ({ ...prev, dateFrom: event.target.value }))
                }
              />
            </label>
            <label>
              Примітка
              <input
                type="text"
                value={transferredForm.notes ?? ""}
                onChange={(event) =>
                  setTransferredForm((prev) => ({ ...prev, notes: event.target.value || null }))
                }
              />
            </label>
          </div>
        ) : null}

        <div className="inline-actions">
          <button type="button" onClick={handleCreate} disabled={isSaving || isLoadingReferences}>
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
