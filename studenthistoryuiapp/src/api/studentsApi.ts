import type {
  AverageGradeDto,
  ChangeStatusDto,
  ClassmateDto,
  CloseEnrollmentDto,
  CreateLeaveDto,
  EntityId,
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

export function getStudentById(studentId: EntityId): Promise<StudentDto> {
  return fetchJson<StudentDto>(`/students/${studentId}`)
}

export function createStudent(dto: StudentCreateDto): Promise<StudentDto> {
  return postJson<StudentDto>("/students", dto)
}

export function updateStudent(studentId: EntityId, dto: StudentUpdateDto): Promise<StudentDto> {
  return putJson<StudentDto>(`/students/${studentId}`, dto)
}

export function changeStudentStatus(studentId: EntityId, dto: ChangeStatusDto): Promise<void> {
  return putJson<void>(`/students/${studentId}/status`, dto)
}

export function getStudentDetails(studentId: EntityId): Promise<StudentDetailDto> {
  return fetchJson<StudentDetailDto>(`/students/${studentId}/details`)
}

export function getStudentEnrollments(studentId: EntityId): Promise<EnrollmentSummaryDto[]> {
  return getStudentDetails(studentId).then((result) => result.enrollments)
}

export function getStudentTimeline(
  studentId: EntityId,
  page: number,
  pageSize: number,
): Promise<PagedResult<TimelineEventDto>> {
  return fetchJson<PagedResult<TimelineEventDto>>(`/students/${studentId}/timeline`, undefined, { page, pageSize })
}

export function getStudentClassmates(
  studentId: EntityId,
  dateFrom?: string,
  dateTo?: string,
): Promise<ClassmateDto[]> {
  return fetchJson<ClassmateDto[]>(`/students/${studentId}/classmates`, undefined, {
    dateFrom,
    dateTo,
  })
}

export function getStudentDisciplines(studentId: EntityId): Promise<StudentDisciplineOptionDto[]> {
  return fetchJson<StudentDisciplineOptionDto[]>(`/students/${studentId}/disciplines`)
}

export function getStudentGrades(
  studentId: EntityId,
  page: number,
  pageSize: number,
): Promise<PagedResult<GradeDto>> {
  return fetchJson<PagedResult<GradeDto>>(`/students/${studentId}/grades`, undefined, { page, pageSize })
}

export function getStudentAverageGrade(
  studentId: EntityId,
  params: {
    semesterNo?: number
    disciplineId?: EntityId
    academicYearStart?: number
  },
): Promise<AverageGradeDto> {
  return fetchJson<AverageGradeDto>(`/students/${studentId}/grades/average`, undefined, params)
}

export function getStudentTransfers(studentId: EntityId): Promise<ExternalTransferDto[]> {
  return getStudentDetails(studentId).then((result) => result.transfers)
}

export function moveStudentToGroup(studentId: EntityId, dto: MoveStudentDto): Promise<void> {
  return postJson<void>(`/students/${studentId}/move`, dto)
}

export function createAcademicLeave(studentId: EntityId, dto: CreateLeaveDto): Promise<void> {
  return postJson<void>(`/students/${studentId}/leaves`, dto)
}

export function enrollStudent(dto: EnrollStudentDto): Promise<{ enrollmentId: EntityId }> {
  return postJson<{ enrollmentId: EntityId }>("/enrollments", dto)
}

export function closeEnrollment(enrollmentId: EntityId, dto: CloseEnrollmentDto): Promise<void> {
  return putJson<void>(`/enrollments/${enrollmentId}/close`, dto)
}

export async function getSelectableGroups(date?: string) {
  return getActiveGroups(date)
}
