export function formatDate(value: string | null | undefined): string {
  if (!value) {
    return "—"
  }

  const parsed = new Date(value)
  if (Number.isNaN(parsed.getTime())) {
    return value
  }

  return new Intl.DateTimeFormat("uk-UA").format(parsed)
}

export function formatNullable(value: string | null | undefined): string {
  return value && value.trim() ? value : "—"
}

export function fullName(firstName: string, lastName: string): string {
  return `${lastName} ${firstName}`
}
