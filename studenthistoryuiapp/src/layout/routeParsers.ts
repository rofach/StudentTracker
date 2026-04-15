export type AdminStudentRoute =
  | { kind: "list" }
  | { kind: "create" }
  | { kind: "view"; studentId: string }
  | { kind: "edit"; studentId: string }
  | { kind: "operations"; studentId: string }

export type AdminStudyPlanRoute =
  | { kind: "list" }
  | { kind: "create" }
  | { kind: "detail"; planId: string }

export function parseAdminStudentRoute(path: string): AdminStudentRoute {
  if (path === "/admin/students") {
    return { kind: "list" }
  }

  if (path === "/admin/students/new") {
    return { kind: "create" }
  }

  const operationsMatch = path.match(/^\/admin\/students\/([^/]+)\/operations$/)
  if (operationsMatch) {
    return { kind: "operations", studentId: operationsMatch[1] }
  }

  const editMatch = path.match(/^\/admin\/students\/([^/]+)\/edit$/)
  if (editMatch) {
    return { kind: "edit", studentId: editMatch[1] }
  }

  const viewMatch = path.match(/^\/admin\/students\/([^/]+)$/)
  if (viewMatch) {
    return { kind: "view", studentId: viewMatch[1] }
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

  const detailMatch = path.match(/^\/admin\/study-plans\/([^/]+)$/)
  if (detailMatch) {
    return { kind: "detail", planId: detailMatch[1] }
  }

  return { kind: "list" }
}
