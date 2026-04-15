import type {
  ActiveGroupDto,
  AssignGroupPlanDto,
  ChangeGroupPlanDto,
  EntityId,
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
  groupId: EntityId,
  params: { date?: string; page: number; pageSize: number },
): Promise<PagedResult<GroupCompositionMemberDto>> {
  return fetchJson<PagedResult<GroupCompositionMemberDto>>(`/groups/${groupId}/composition`, undefined, params)
}

export function getStudentsInGroup(
  groupId: EntityId,
  params: { date?: string; page: number; pageSize: number },
): Promise<PagedResult<GroupStudentDto>> {
  return fetchJson<PagedResult<GroupStudentDto>>(`/groups/${groupId}/students`, undefined, params)
}

export function getGroupPlanHistory(groupId: EntityId): Promise<GroupPlanAssignmentDto[]> {
  return fetchJson<GroupPlanAssignmentDto[]>(`/groups/${groupId}/plans`)
}

export function assignGroupPlan(groupId: EntityId, dto: AssignGroupPlanDto): Promise<GroupPlanAssignmentDto> {
  return postJson<GroupPlanAssignmentDto>(`/groups/${groupId}/plans`, dto)
}

export function changeCurrentGroupPlan(groupId: EntityId, dto: ChangeGroupPlanDto): Promise<GroupPlanAssignmentDto> {
  return putJson<GroupPlanAssignmentDto>(`/groups/${groupId}/plans/current`, dto)
}
