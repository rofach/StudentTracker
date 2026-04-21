export type EntityId = string

export type DifferenceItemStatus = "Pending" | "Completed" | "Waived"

export type PagedResult<TItem> = {
  items: TItem[]
  page: number
  pageSize: number
  totalCount: number
}

export type StudentDto = {
  studentId: EntityId
  firstName: string
  lastName: string
  patronymic: string | null
  birthDate: string | null
  email: string | null
  phone: string | null
  status: string
}

export type EnrollmentSummaryDto = {
  enrollmentId: EntityId
  groupId: EntityId
  groupCode: string
  departmentName: string
  academicUnitName: string
  dateFrom: string
  dateTo: string | null
  subgroupId: EntityId | null
  subgroupName: string | null
}

export type GroupPlanAssignmentDto = {
  groupPlanAssignmentId: EntityId
  groupId: EntityId
  planId: EntityId
  specialtyCode: string
  planName: string | null
  dateFrom: string
  dateTo: string | null
}

export type AcademicLeaveDto = {
  leaveId: EntityId
  startDate: string
  endDate: string | null
  reason: string | null
  returnReason: string | null
}

export type ExternalTransferDto = {
  transferId: EntityId
  transferType: string
  transferDate: string
  institutionName: string
  notes: string | null
}

export type StudentInternalTransferSummaryDto = {
  transferId: EntityId
  transferDate: string
  reason: string
  oldEnrollmentId: EntityId
  oldGroupCode: string
  newEnrollmentId: EntityId
  newGroupCode: string
  differenceItemsTotal: number
  differenceItemsPending: number
  differenceItemsCompleted: number
  differenceItemsWaived: number
}

export type StudentDetailDto = {
  studentId: EntityId
  firstName: string
  lastName: string
  patronymic: string | null
  birthDate: string | null
  email: string | null
  phone: string | null
  status: string
  isOnAcademicLeave: boolean
  enrollments: EnrollmentSummaryDto[]
  plans: GroupPlanAssignmentDto[]
  leaves: AcademicLeaveDto[]
  transfers: ExternalTransferDto[]
  internalTransfers: StudentInternalTransferSummaryDto[]
}

export type StudentDisciplineOptionDto = {
  courseEnrollmentId: EntityId
  disciplineId: EntityId
  disciplineName: string
  semesterNo: number
  academicYearStart: number
  academicYearLabel: string
  hasGrade: boolean
}

export type GradeDto = {
  gradeId: EntityId
  courseEnrollmentId: EntityId
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

export type UpsertGradeDto = {
  gradeValue: string
  assessmentDate: string
}

export type ClassmateDto = {
  classmateStudentId: EntityId
  firstName: string
  lastName: string
  groupId: EntityId
  groupCode: string
  subgroupId: EntityId | null
  subgroupName: string | null
  sharedFrom: string
  sharedTo: string | null
}

export type TimelineEventDto = {
  eventType: string
  description: string
  dateFrom: string
  dateTo: string | null
  groupCode: string | null
  departmentName: string | null
  academicUnitName: string | null
  academicUnitType: string | null
}

export type StudentMovementDto = {
  leaves: AcademicLeaveDto[]
  transfers: ExternalTransferDto[]
  internalTransfers: StudentInternalTransferSummaryDto[]
}

export type GroupCompositionMemberDto = {
  studentId: EntityId
  firstName: string
  lastName: string
  email: string | null
  subgroupId: EntityId | null
  subgroupName: string | null
  dateFrom: string
  dateTo: string | null
}

export type ActiveGroupDto = {
  groupId: EntityId
  groupCode: string
  departmentName: string
  academicUnitName: string
  academicUnitType: string
  dateCreated: string
  dateClosed: string | null
}

export type SubgroupDto = {
  subgroupId: EntityId
  subgroupName: string
}

export type GroupStudentDto = {
  enrollmentId: EntityId
  studentId: EntityId
  firstName: string
  lastName: string
  email: string | null
  subgroupId: EntityId | null
  subgroupName: string | null
  dateFrom: string
  dateTo: string | null
}

export type StudentCurrentGroupDto = {
  enrollmentId: EntityId
  groupId: EntityId
  groupCode: string
  departmentName: string
  academicUnitName: string
  academicUnitType: string
  subgroupId: EntityId | null
  subgroupName: string | null
  dateFrom: string
  dateTo: string | null
}

export type StudyPlanDto = {
  planId: EntityId
  specialtyCode: string
  planName: string | null
  validFrom: string
}

export type PlanDisciplineDto = {
  planId: EntityId
  disciplineId: EntityId
  disciplineName: string
  semesterNo: number
  controlType: string
  hours: number
  credits: number
}

export type AddPlanDisciplineDto = {
  disciplineId: EntityId
  semesterNo: number
  controlType: string
  hours: number
  credits: number
}

export type UpdatePlanDisciplineDto = {
  semesterNo: number
  controlType: string
  hours: number
  credits: number
}

export type DisciplineDto = {
  disciplineId: EntityId
  disciplineName: string
  description: string | null
}

export type DisciplineSearchItemDto = {
  disciplineId: EntityId
  disciplineName: string
  description: string | null
  planUsageCount: number
}

export type AcademicUnitDto = {
  academicUnitId: EntityId
  name: string
  type: string
  departments: DepartmentSummaryDto[]
}

export type DepartmentSummaryDto = {
  departmentId: EntityId
  name: string
}

export type DepartmentDto = {
  departmentId: EntityId
  academicUnitId: EntityId
  name: string
  academicUnitName: string
  academicUnitType: string
}

export type StudentCreateDto = {
  firstName: string
  lastName: string
  patronymic: string | null
  birthDate: string | null
  email: string | null
  phone: string | null
}

export type StudentUpdateDto = StudentCreateDto

export type ChangeStatusDto = {
  status: string
}

export type EnrollStudentDto = {
  studentId: EntityId
  groupId: EntityId
  subgroupId: EntityId | null
  dateFrom: string
  reasonStart: string
}

export type CloseEnrollmentDto = {
  dateTo: string
  reasonEnd: string
}

export type MoveStudentDto = {
  newGroupId: EntityId
  newSubgroupId: EntityId | null
  moveDate: string
  reasonEnd: string
  reasonStart: string
}

export type AssignSubgroupDto = {
  subgroupId: EntityId
}

export type MoveStudentToSubgroupDto = {
  newSubgroupId: EntityId
  moveDate: string
  reason: string
}

export type CreateLeaveDto = {
  enrollmentId: EntityId
  startDate: string
  endDate?: string | null
  reason: string
}

export type CloseAcademicLeaveDto = {
  endDate: string
  returnReason: string | null
}

export type AssignGroupPlanDto = {
  planId: EntityId
  dateFrom: string
}

export type ChangeGroupPlanDto = {
  newPlanId: EntityId
  newPlanDateFrom: string
}

export type CreateStudyPlanDto = {
  specialtyCode: string
  planName: string | null
  validFrom: string
}

export type UpdateStudyPlanDto = CreateStudyPlanDto

export type CreateDisciplineDto = {
  disciplineName: string
  description: string | null
}

export type UpdateDisciplineDto = CreateDisciplineDto

export type CreateAcademicUnitDto = {
  name: string
  type: string
}

export type UpdateAcademicUnitDto = CreateAcademicUnitDto

export type CreateDepartmentDto = {
  academicUnitId: EntityId
  name: string
}

export type UpdateDepartmentDto = {
  name: string
}

export type AcademicDifferenceItemDto = {
  differenceItemId: EntityId
  transferId: EntityId
  planDisciplineId: EntityId
  disciplineName: string
  semesterNo: number
  status: DifferenceItemStatus
  notes: string | null
}

export type UpdateDifferenceItemDto = {
  status: DifferenceItemStatus
  notes: string | null
}

export type StudentGroupTransferDto = {
  transferId: EntityId
  studentId: EntityId
  oldEnrollmentId: EntityId
  newEnrollmentId: EntityId
  transferDate: string
  reason: string
}

export type StudentGroupTransferDetailDto = {
  transferId: EntityId
  studentId: EntityId
  oldEnrollmentId: EntityId
  oldGroupCode: string
  newEnrollmentId: EntityId
  newGroupCode: string
  transferDate: string
  reason: string
  differenceItems: AcademicDifferenceItemDto[]
}

export type ActiveAcademicDifferenceDto = {
  differenceItemId: EntityId
  transferId: EntityId
  studentId: EntityId
  studentName: string
  oldGroupCode: string
  newGroupCode: string
  transferDate: string
  disciplineName: string
  semesterNo: number
  status: DifferenceItemStatus
  notes: string | null
}

export type InternalTransferJournalItemDto = {
  transferId: EntityId
  studentId: EntityId
  studentName: string
  oldGroupCode: string
  newGroupCode: string
  transferDate: string
  reason: string
  differenceItemsTotal: number
  differenceItemsPending: number
  differenceItemsCompleted: number
  differenceItemsWaived: number
}
