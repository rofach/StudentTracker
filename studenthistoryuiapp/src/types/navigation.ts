export type AppArea = "student" | "admin"

export type NavItem = {
  path: string
  label: string
}

export type AppRoute = {
  area: AppArea
  path: string
}
