const eventTypeLabels: Record<string, string> = {
  Enrollment: 'Зарахування до групи',
  AcademicLeave: 'Академічна відпустка',
  ExternalTransfer: 'Зовнішній трансфер',
}

export function formatDate(input: string): string {
  const [year, month, day] = input.split('-')
  const hasValidDateParts = Boolean(year && month && day)

  if (!hasValidDateParts) {
    return input
  }

  return `${day}.${month}.${year}`
}

export function formatRange(from: string, to: string | null): string {
  if (to === null) {
    return `${formatDate(from)} - дотепер`
  }

  return `${formatDate(from)} - ${formatDate(to)}`
}

export function mapEventTypeLabel(eventType: string): string {
  return eventTypeLabels[eventType] ?? eventType
}
