import type { AverageGradeDto, GradeDto, StudentDto } from '../types'
import { formatDate } from '../utils/formatters'
import { PaginationControls } from './PaginationControls'
import { Spinner } from './Spinner'
import { StudentSelectionPanel } from './StudentSelectionPanel'

export type DashboardPageProps = {
  students: StudentDto[]
  studentsPage: number
  studentsPageSize: number
  studentsTotalCount: number
  selectedStudentId: number | null
  isLoadingStudents: boolean
  semesterNo: string
  disciplineId: string
  academicYearStart: string
  isLoadingAverage: boolean
  isLoadingGrades: boolean
  average: AverageGradeDto | null
  hasLoadedAverage: boolean
  grades: GradeDto[]
  gradesPage: number
  gradesPageSize: number
  gradesTotalCount: number
  hasLoadedGrades: boolean
  error: string | null
  onStudentChange: (studentId: number) => void
  onStudentsPageChange: (page: number) => void
  onSemesterNoChange: (value: string) => void
  onDisciplineIdChange: (value: string) => void
  onAcademicYearStartChange: (value: string) => void
  onRefreshAverage: () => void
  onGradesPageChange: (page: number) => void
}

export function DashboardPage({
  students,
  studentsPage,
  studentsPageSize,
  studentsTotalCount,
  selectedStudentId,
  isLoadingStudents,
  semesterNo,
  disciplineId,
  academicYearStart,
  isLoadingAverage,
  isLoadingGrades,
  average,
  hasLoadedAverage,
  grades,
  gradesPage,
  gradesPageSize,
  gradesTotalCount,
  hasLoadedGrades,
  error,
  onStudentChange,
  onStudentsPageChange,
  onSemesterNoChange,
  onDisciplineIdChange,
  onAcademicYearStartChange,
  onRefreshAverage,
  onGradesPageChange,
}: DashboardPageProps) {
  return (
    <main className="grid">
      <StudentSelectionPanel
        title="Налаштування"
        students={students}
        studentsPage={studentsPage}
        studentsPageSize={studentsPageSize}
        studentsTotalCount={studentsTotalCount}
        selectedStudentId={selectedStudentId}
        isLoadingStudents={isLoadingStudents}
        onStudentChange={onStudentChange}
        onStudentsPageChange={onStudentsPageChange}
      >
        <div className="filters">
          <label>
            Семестр
            <input
              type="number"
              min={1}
              value={semesterNo}
              onChange={(event) => onSemesterNoChange(event.target.value)}
              placeholder="Напр. 1 або 3"
            />
          </label>

          <label>
            ID предмета
            <input
              type="number"
              min={1}
              value={disciplineId}
              onChange={(event) => onDisciplineIdChange(event.target.value)}
              placeholder="Напр. 2"
            />
          </label>

          <label>
            Початок навчального року
            <input
              type="number"
              min={2000}
              value={academicYearStart}
              onChange={(event) => onAcademicYearStartChange(event.target.value)}
              placeholder="Напр. 2024"
            />
          </label>
        </div>

        <button className="primary" type="button" disabled={isLoadingAverage} onClick={onRefreshAverage}>
          {isLoadingAverage ? 'Оновлення...' : 'Оновити середній бал'}
        </button>

        {error && <p className="error">{error}</p>}
      </StudentSelectionPanel>

      <section className="card summary">
        <h2>Середній бал</h2>
        <div className="metric-wrap">
          <div className="metric">
            <span className="metric-value">
              {average?.average !== null && average?.average !== undefined
                ? average.average.toFixed(2)
                : '-'}
            </span>
            <span className="metric-label">AVG</span>
          </div>
          {isLoadingAverage && <Spinner big />}
        </div>
        <p>Кількість оцінок у вибірці: {average?.gradeCount ?? 0}</p>
        <p>Навчальний рік: {hasLoadedAverage ? average?.academicYearLabel ?? 'усі роки' : '-'}</p>
      </section>

      <section className="card table-card">
        <h2>Оцінки студента</h2>
        <div className="table-shell">
          <div className="table-wrap">
            <table>
              <thead>
                <tr>
                  <th>Предмет</th>
                  <th>Семестр</th>
                  <th>Навч. рік</th>
                  <th>Оцінка</th>
                  <th>Дата</th>
                </tr>
              </thead>
              <tbody>
                {grades.map((grade) => (
                  <tr key={grade.gradeId}>
                    <td>{grade.disciplineName}</td>
                    <td>{grade.semesterNo}</td>
                    <td>{grade.academicYearLabel}</td>
                    <td>{grade.gradeValue}</td>
                    <td>{formatDate(grade.assessmentDate)}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          {isLoadingGrades && (
            <div className="table-overlay">
              <Spinner big />
              <p>Оновлюємо дані...</p>
            </div>
          )}
        </div>

        {!isLoadingGrades && hasLoadedGrades && grades.length === 0 && (
          <p>Поки немає даних для відображення.</p>
        )}

        {gradesTotalCount > 0 && (
          <PaginationControls
            currentPage={gradesPage}
            pageSize={gradesPageSize}
            totalCount={gradesTotalCount}
            disabled={isLoadingGrades}
            onPageChange={onGradesPageChange}
          />
        )}
      </section>
    </main>
  )
}
