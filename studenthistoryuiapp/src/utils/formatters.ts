export function formatDate(input: string): string {
  const parts = input.split('-')
  if (parts.length !== 3) return input
  return `${parts[2]}.${parts[1]}.${parts[0]}`
}

export function formatRange(from: string, to: string | null): string {
  if (to === null) return `${formatDate(from)} — дотепер`
  return `${formatDate(from)} — ${formatDate(to)}`
}

export function mapEventTypeLabel(eventType: string): string {
  if (eventType === 'Enrollment') return 'Зарахування до групи'
  if (eventType === 'AcademicLeave') return 'Академічна відпустка'
  if (eventType === 'ExternalTransfer') return 'Зовнішній трансфер'
  return eventType
}
