import type {
  ActiveGroupDto,
  GroupCompositionMemberDto,
  GroupStudentDto,
  PagedResult,
} from "../types/api"
import { fetchJson } from "./http"

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
