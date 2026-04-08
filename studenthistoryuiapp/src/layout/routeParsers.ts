export type AdminStudentRoute =
  | { kind: "list" }
  | { kind: "create" }
  | { kind: "view"; studentId: number }
  | { kind: "edit"; studentId: number }
  | { kind: "operations"; studentId: number }

export type AdminStudyPlanRoute =
  | { kind: "list" }
  | { kind: "create" }
  | { kind: "detail"; planId: number }

export function parseAdminStudentRoute(path: string): AdminStudentRoute {
  if (path === "/admin/students") {
    return { kind: "list" }
  }

  if (path === "/admin/students/new") {
    return { kind: "create" }
  }

  const operationsMatch = path.match(/^\/admin\/students\/(\d+)\/operations$/)
  if (operationsMatch) {
    return { kind: "operations", studentId: Number(operationsMatch[1]) }
  }

  const editMatch = path.match(/^\/admin\/students\/(\d+)\/edit$/)
  if (editMatch) {
    return { kind: "edit", studentId: Number(editMatch[1]) }
  }

  const viewMatch = path.match(/^\/admin\/students\/(\d+)$/)
  if (viewMatch) {
    return { kind: "view", studentId: Number(viewMatch[1]) }
  }

  return { kind: "list" }
}

export function parseAdminStudyPlanRoute(path: string): AdminStudyPlanRoute {
  if (path === "/admin/study-plans") {
    return { kind: "list" }
  }

  if (path === "/admin/study-plans/new") {
    return { kind: "create" }
  }

  const detailMatch = path.match(/^\/admin\/study-plans\/(\d+)$/)
  if (detailMatch) {
    return { kind: "detail", planId: Number(detailMatch[1]) }
  }

  return { kind: "list" }
}
