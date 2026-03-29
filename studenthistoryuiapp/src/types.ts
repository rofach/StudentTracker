export type StudentDto = {
  studentId: number
  firstName: string
  lastName: string
  status: string
}

export type PagedResult<T> = {
  items: T[]
  page: number
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

export type TimelineEventDto = {
  eventType: string
  description: string
  dateFrom: string
  dateTo: string | null
}

export type AppPage = 'dashboard' | 'timeline'
