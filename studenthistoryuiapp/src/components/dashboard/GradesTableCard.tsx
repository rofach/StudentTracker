import type { AsyncCollectionState, GradeDto } from '../../types'
import { formatDate } from '../../utils/formatters'
import { LoadingOverlay } from '../LoadingOverlay'
import { PaginationControls } from '../PaginationControls'

type GradesTableCardProps = {
  gradesState: AsyncCollectionState<GradeDto>
  handleGradesPageChange: (page: number) => void
}

export function GradesTableCard({
  gradesState,
  handleGradesPageChange,
}: GradesTableCardProps) {
  const { items: grades, pagination, hasLoaded, isLoading } = gradesState
  const hasGrades = grades.length > 0
  const shouldShowEmptyState = !isLoading && hasLoaded && !hasGrades
  const shouldShowPagination = pagination.totalCount > 0

  return (
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

        {isLoading && (
          <LoadingOverlay className="surface-overlay" message="Оновлюємо дані..." />
        )}
      </div>

      {shouldShowEmptyState && (
        <p className="card-message">Поки немає даних для відображення.</p>
      )}

      {shouldShowPagination && (
        <PaginationControls
          pagination={pagination}
          disabled={isLoading}
          handlePageChange={handleGradesPageChange}
        />
      )}
    </section>
  )
}
