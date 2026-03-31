import type {
  AsyncCollectionState,
  AverageGradeFilters,
  AverageSummaryState,
  GradeDto,
  StudentDisciplineOptionDto,
  StudentSelectionState,
} from '../types'
import type { StudentSelectionHandlers } from './StudentSelectionPanel'
import { AverageFiltersCard } from './dashboard/AverageFiltersCard'
import { AverageSummaryCard } from './dashboard/AverageSummaryCard'
import { GradesTableCard } from './dashboard/GradesTableCard'

type DashboardHandlers = {
  handleSemesterFilterChange: (value: string) => void
  handleDisciplineFilterChange: (value: string) => void
  handleAverageRefresh: () => void
  handleGradesPageChange: (page: number) => void
}

export type DashboardPageProps = {
  studentSelection: StudentSelectionState
  selectionHandlers: StudentSelectionHandlers
  filters: AverageGradeFilters
  averageState: AverageSummaryState
  gradesState: AsyncCollectionState<GradeDto>
  semesterOptions: number[]
  disciplineOptions: StudentDisciplineOptionDto[]
  isLoadingDisciplineOptions: boolean
  errorMessage: string | null
  handlers: DashboardHandlers
}

export function DashboardPage({
  studentSelection,
  selectionHandlers,
  filters,
  averageState,
  gradesState,
  semesterOptions,
  disciplineOptions,
  isLoadingDisciplineOptions,
  errorMessage,
  handlers,
}: DashboardPageProps) {
  return (
    <main className="grid">
      <AverageFiltersCard
        selection={studentSelection}
        selectionHandlers={selectionHandlers}
        filters={filters}
        semesterOptions={semesterOptions}
        disciplineOptions={disciplineOptions}
        errorMessage={errorMessage}
        isLoadingAverage={averageState.isLoading}
        isLoadingDisciplines={isLoadingDisciplineOptions}
        handleSemesterFilterChange={handlers.handleSemesterFilterChange}
        handleDisciplineFilterChange={handlers.handleDisciplineFilterChange}
        handleAverageRefresh={handlers.handleAverageRefresh}
      />

      <AverageSummaryCard averageState={averageState} />

      <GradesTableCard
        gradesState={gradesState}
        handleGradesPageChange={handlers.handleGradesPageChange}
      />
    </main>
  )
}
