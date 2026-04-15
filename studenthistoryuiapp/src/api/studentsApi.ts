import type {
  AverageGradeDto,
  ChangeStatusDto,
  ClassmateDto,
  CloseEnrollmentDto,
  CreateLeaveDto,
  EnrollStudentDto,
  EnrollmentSummaryDto,
  ExternalTransferDto,
  GradeDto,
  MoveStudentDto,
  PagedResult,
  StudentCreateDto,
  StudentDetailDto,
  StudentDisciplineOptionDto,
  StudentDto,
  StudentUpdateDto,
  TimelineEventDto,
} from "../types/api"
import { fetchJson, postJson, putJson } from "./http"
import { getActiveGroups } from "./groupsApi"

export function getStudents(page: number, pageSize: number): Promise<PagedResult<StudentDto>> {
  return fetchJson<PagedResult<StudentDto>>("/students", undefined, { page, pageSize })
}

export function searchStudents(params: {
  fullName?: string
  email?: string
  status?: string
  page: number
  pageSize: number
}): Promise<PagedResult<StudentDto>> {
  return fetchJson<PagedResult<StudentDto>>("/students/search", undefined, params)
}

export function getStudentById(studentId: number): Promise<StudentDto> {
  return fetchJson<StudentDto>(`/students/${studentId}`)
}

export function createStudent(dto: StudentCreateDto): Promise<StudentDto> {
  return postJson<StudentDto>("/students", dto)
}

export function updateStudent(studentId: number, dto: StudentUpdateDto): Promise<StudentDto> {
  return putJson<StudentDto>(`/students/${studentId}`, dto)
}

export function changeStudentStatus(studentId: number, dto: ChangeStatusDto): Promise<void> {
  return putJson<void>(`/students/${studentId}/status`, dto)
}

export function getStudentDetails(studentId: number): Promise<StudentDetailDto> {
  return fetchJson<StudentDetailDto>(`/students/${studentId}/details`)
}

export function getStudentEnrollments(studentId: number): Promise<EnrollmentSummaryDto[]> {
  return getStudentDetails(studentId).then((result) => result.enrollments)
}

export function getStudentTimeline(
  studentId: number,
  page: number,
  pageSize: number,
): Promise<PagedResult<TimelineEventDto>> {
  return fetchJson<PagedResult<TimelineEventDto>>(`/students/${studentId}/timeline`, undefined, { page, pageSize })
}

export function getStudentClassmates(
  studentId: number,
  dateFrom?: string,
  dateTo?: string,
): Promise<ClassmateDto[]> {
  return fetchJson<ClassmateDto[]>(`/students/${studentId}/classmates`, undefined, {
    dateFrom,
    dateTo,
  })
}

export function getStudentDisciplines(studentId: number): Promise<StudentDisciplineOptionDto[]> {
  return fetchJson<StudentDisciplineOptionDto[]>(`/students/${studentId}/disciplines`)
}

export function getStudentGrades(
  studentId: number,
  page: number,
  pageSize: number,
): Promise<PagedResult<GradeDto>> {
  return fetchJson<PagedResult<GradeDto>>(`/students/${studentId}/grades`, undefined, { page, pageSize })
}

export function getStudentAverageGrade(
  studentId: number,
  params: {
    semesterNo?: number
    disciplineId?: number
    academicYearStart?: number
  },
): Promise<AverageGradeDto> {
  return fetchJson<AverageGradeDto>(`/students/${studentId}/grades/average`, undefined, params)
}

export function getStudentTransfers(studentId: number): Promise<ExternalTransferDto[]> {
  return getStudentDetails(studentId).then((result) => result.transfers)
}

export function moveStudentToGroup(studentId: number, dto: MoveStudentDto): Promise<void> {
  return postJson<void>(`/students/${studentId}/move`, dto)
}

export function createAcademicLeave(studentId: number, dto: CreateLeaveDto): Promise<void> {
  return postJson<void>(`/students/${studentId}/leaves`, dto)
}

export function enrollStudent(dto: EnrollStudentDto): Promise<{ enrollmentId: number }> {
  return postJson<{ enrollmentId: number }>("/enrollments", dto)
}

export function closeEnrollment(enrollmentId: number, dto: CloseEnrollmentDto): Promise<void> {
  return putJson<void>(`/enrollments/${enrollmentId}/close`, dto)
}

export async function getSelectableGroups(date?: string) {
  return getActiveGroups(date)
}
