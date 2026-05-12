export function formatDate(value: string | null | undefined): string {
  if (!value) {
    return "—"
  }

  // ISO date-only strings ("YYYY-MM-DD") are parsed by Date as UTC midnight,
  // which causes a timezone shift in UTC+ locales (e.g. "2026-05-20" → "19.05.2026").
  // Parse the parts manually to treat the value as a local date.
  const isoDateOnly = /^(\d{4})-(\d{2})-(\d{2})$/.exec(value)
  if (isoDateOnly) {
    const [, y, m, d] = isoDateOnly
    const local = new Date(Number(y), Number(m) - 1, Number(d))
    return new Intl.DateTimeFormat("uk-UA").format(local)
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

export function fullName(firstName: string, lastName: string, patronymic?: string | null): string {
  return [lastName, firstName, patronymic].filter((value) => value && value.trim()).join(" ")
}
