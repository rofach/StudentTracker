const statusLabels: Record<string, string> = {
  Active: "Активний",
  OnLeave: "Академвідпустка",
  Expelled: "Відрахований",
  Graduated: "Випускник",
}

export function formatStudentStatus(status: string): string {
  return statusLabels[status] ?? status
}
