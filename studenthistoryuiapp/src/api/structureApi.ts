import type {
  AcademicUnitDto,
  CreateAcademicUnitDto,
  CreateDepartmentDto,
  DepartmentDto,
  EntityId,
  UpdateAcademicUnitDto,
  UpdateDepartmentDto,
} from "../types/api"
import { deleteJson, fetchJson, postJson, putJson } from "./http"

export function getAcademicUnits(): Promise<AcademicUnitDto[]> {
  return fetchJson<AcademicUnitDto[]>("/academicunits")
}

export function createAcademicUnit(dto: CreateAcademicUnitDto): Promise<AcademicUnitDto> {
  return postJson<AcademicUnitDto>("/academicunits", dto)
}

export function updateAcademicUnit(academicUnitId: EntityId, dto: UpdateAcademicUnitDto): Promise<AcademicUnitDto> {
  return putJson<AcademicUnitDto>(`/academicunits/${academicUnitId}`, dto)
}

export function deleteAcademicUnit(academicUnitId: EntityId): Promise<void> {
  return deleteJson(`/academicunits/${academicUnitId}`)
}

export function getDepartments(): Promise<DepartmentDto[]> {
  return fetchJson<DepartmentDto[]>("/departments")
}

export function createDepartment(dto: CreateDepartmentDto): Promise<DepartmentDto> {
  return postJson<DepartmentDto>("/departments", dto)
}

export function updateDepartment(departmentId: EntityId, dto: UpdateDepartmentDto): Promise<DepartmentDto> {
  return putJson<DepartmentDto>(`/departments/${departmentId}`, dto)
}

export function deleteDepartment(departmentId: EntityId): Promise<void> {
  return deleteJson(`/departments/${departmentId}`)
}
