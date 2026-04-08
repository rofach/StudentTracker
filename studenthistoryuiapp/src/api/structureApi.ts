import type {
  AcademicUnitDto,
  CreateAcademicUnitDto,
  CreateDepartmentDto,
  DepartmentDto,
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

export function updateAcademicUnit(academicUnitId: number, dto: UpdateAcademicUnitDto): Promise<AcademicUnitDto> {
  return putJson<AcademicUnitDto>(`/academicunits/${academicUnitId}`, dto)
}

export function deleteAcademicUnit(academicUnitId: number): Promise<void> {
  return deleteJson(`/academicunits/${academicUnitId}`)
}

export function getDepartments(): Promise<DepartmentDto[]> {
  return fetchJson<DepartmentDto[]>("/departments")
}

export function createDepartment(dto: CreateDepartmentDto): Promise<DepartmentDto> {
  return postJson<DepartmentDto>("/departments", dto)
}

export function updateDepartment(departmentId: number, dto: UpdateDepartmentDto): Promise<DepartmentDto> {
  return putJson<DepartmentDto>(`/departments/${departmentId}`, dto)
}

export function deleteDepartment(departmentId: number): Promise<void> {
  return deleteJson(`/departments/${departmentId}`)
}
