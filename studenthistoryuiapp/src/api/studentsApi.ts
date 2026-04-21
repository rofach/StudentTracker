import type {
  AcademicDifferenceItemDto,
  AverageGradeDto,
  ChangeStatusDto,
  ClassmateDto,
  CloseAcademicLeaveDto,
  CloseEnrollmentDto,
  CreateLeaveDto,
  EntityId,
  EnrollStudentDto,
  EnrollmentSummaryDto,
  ExternalTransferDto,
  GradeDto,
  MoveStudentDto,
  MoveStudentToSubgroupDto,
  PagedResult,
  StudentCreateDto,
  StudentCurrentGroupDto,
  StudentDetailDto,
  StudentDisciplineOptionDto,
  StudentDto,
  StudentGroupTransferDetailDto,
  StudentGroupTransferDto,
  StudentMovementDto,
  StudentUpdateDto,
  TimelineEventDto,
  TransferPreviewDto,
  TransferPreviewRequestDto,
  UpsertGradeDto,
  UpdateDifferenceItemDto,
  AssignSubgroupDto,
} from "../types/api"
import { fetchJson, patchJson, postJson, putJson } from "./http"
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

export function getStudentGroupOnDate(
  studentId: EntityId,
  date?: string,
): Promise<StudentCurrentGroupDto> {
  return fetchJson<StudentCurrentGroupDto>(`/students/${studentId}/group`, undefined, { date })
}

export function getStudentMovements(studentId: EntityId): Promise<StudentMovementDto> {
  return fetchJson<StudentMovementDto>(`/students/${studentId}/movements`)
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

export function upsertStudentGrade(
  studentId: EntityId,
  courseEnrollmentId: EntityId,
  dto: UpsertGradeDto,
): Promise<GradeDto> {
  return putJson<GradeDto>(`/students/${studentId}/grades/${courseEnrollmentId}`, dto)
}

export function getStudentTransfers(studentId: EntityId): Promise<ExternalTransferDto[]> {
  return getStudentDetails(studentId).then((result) => result.transfers)
}

export function previewStudentTransfer(
  studentId: EntityId,
  dto: TransferPreviewRequestDto,
): Promise<TransferPreviewDto> {
  return postJson<TransferPreviewDto>(`/students/${studentId}/transfer-preview`, dto)
}

export function getStudentGroupTransfers(studentId: EntityId): Promise<StudentGroupTransferDto[]> {
  return fetchJson<StudentGroupTransferDto[]>(`/students/${studentId}/group-transfers`)
}

export function getStudentGroupTransferDetail(
  studentId: EntityId,
  transferId: EntityId,
): Promise<StudentGroupTransferDetailDto> {
  return fetchJson<StudentGroupTransferDetailDto>(`/students/${studentId}/group-transfers/${transferId}`)
}

export function updateDifferenceItem(
  studentId: EntityId,
  transferId: EntityId,
  itemId: EntityId,
  dto: UpdateDifferenceItemDto,
): Promise<AcademicDifferenceItemDto> {
  return patchJson<AcademicDifferenceItemDto>(
    `/students/${studentId}/group-transfers/${transferId}/difference-items/${itemId}`,
    dto,
  )
}

export function moveStudentToGroup(studentId: EntityId, dto: MoveStudentDto): Promise<void> {
  return postJson<void>(`/students/${studentId}/move`, dto)
}

export function createAcademicLeave(studentId: EntityId, dto: CreateLeaveDto): Promise<void> {
  return postJson<void>(`/students/${studentId}/leaves`, dto)
}

export function closeAcademicLeave(leaveId: EntityId, dto: CloseAcademicLeaveDto): Promise<void> {
  return putJson<void>(`/leaves/${leaveId}/close`, dto)
}

export function enrollStudent(dto: EnrollStudentDto): Promise<{ enrollmentId: EntityId }> {
  return postJson<{ enrollmentId: EntityId }>("/enrollments", dto)
}

export function closeEnrollment(enrollmentId: EntityId, dto: CloseEnrollmentDto): Promise<void> {
  return putJson<void>(`/enrollments/${enrollmentId}/close`, dto)
}

export function assignEnrollmentSubgroup(enrollmentId: EntityId, dto: AssignSubgroupDto): Promise<void> {
  return putJson<void>(`/enrollments/${enrollmentId}/subgroup`, dto)
}

export function moveEnrollmentSubgroup(enrollmentId: EntityId, dto: MoveStudentToSubgroupDto): Promise<void> {
  return postJson<void>(`/enrollments/${enrollmentId}/subgroup-move`, dto)
}

export async function getSelectableGroups(date?: string) {
  return getActiveGroups(date)
}
