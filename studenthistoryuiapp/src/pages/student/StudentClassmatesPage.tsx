import { useEffect, useMemo, useState } from "react"
import { getStudentById, getStudentClassmates } from "../../api/studentsApi"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { ClassmateDto, EntityId, StudentDto } from "../../types/api"
import { formatDate, formatNullable } from "../../utils/format"
import { formatStudentStatus } from "../../utils/status"

type StudentClassmatesPageProps = {
  studentId: EntityId
}

type FilterMode = "current" | "date" | "period"

export function StudentClassmatesPage({ studentId }: StudentClassmatesPageProps) {
  const [mode, setMode] = useState<FilterMode>("current")
  const [singleDate, setSingleDate] = useState("")
  const [dateFrom, setDateFrom] = useState("")
  const [dateTo, setDateTo] = useState("")

  const [items, setItems] = useState<ClassmateDto[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const [hasLoaded, setHasLoaded] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const [selectedClassmateId, setSelectedClassmateId] = useState<EntityId | null>(null)
  const [selectedClassmate, setSelectedClassmate] = useState<StudentDto | null>(null)
  const [isClassmateLoading, setIsClassmateLoading] = useState(false)

  const params = useMemo(() => {
    if (mode === "current") {
      return { dateFrom: undefined, dateTo: undefined }
    }

    if (mode === "date") {
      return { dateFrom: singleDate || undefined, dateTo: singleDate || undefined }
    }

    return {
      dateFrom: dateFrom || undefined,
      dateTo: dateTo || undefined,
    }
  }, [mode, singleDate, dateFrom, dateTo])

  useEffect(() => {
    let isActive = true
    setIsLoading(true)
    setError(null)

    getStudentClassmates(studentId, params.dateFrom, params.dateTo)
      .then((result) => {
        if (!isActive) {
          return
        }

        setItems(result)
        setHasLoaded(true)
      })
      .catch((err: unknown) => {
        if (!isActive) {
          return
        }

        setError(err instanceof Error ? err.message : "Не вдалося завантажити одногрупників.")
        setHasLoaded(true)
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
  }, [studentId, params.dateFrom, params.dateTo])

  useEffect(() => {
    if (!selectedClassmateId) {
      setSelectedClassmate(null)
      return
    }

    let isActive = true
    setIsClassmateLoading(true)

    getStudentById(selectedClassmateId)
      .then((result) => {
        if (!isActive) {
          return
        }

        setSelectedClassmate(result)
      })
      .catch(() => {
        if (!isActive) {
          return
        }

        setSelectedClassmate(null)
      })
      .finally(() => {
        if (!isActive) {
          return
        }

        setIsClassmateLoading(false)
      })

    return () => {
      isActive = false
    }
  }, [selectedClassmateId])

  const isInitialLoading = isLoading && !hasLoaded
  const isRefreshing = isLoading && hasLoaded

  return (
    <div className="page-stack">
      <section className="panel">
        <h2>Фільтр одногрупників</h2>
        <div className="filters-row">
          <label>
            Режим
            <select value={mode} onChange={(event) => setMode(event.target.value as FilterMode)}>
              <option value="current">Поточний момент</option>
              <option value="date">На дату</option>
              <option value="period">За період</option>
            </select>
          </label>

          {mode === "date" ? (
            <label>
              Дата
              <input type="date" value={singleDate} onChange={(event) => setSingleDate(event.target.value)} />
            </label>
          ) : null}

          {mode === "period" ? (
            <>
              <label>
                Від
                <input type="date" value={dateFrom} onChange={(event) => setDateFrom(event.target.value)} />
              </label>
              <label>
                До
                <input type="date" value={dateTo} onChange={(event) => setDateTo(event.target.value)} />
              </label>
            </>
          ) : null}
        </div>
      </section>

      <section className="panel">
        <div className="section-heading">
          <h2>Одногрупники</h2>
          {isRefreshing ? <span className="loading-inline">Оновлення...</span> : null}
        </div>

        {isInitialLoading ? <Spinner label="Завантаження одногрупників..." /> : null}
        {error ? <StatusState tone="error" message={error} /> : null}
        {!isLoading && !error && items.length === 0 ? (
          <StatusState tone="info" message="Для вибраного інтервалу одногрупників не знайдено." />
        ) : null}

        {items.length > 0 ? (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>ПІБ</th>
                  <th>Група</th>
                  <th>Підгрупа</th>
                  <th>Спільно з</th>
                  <th>Спільно до</th>
                  <th>Деталі</th>
                </tr>
              </thead>
              <tbody>
                {items.map((item) => (
                  <tr key={`${item.classmateStudentId}-${item.groupId}-${item.sharedFrom}`}>
                    <td>
                      {item.lastName} {item.firstName}
                    </td>
                    <td>{item.groupCode}</td>
                    <td>{item.subgroupName ?? "—"}</td>
                    <td>{formatDate(item.sharedFrom)}</td>
                    <td>{formatDate(item.sharedTo)}</td>
                    <td>
                      <button type="button" onClick={() => setSelectedClassmateId(item.classmateStudentId)}>
                        Переглянути
                      </button>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        ) : null}
      </section>

      {selectedClassmateId ? (
        <section className="panel">
          <h2>Дані одногрупника</h2>
          {isClassmateLoading ? <Spinner label="Завантаження картки студента..." /> : null}
          {!isClassmateLoading && selectedClassmate ? (
            <div className="summary-grid summary-grid--compact">
              <div>
                <strong>ПІБ:</strong> {selectedClassmate.lastName} {selectedClassmate.firstName}
              </div>
              <div>
                <strong>Статус:</strong> {formatStudentStatus(selectedClassmate.status)}
              </div>
              <div>
                <strong>Email:</strong> {formatNullable(selectedClassmate.email)}
              </div>
              <div>
                <strong>Телефон:</strong> {formatNullable(selectedClassmate.phone)}
              </div>
            </div>
          ) : null}
          {!isClassmateLoading && !selectedClassmate ? (
            <StatusState tone="info" message="Не вдалося завантажити дані одногрупника." />
          ) : null}
          <p className="note-text">Підгрупа вказана для періоду, коли ви навчалися разом.</p>
        </section>
      ) : null}
    </div>
  )
}
