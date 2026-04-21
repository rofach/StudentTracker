import type {
  CreateDisciplineDto,
  DisciplineDto,
  DisciplineSearchItemDto,
  EntityId,
  PagedResult,
  UpdateDisciplineDto,
} from "../types/api"
import { deleteJson, fetchJson, postJson, putJson } from "./http"

export function getDisciplines(): Promise<DisciplineDto[]> {
  return fetchJson<DisciplineDto[]>("/disciplines")
}

export function searchDisciplines(params: {
  name?: string
  page: number
  pageSize: number
}): Promise<PagedResult<DisciplineSearchItemDto>> {
  return fetchJson<PagedResult<DisciplineSearchItemDto>>("/disciplines/search", undefined, params)
}

export function createDiscipline(dto: CreateDisciplineDto): Promise<DisciplineDto> {
  return postJson<DisciplineDto>("/disciplines", dto)
}

export function updateDiscipline(disciplineId: EntityId, dto: UpdateDisciplineDto): Promise<DisciplineDto> {
  return putJson<DisciplineDto>(`/disciplines/${disciplineId}`, dto)
}

export function deleteDiscipline(disciplineId: EntityId): Promise<void> {
  return deleteJson(`/disciplines/${disciplineId}`)
}
