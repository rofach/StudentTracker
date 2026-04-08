import { useDeferredValue, useEffect, useMemo, useState } from "react"
import { getStudentAverageGrade, getStudentDisciplines, getStudentGrades } from "../../api/studentsApi"
import { Spinner } from "../../components/common/Spinner"
import { StatusState } from "../../components/common/StatusState"
import type { AverageGradeDto, GradeDto, StudentDisciplineOptionDto } from "../../types/api"
import { formatDate } from "../../utils/format"

type StudentSubjectsPageProps = {
  studentId: number
}

type SemesterOption = {
  key: string
  semesterNo: number
  academicYearStart: number
  academicYearLabel: string
}

function buildSemesterKey(semesterNo: number, academicYearStart: number): string {
  return `${semesterNo}:${academicYearStart}`
}

function buildGradeKey(disciplineName: string, semesterNo: number, academicYearStart: number): string {
  return `${disciplineName.trim().toLowerCase()}::${semesterNo}::${academicYearStart}`
}

export function StudentSubjectsPage({ studentId }: StudentSubjectsPageProps) {
  const [disciplines, setDisciplines] = useState<StudentDisciplineOptionDto[]>([])
  const [grades, setGrades] = useState<GradeDto[]>([])
  const [isLoading, setIsLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

  const [semesterFilter, setSemesterFilter] = useState<string>("all")
  const [searchText, setSearchText] = useState("")
  const deferredSearchText = useDeferredValue(searchText)

  const [average, setAverage] = useState<AverageGradeDto | null>(null)
  const [isAverageLoading, setIsAverageLoading] = useState(false)
  const [averageMode, setAverageMode] = useState<"all" | "semester">("all")

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
      map.set(buildGradeKey(row.disciplineName, row.semesterNo, row.academicYearStart), row)
    }
    return map
  }, [grades])

  const filteredRows = useMemo(() => {
    const normalizedSearch = deferredSearchText.trim().toLowerCase()

    return disciplines.filter((row) => {
      const semesterMatches =
        semesterFilter === "all" ||
        buildSemesterKey(row.semesterNo, row.academicYearStart) === semesterFilter
      const searchMatches =
        normalizedSearch.length === 0 || row.disciplineName.toLowerCase().includes(normalizedSearch)

      return semesterMatches && searchMatches
    })
  }, [disciplines, semesterFilter, deferredSearchText])

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
    } catch (err: unknown) {
      setError(err instanceof Error ? err.message : "Не вдалося обчислити середній бал.")
    } finally {
      setIsAverageLoading(false)
    }
  }

  if (isLoading) {
    return <Spinner label="Завантаження предметів..." />
  }

  if (error) {
    return <StatusState tone="error" message={error} />
  }

  return (
    <div className="page-stack">
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
        ) : (
          <div></div>
        )}
      </section>

      <section className="panel">
        <h2>Предмети студента</h2>
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
                </tr>
              </thead>
              <tbody>
                {filteredRows.map((row) => {
                  const grade = gradeMap.get(
                    buildGradeKey(row.disciplineName, row.semesterNo, row.academicYearStart),
                  )

                  return (
                    <tr key={`${row.disciplineId}-${row.semesterNo}-${row.academicYearStart}`}>
                      <td>
                        <span
                          className={`grade-dot ${row.hasGrade ? "grade-dot--done" : ""}`}
                          title={row.hasGrade ? "Є оцінка" : "Без оцінки"}
                        />
                      </td>
                      <td>{row.disciplineName}</td>
                      <td>{row.semesterNo}</td>
                      <td>{row.academicYearLabel}</td>
                      <td>{grade?.gradeValue ?? "—"}</td>
                      <td>{grade ? formatDate(grade.assessmentDate) : "—"}</td>
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
