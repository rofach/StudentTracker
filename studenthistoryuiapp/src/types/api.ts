export type PagedResult<TItem> = {
  items: TItem[]
  page: number
  pageSize: number
  totalCount: number
}

export type StudentDto = {
  studentId: number
  firstName: string
  lastName: string
  birthDate: string | null
  email: string | null
  phone: string | null
  status: string
}

export type EnrollmentSummaryDto = {
  enrollmentId: number
  groupId: number
  groupCode: string
  departmentName: string
  academicUnitName: string
  dateFrom: string
  dateTo: string | null
  subgroupId: number | null
  subgroupName: string | null
}

export type StudyPlanAssignmentDto = {
  assignmentId: number
  specialtyCode: string
  planName: string | null
  dateFrom: string
  dateTo: string | null
}

export type AcademicLeaveDto = {
  leaveId: number
  startDate: string
  endDate: string | null
  reason: string | null
}

export type ExternalTransferDto = {
  transferId: number
  transferType: string
  transferDate: string
  institutionName: string
  notes: string | null
}

export type StudentDetailDto = {
  studentId: number
  firstName: string
  lastName: string
  birthDate: string | null
  email: string | null
  phone: string | null
  status: string
  enrollments: EnrollmentSummaryDto[]
  plans: StudyPlanAssignmentDto[]
  leaves: AcademicLeaveDto[]
  transfers: ExternalTransferDto[]
}

export type StudentDisciplineOptionDto = {
  disciplineId: number
  disciplineName: string
  semesterNo: number
  academicYearStart: number
  academicYearLabel: string
  hasGrade: boolean
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

export type ClassmateDto = {
  classmateStudentId: number
  firstName: string
  lastName: string
  groupId: number
  groupCode: string
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

export type GroupCompositionMemberDto = {
  studentId: number
  firstName: string
  lastName: string
  email: string | null
  subgroupId: number | null
  subgroupName: string | null
  dateFrom: string
  dateTo: string | null
}

export type ActiveGroupDto = {
  groupId: number
  groupCode: string
  departmentName: string
  academicUnitName: string
  academicUnitType: string
  dateCreated: string
  dateClosed: string | null
}

export type GroupStudentDto = {
  enrollmentId: number
  studentId: number
  firstName: string
  lastName: string
  email: string | null
  dateFrom: string
  dateTo: string | null
}

export type StudyPlanDto = {
  planId: number
  specialtyCode: string
  planName: string | null
  validFrom: string
}

export type PlanDisciplineDto = {
  planId: number
  disciplineId: number
  disciplineName: string
  semesterNo: number
  controlType: string
  hours: number
  credits: number
}

export type AddPlanDisciplineDto = {
  disciplineId: number
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
  disciplineId: number
  disciplineName: string
}

export type AcademicUnitDto = {
  academicUnitId: number
  name: string
  type: string
  departments: DepartmentSummaryDto[]
}

export type DepartmentSummaryDto = {
  departmentId: number
  name: string
}

export type DepartmentDto = {
  departmentId: number
  academicUnitId: number
  name: string
  academicUnitName: string
  academicUnitType: string
}

export type StudentCreateDto = {
  firstName: string
  lastName: string
  birthDate: string | null
  email: string | null
  phone: string | null
}

export type StudentUpdateDto = StudentCreateDto

export type ChangeStatusDto = {
  status: string
}

export type EnrollStudentDto = {
  studentId: number
  groupId: number
  subgroupId: number | null
  dateFrom: string
  reasonStart: string
}

export type CloseEnrollmentDto = {
  dateTo: string
  reasonEnd: string
}

export type MoveStudentDto = {
  newGroupId: number
  newSubgroupId: number | null
  moveDate: string
  reasonEnd: string
  reasonStart: string
}

export type CreateLeaveDto = {
  enrollmentId: number
  startDate: string
  reason: string | null
}

export type AssignPlanDto = {
  planId: number
  dateFrom: string
}

export type CreateStudyPlanDto = {
  specialtyCode: string
  planName: string | null
  validFrom: string
}

export type UpdateStudyPlanDto = CreateStudyPlanDto

export type CreateDisciplineDto = {
  disciplineName: string
}

export type UpdateDisciplineDto = CreateDisciplineDto

export type CreateAcademicUnitDto = {
  name: string
  type: string
}

export type UpdateAcademicUnitDto = CreateAcademicUnitDto

export type CreateDepartmentDto = {
  academicUnitId: number
  name: string
}

export type UpdateDepartmentDto = {
  name: string
}
