import type {
  ActiveGroupDto,
  AssignGroupPlanDto,
  ChangeGroupPlanDto,
  GroupCompositionMemberDto,
  GroupPlanAssignmentDto,
  GroupStudentDto,
  PagedResult,
} from "../types/api"
import { fetchJson, postJson, putJson } from "./http"

export function getActiveGroups(date?: string): Promise<ActiveGroupDto[]> {
  return fetchJson<ActiveGroupDto[]>("/groups/active", undefined, { date })
}

export function getGroupComposition(
  groupId: number,
  params: { date?: string; page: number; pageSize: number },
): Promise<PagedResult<GroupCompositionMemberDto>> {
  return fetchJson<PagedResult<GroupCompositionMemberDto>>(`/groups/${groupId}/composition`, undefined, params)
}

export function getStudentsInGroup(
  groupId: number,
  params: { date?: string; page: number; pageSize: number },
): Promise<PagedResult<GroupStudentDto>> {
  return fetchJson<PagedResult<GroupStudentDto>>(`/groups/${groupId}/students`, undefined, params)
}

export function getGroupPlanHistory(groupId: number): Promise<GroupPlanAssignmentDto[]> {
  return fetchJson<GroupPlanAssignmentDto[]>(`/groups/${groupId}/plans`)
}

export function assignGroupPlan(groupId: number, dto: AssignGroupPlanDto): Promise<GroupPlanAssignmentDto> {
  return postJson<GroupPlanAssignmentDto>(`/groups/${groupId}/plans`, dto)
}

export function changeCurrentGroupPlan(groupId: number, dto: ChangeGroupPlanDto): Promise<GroupPlanAssignmentDto> {
  return putJson<GroupPlanAssignmentDto>(`/groups/${groupId}/plans/current`, dto)
}
