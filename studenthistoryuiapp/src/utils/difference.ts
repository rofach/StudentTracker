import type { DifferenceItemStatus } from "../types/api"

const differenceStatusLabels: Record<DifferenceItemStatus, string> = {
  Pending: "Очікує",
  Completed: "Завершено",
  Waived: "Зараховано",
}

export function formatDifferenceStatus(status: DifferenceItemStatus): string {
  return differenceStatusLabels[status]
}

export function formatDifferenceSummary(pending: number, total: number): string {
  return `${pending}/${total} очікує`
}

export function formatDifferenceBreakdown(
  pending: number,
  completed: number,
  waived: number,
): string {
  return `очікує: ${pending}, завершено: ${completed}, зараховано: ${waived}`
}
