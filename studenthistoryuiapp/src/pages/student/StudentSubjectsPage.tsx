import "./StudentSubjectsPage.css"
import { useEffect, useMemo, useState } from "react"
import {
  getStudentAverageGrade,
  getStudentDisciplines,
  getStudentGrades,
  upsertStudentGrade,
} from "../../api/studentsApi"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import { useToast } from "../../components/common/ToastCenter"
import { useDebouncedValue } from "../../hooks/useDebouncedValue"
import type { AverageGradeDto, EntityId, GradeDto, StudentDisciplineOptionDto } from "../../types/api"
import { formatDate } from "../../utils/format"

type StudentSubjectsPageProps = {
  studentId: EntityId
  editable?: boolean
}

type SemesterOption = {
  key: string
  semesterNo: number
  academicYearStart: number
  academicYearLabel: string
}

type GradeDraft = {
  gradeValue: string
  assessmentDate: string
}

function buildSemesterKey(semesterNo: number, academicYearStart: number): string {
  return `${semesterNo}:${academicYearStart}`
}

function todayValue(): string {
  return new Date().toISOString().slice(0, 10)
}

export function StudentSubjectsPage({ studentId, editable = false }: StudentSubjectsPageProps) {
  const { pushToast } = useToast()
  const [disciplines, setDisciplines] = useState<StudentDisciplineOptionDto[]>([])
  const [grades, setGrades] = useState<GradeDto[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  const [semesterFilter, setSemesterFilter] = useState<string>("all")
  const [searchText, setSearchText] = useState("")
  const debouncedSearchText = useDebouncedValue(searchText, 250)

  const [average, setAverage] = useState<AverageGradeDto | null>(null)
  const [isAverageLoading, setIsAverageLoading] = useState(false)
  const [averageMode, setAverageMode] = useState<"all" | "semester">("all")
  const [drafts, setDrafts] = useState<Record<string, GradeDraft>>({})
  const [savingCourseEnrollmentId, setSavingCourseEnrollmentId] = useState<EntityId | null>(null)

  useEffect(() => {
    let isActive = true

    setIsLoading(true)
    setError(null)

    Promise.all([getStudentDisciplines(studentId), getStudentGrades(studentId, 1, 200)])
      .then(([disciplineRows, gradesPage]) => {
        if (!isActive) {
          return
        }

        setDisciplines(disciplineRows)
        setGrades(gradesPage.items)
      })
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
  }, [studentId])

  const semesterOptions = useMemo(() => {
    const map = new Map<string, SemesterOption>()

    for (const row of disciplines) {
      const key = buildSemesterKey(row.semesterNo, row.academicYearStart)
      if (!map.has(key)) {
        map.set(key, {
          key,
          semesterNo: row.semesterNo,
          academicYearStart: row.academicYearStart,
          academicYearLabel: row.academicYearLabel,
        })
      }
    }

    return Array.from(map.values()).sort((left, right) => {
      if (left.academicYearStart !== right.academicYearStart) {
        return left.academicYearStart - right.academicYearStart
      }

      return left.semesterNo - right.semesterNo
    })
  }, [disciplines])

  const selectedSemesterOption =
    semesterFilter === "all" ? null : semesterOptions.find((option) => option.key === semesterFilter) ?? null

  const gradeMap = useMemo(() => {
    const map = new Map<string, GradeDto>()
    for (const row of grades) {
      map.set(row.courseEnrollmentId, row)
    }
    return map
  }, [grades])

  useEffect(() => {
    setDrafts((current) => {
      const next = { ...current }

      for (const row of disciplines) {
        const grade = gradeMap.get(row.courseEnrollmentId)
        if (!next[row.courseEnrollmentId]) {
          next[row.courseEnrollmentId] = {
            gradeValue: grade?.gradeValue ?? "",
            assessmentDate: grade?.assessmentDate ?? todayValue(),
          }
        }
      }

      return next
    })
  }, [disciplines, gradeMap])

  const filteredRows = useMemo(() => {
    const normalizedSearch = debouncedSearchText.trim().toLowerCase()

    return disciplines.filter((row) => {
      const semesterMatches =
        semesterFilter === "all" || buildSemesterKey(row.semesterNo, row.academicYearStart) === semesterFilter
      const searchMatches =
        normalizedSearch.length === 0 || row.disciplineName.toLowerCase().includes(normalizedSearch)

      return semesterMatches && searchMatches
    })
  }, [disciplines, semesterFilter, debouncedSearchText])

  const averagePeriodLabel = useMemo(() => {
    if (!average) {
      return null
    }

    if (averageMode === "all") {
      return "Увесь період"
    }

    if (!selectedSemesterOption) {
      return "Вибраний семестр"
    }

    return `Семестр ${selectedSemesterOption.semesterNo} (${selectedSemesterOption.academicYearLabel})`
  }, [average, averageMode, selectedSemesterOption])

  const handleCalculateAverage = async () => {
    setIsAverageLoading(true)
    setError(null)

    try {
      const result =
        averageMode === "semester" && selectedSemesterOption
          ? await getStudentAverageGrade(studentId, {
              semesterNo: selectedSemesterOption.semesterNo,
              academicYearStart: selectedSemesterOption.academicYearStart,
            })
          : await getStudentAverageGrade(studentId, {})

      setAverage(result)
      pushToast({ tone: "info", title: "Готово", message: "Середній бал оновлено." })
    } catch (err: unknown) {
      const nextError = err instanceof Error ? err.message : "Не вдалося обчислити середній бал."
      setError(nextError)
      pushToast({ tone: "error", message: nextError })
    } finally {
      setIsAverageLoading(false)
    }
  }

  const saveGrade = async (courseEnrollmentId: EntityId) => {
    const draft = drafts[courseEnrollmentId]
    if (!draft || draft.gradeValue.trim().length === 0) {
      const nextError = "Вкажіть значення оцінки."
      setError(nextError)
      pushToast({ tone: "error", message: nextError })
      return
    }

    setSavingCourseEnrollmentId(courseEnrollmentId)
    setError(null)

    try {
      const updated = await upsertStudentGrade(studentId, courseEnrollmentId, {
        gradeValue: draft.gradeValue.trim(),
        assessmentDate: draft.assessmentDate,
      })

      setGrades((current) => {
        const existingIndex = current.findIndex((item) => item.courseEnrollmentId === courseEnrollmentId)
        if (existingIndex === -1) {
          return [...current, updated]
        }

        return current.map((item) => (item.courseEnrollmentId === courseEnrollmentId ? updated : item))
      })

      setDisciplines((current) =>
        current.map((item) =>
          item.courseEnrollmentId === courseEnrollmentId ? { ...item, hasGrade: true } : item,
        ),
      )

      pushToast({ tone: "info", title: "Успішно", message: "Оцінку збережено." })
    } catch (err: unknown) {
      const nextError = err instanceof Error ? err.message : "Не вдалося зберегти оцінку."
      setError(nextError)
      pushToast({ tone: "error", message: nextError })
    } finally {
      setSavingCourseEnrollmentId(null)
    }
  }

  if (isLoading) {
    return <Spinner label="Завантаження предметів..." />
  }

  return (
    <div className="page-stack">
      {error ? <StatusState tone="error" message={error} /> : null}

      <section className="panel">
        <h2>Фільтри предметів</h2>
        <div className="filters-row">
          <label>
            Семестр
            <select value={semesterFilter} onChange={(event) => setSemesterFilter(event.target.value)}>
              <option value="all">Усі семестри</option>
              {semesterOptions.map((option) => (
                <option key={option.key} value={option.key}>
                  Семестр {option.semesterNo} ({option.academicYearLabel})
                </option>
              ))}
            </select>
          </label>

          <label>
            Пошук предмету
            <input
              type="text"
              value={searchText}
              onChange={(event) => setSearchText(event.target.value)}
              placeholder="Введіть назву предмету"
            />
          </label>
        </div>
      </section>

      <section className="panel">
        <h2>Середній бал</h2>
        <div className="filters-row">
          <label>
            Режим
            <select value={averageMode} onChange={(event) => setAverageMode(event.target.value as "all" | "semester")}>
              <option value="all">За весь період</option>
              <option value="semester">За вибраний семестр</option>
            </select>
          </label>

          <button
            type="button"
            onClick={handleCalculateAverage}
            disabled={isAverageLoading || (averageMode === "semester" && !selectedSemesterOption)}
          >
            {isAverageLoading ? "Обчислення..." : "Порахувати середній бал"}
          </button>
        </div>

        {average ? (
          <div className="summary-grid summary-grid--compact">
            <div>
              <strong>Середній бал:</strong> {average.average ?? "—"}
            </div>
            <div>
              <strong>Кількість оцінок:</strong> {average.gradeCount}
            </div>
            <div>
              <strong>Період:</strong> {averagePeriodLabel}
            </div>
          </div>
        ) : null}
      </section>

      <section className="panel">
        <h2>{editable ? "Предмети та оцінювання" : "Предмети студента"}</h2>
        {filteredRows.length === 0 ? (
          <StatusState tone="info" message="За обраними фільтрами предмети не знайдено." />
        ) : (
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Стан</th>
                  <th>Назва предмету</th>
                  <th>Семестр</th>
                  <th>Навчальний рік</th>
                  <th>Оцінка</th>
                  <th>Дата оцінювання</th>
                  {editable ? <th>Дія</th> : null}
                </tr>
              </thead>
              <tbody>
                {filteredRows.map((row) => {
                  const grade = gradeMap.get(row.courseEnrollmentId)
                  const draft = drafts[row.courseEnrollmentId] ?? {
                    gradeValue: grade?.gradeValue ?? "",
                    assessmentDate: grade?.assessmentDate ?? todayValue(),
                  }

                  return (
                    <tr key={row.courseEnrollmentId}>
                      <td>
                        <span
                          className={`grade-dot ${row.hasGrade ? "grade-dot--done" : ""}`}
                          title={row.hasGrade ? "Є оцінка" : "Без оцінки"}
                        />
                      </td>
                      <td>{row.disciplineName}</td>
                      <td>{row.semesterNo}</td>
                      <td>{row.academicYearLabel}</td>
                      <td>
                        {editable ? (
                          <input
                            type="text"
                            value={draft.gradeValue}
                            onChange={(event) =>
                              setDrafts((current) => ({
                                ...current,
                                [row.courseEnrollmentId]: {
                                  ...draft,
                                  gradeValue: event.target.value,
                                },
                              }))
                            }
                          />
                        ) : (
                          grade?.gradeValue ?? "—"
                        )}
                      </td>
                      <td>
                        {editable ? (
                          <input
                            type="date"
                            value={draft.assessmentDate}
                            onChange={(event) =>
                              setDrafts((current) => ({
                                ...current,
                                [row.courseEnrollmentId]: {
                                  ...draft,
                                  assessmentDate: event.target.value,
                                },
                              }))
                            }
                          />
                        ) : grade ? (
                          formatDate(grade.assessmentDate)
                        ) : (
                          "—"
                        )}
                      </td>
                      {editable ? (
                        <td>
                          <button
                            type="button"
                            disabled={savingCourseEnrollmentId === row.courseEnrollmentId}
                            onClick={() => void saveGrade(row.courseEnrollmentId)}
                          >
                            {savingCourseEnrollmentId === row.courseEnrollmentId ? "Збереження..." : "Зберегти"}
                          </button>
                        </td>
                      ) : null}
                    </tr>
                  )
                })}
              </tbody>
            </table>
          </div>
        )}
      </section>
    </div>
  )
}
