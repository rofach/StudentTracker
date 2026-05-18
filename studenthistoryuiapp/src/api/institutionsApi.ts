import type { InstitutionDto } from "../types/api"
import { fetchJson, postJson } from "./http"

export function getInstitutions(): Promise<InstitutionDto[]> {
  return fetchJson<InstitutionDto[]>("/institutions")
}

export function createInstitution(institutionName: string): Promise<InstitutionDto> {
  return postJson<InstitutionDto>("/institutions", institutionName)
}
