export type StudentDto = {
  studentId: number
  firstName: string
  lastName: string
  birthDate: string | null
  email: string | null
  phone: string | null
  status: string
}

export type PagedResult<T> = {
  items: T[]
  page: number
  pageSize: number
  totalCount: number
}

export type PaginationState = {
  currentPage: number
  pageSize: number
  totalCount: number
}

export type GradeDto = {
  gradeId: number
  disciplineName: string
  semesterNo: number
  academicYearStart: number
  academicYearLabel: string
  gradeValue: string
  assessmentDate: string
}

export type AverageGradeDto = {
  average: number | null
  gradeCount: number
  academicYearLabel: string | null
}

export type AverageSummaryState = {
  data: AverageGradeDto | null
  hasLoaded: boolean
  isLoading: boolean
}

export type AverageGradeFilters = {
  semesterNo: string
  disciplineId: string
  academicYearStart: string
}

export type StudentDisciplineOptionDto = {
  disciplineId: number
  disciplineName: string
  semesterNo: number
  hasGrade: boolean
}

export type TimelineEventDto = {
  eventType: string
  description: string
  dateFrom: string
  dateTo: string | null
}

export type AsyncCollectionState<T> = {
  items: T[]
  pagination: PaginationState
  hasLoaded: boolean
  isLoading: boolean
}

export type StudentSelectionState = {
  students: StudentDto[]
  pagination: PaginationState
  selectedStudentId: number | null
  isLoading: boolean
}

export type AppPage = 'dashboard' | 'timeline'
