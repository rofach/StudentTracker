import type {
  CreateDisciplineDto,
  DisciplineDto,
  UpdateDisciplineDto,
} from "../types/api"
import { deleteJson, fetchJson, postJson, putJson } from "./http"

export function getDisciplines(): Promise<DisciplineDto[]> {
  return fetchJson<DisciplineDto[]>("/disciplines")
}

export function createDiscipline(dto: CreateDisciplineDto): Promise<DisciplineDto> {
  return postJson<DisciplineDto>("/disciplines", dto)
}

export function updateDiscipline(disciplineId: number, dto: UpdateDisciplineDto): Promise<DisciplineDto> {
  return putJson<DisciplineDto>(`/disciplines/${disciplineId}`, dto)
}

export function deleteDiscipline(disciplineId: number): Promise<void> {
  return deleteJson(`/disciplines/${disciplineId}`)
}
