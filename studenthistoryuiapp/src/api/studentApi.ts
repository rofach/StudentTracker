import type {
  AverageGradeDto,
  AverageGradeFilters,
  GradeDto,
  PagedResult,
  StudentDto,
  StudentDisciplineOptionDto,
  TimelineEventDto,
} from '../types'
import { fetchJson } from './http'

export const DEFAULT_PAGE_SIZE = 20

export function getStudents(page: number, pageSize = DEFAULT_PAGE_SIZE): Promise<PagedResult<StudentDto>> {
  return fetchJson<PagedResult<StudentDto>>(`/api/students?page=${page}&pageSize=${pageSize}`)
}

export function getGrades(
  studentId: number,
  page: number,
  pageSize = DEFAULT_PAGE_SIZE,
): Promise<PagedResult<GradeDto>> {
  return fetchJson<PagedResult<GradeDto>>(
    `/api/students/${studentId}/grades?page=${page}&pageSize=${pageSize}`,
  )
}

export function getAverageGrade(
  studentId: number,
  filters: AverageGradeFilters,
): Promise<AverageGradeDto> {
  const params = new URLSearchParams()

  if (filters.semesterNo.trim()) {
    params.set('semesterNo', filters.semesterNo.trim())
  }

  if (filters.disciplineId.trim()) {
    params.set('disciplineId', filters.disciplineId.trim())
  }

  if (filters.academicYearStart.trim()) {
    params.set('academicYearStart', filters.academicYearStart.trim())
  }

  const queryString = params.toString()
  const url = queryString
    ? `/api/students/${studentId}/grades/average?${queryString}`
    : `/api/students/${studentId}/grades/average`

  return fetchJson<AverageGradeDto>(url)
}

export function getTimeline(
  studentId: number,
  page: number,
  pageSize = DEFAULT_PAGE_SIZE,
): Promise<PagedResult<TimelineEventDto>> {
  return fetchJson<PagedResult<TimelineEventDto>>(
    `/api/students/${studentId}/timeline?page=${page}&pageSize=${pageSize}`,
  )
}

export function getStudentDisciplines(
  studentId: number,
): Promise<StudentDisciplineOptionDto[]> {
  return fetchJson<StudentDisciplineOptionDto[]>(`/api/students/${studentId}/disciplines`)
}
