import type {
  AddPlanDisciplineDto,
  CreateStudyPlanDto,
  PlanDisciplineDto,
  StudyPlanDto,
  UpdatePlanDisciplineDto,
  UpdateStudyPlanDto,
} from "../types/api"
import { deleteJson, fetchJson, postJson, putJson } from "./http"

export function getStudyPlans(): Promise<StudyPlanDto[]> {
  return fetchJson<StudyPlanDto[]>("/studyplans")
}

export function getStudyPlanById(planId: number): Promise<StudyPlanDto> {
  return fetchJson<StudyPlanDto>(`/studyplans/${planId}`)
}

export function createStudyPlan(dto: CreateStudyPlanDto): Promise<StudyPlanDto> {
  return postJson<StudyPlanDto>("/studyplans", dto)
}

export function updateStudyPlan(planId: number, dto: UpdateStudyPlanDto): Promise<StudyPlanDto> {
  return putJson<StudyPlanDto>(`/studyplans/${planId}`, dto)
}

export function deleteStudyPlan(planId: number): Promise<void> {
  return deleteJson(`/studyplans/${planId}`)
}

export function getPlanDisciplines(planId: number): Promise<PlanDisciplineDto[]> {
  return fetchJson<PlanDisciplineDto[]>(`/studyplans/${planId}/disciplines`)
}

export function addPlanDiscipline(planId: number, dto: AddPlanDisciplineDto): Promise<PlanDisciplineDto> {
  return postJson<PlanDisciplineDto>(`/studyplans/${planId}/disciplines`, dto)
}

export function updatePlanDiscipline(
  planId: number,
  disciplineId: number,
  dto: UpdatePlanDisciplineDto,
): Promise<PlanDisciplineDto> {
  return putJson<PlanDisciplineDto>(`/studyplans/${planId}/disciplines/${disciplineId}`, dto)
}

export function deletePlanDiscipline(planId: number, disciplineId: number): Promise<void> {
  return deleteJson(`/studyplans/${planId}/disciplines/${disciplineId}`)
}
