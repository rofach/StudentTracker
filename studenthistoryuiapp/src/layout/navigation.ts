import type { AppArea, AppRoute, NavItem } from "../types/navigation"

export const STUDENT_MENU: NavItem[] = [
  { path: "/student/overview", label: "Огляд" },
  { path: "/student/subjects", label: "Предмети" },
  { path: "/student/classmates", label: "Одногрупники" },
  { path: "/student/history", label: "Історія" },
]

export const ADMIN_MENU: NavItem[] = [
  { path: "/admin/students", label: "Студенти" },
  { path: "/admin/groups", label: "Групи" },
  { path: "/admin/study-plans", label: "Навчальні плани" },
  { path: "/admin/disciplines", label: "Предмети" },
  { path: "/admin/structure", label: "Структура" },
]

export function normalizePath(pathname: string): string {
  if (!pathname || pathname === "/") {
    return "/student/overview"
  }

  if (
    pathname === "/student/overview" ||
    pathname === "/student/subjects" ||
    pathname === "/student/classmates" ||
    pathname === "/student/history"
  ) {
    return pathname
  }

  if (pathname === "/admin/students" || pathname === "/admin/students/new") {
    return pathname
  }

  if (/^\/admin\/students\/\d+$/.test(pathname)) {
    return pathname
  }

  if (/^\/admin\/students\/\d+\/edit$/.test(pathname)) {
    return pathname
  }

  if (/^\/admin\/students\/\d+\/operations$/.test(pathname)) {
    return pathname
  }

  if (pathname === "/admin/groups") {
    return pathname
  }

  if (pathname === "/admin/study-plans" || pathname === "/admin/study-plans/new") {
    return pathname
  }

  if (/^\/admin\/study-plans\/\d+$/.test(pathname)) {
    return pathname
  }

  if (pathname === "/admin/disciplines") {
    return pathname
  }

  if (pathname === "/admin/structure") {
    return pathname
  }

  return "/student/overview"
}

export function getAppArea(pathname: string): AppArea {
  return pathname.startsWith("/admin/") ? "admin" : "student"
}

export function isKnownRoute(route: string): route is AppRoute["path"] {
  const knownRoutes = [...STUDENT_MENU, ...ADMIN_MENU].map((item) => item.path)
  return knownRoutes.includes(route)
}

export function getActiveMenuPath(pathname: string): string {
  if (pathname.startsWith("/admin/students")) {
    return "/admin/students"
  }

  if (pathname.startsWith("/admin/groups")) {
    return "/admin/groups"
  }

  if (pathname.startsWith("/admin/study-plans")) {
    return "/admin/study-plans"
  }

  if (pathname.startsWith("/admin/disciplines")) {
    return "/admin/disciplines"
  }

  if (pathname.startsWith("/admin/structure")) {
    return "/admin/structure"
  }

  if (pathname.startsWith("/student/subjects")) {
    return "/student/subjects"
  }

  if (pathname.startsWith("/student/classmates")) {
    return "/student/classmates"
  }

  if (pathname.startsWith("/student/history")) {
    return "/student/history"
  }

  return "/student/overview"
}
