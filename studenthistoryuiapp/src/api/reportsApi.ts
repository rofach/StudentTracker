import type {
  ActiveAcademicDifferenceDto,
  InternalTransferJournalItemDto,
  PagedResult,
} from "../types/api"
import { fetchJson } from "./http"

export function getActiveAcademicDifference(params: {
  studentName?: string
  disciplineName?: string
  status?: string
  dateFrom?: string
  dateTo?: string
  page: number
  pageSize: number
}): Promise<PagedResult<ActiveAcademicDifferenceDto>> {
  return fetchJson<PagedResult<ActiveAcademicDifferenceDto>>("/reports/academic-difference/active", undefined, params)
}

export function getInternalTransferJournal(params: {
  studentName?: string
  dateFrom?: string
  dateTo?: string
  onlyWithPendingDifference?: boolean
  page: number
  pageSize: number
}): Promise<PagedResult<InternalTransferJournalItemDto>> {
  return fetchJson<PagedResult<InternalTransferJournalItemDto>>("/reports/internal-transfers", undefined, params)
}
