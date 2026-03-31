import type {
  AverageGradeFilters,
  StudentDisciplineOptionDto,
  StudentSelectionState,
} from '../../types'
import { StudentSelectionPanel, type StudentSelectionHandlers } from '../StudentSelectionPanel'

type AverageFiltersCardProps = {
  selection: StudentSelectionState
  selectionHandlers: StudentSelectionHandlers
  filters: AverageGradeFilters
  semesterOptions: number[]
  disciplineOptions: StudentDisciplineOptionDto[]
  errorMessage: string | null
  isLoadingAverage: boolean
  isLoadingDisciplines: boolean
  handleSemesterFilterChange: (value: string) => void
  handleDisciplineFilterChange: (value: string) => void
  handleAverageRefresh: () => void
}

function getDisciplineOptionLabel(discipline: StudentDisciplineOptionDto): string {
  if (discipline.hasGrade) {
    return `● ${discipline.disciplineName}`
  }

  return discipline.disciplineName
}

export function AverageFiltersCard({
  selection,
  selectionHandlers,
  filters,
  semesterOptions,
  disciplineOptions,
  errorMessage,
  isLoadingAverage,
  isLoadingDisciplines,
  handleSemesterFilterChange,
  handleDisciplineFilterChange,
  handleAverageRefresh,
}: AverageFiltersCardProps) {
  const refreshButtonLabel = isLoadingAverage ? 'Оновлення...' : 'Оновити середній бал'
  const isDisciplineSelectDisabled = isLoadingDisciplines || disciplineOptions.length === 0

  return (
    <StudentSelectionPanel
      title="Налаштування"
      selection={selection}
      handlers={selectionHandlers}
    >
      <div className="filters">
        <label>
          Семестр
          <select
            value={filters.semesterNo}
            onChange={(event) => handleSemesterFilterChange(event.target.value)}
            disabled={isLoadingDisciplines || semesterOptions.length === 0}
          >
            <option value="">Усі семестри</option>
            {semesterOptions.map((semesterNo) => (
              <option key={semesterNo} value={semesterNo}>
                Семестр {semesterNo}
              </option>
            ))}
          </select>
        </label>

        <label>
          Предмет
          <select
            value={filters.disciplineId}
            onChange={(event) => handleDisciplineFilterChange(event.target.value)}
            disabled={isDisciplineSelectDisabled}
          >
            <option value="">Усі предмети</option>
            {disciplineOptions.map((discipline) => (
              <option key={`${discipline.disciplineId}-${discipline.semesterNo}`} value={discipline.disciplineId}>
                {getDisciplineOptionLabel(discipline)}
              </option>
            ))}
          </select>
        </label>
      </div>

      <button
        className="primary"
        type="button"
        disabled={isLoadingAverage}
        onClick={handleAverageRefresh}
      >
        {refreshButtonLabel}
      </button>

      {errorMessage && <p className="error">{errorMessage}</p>}
    </StudentSelectionPanel>
  )
}
