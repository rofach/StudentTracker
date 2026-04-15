import type { AppArea, AppRoute, NavItem } from "../types/navigation"

export const STUDENT_MENU: NavItem[] = [
  { path: "/student/overview", label: "\u041E\u0433\u043B\u044F\u0434" },
  { path: "/student/subjects", label: "\u041F\u0440\u0435\u0434\u043C\u0435\u0442\u0438" },
  { path: "/student/classmates", label: "\u041E\u0434\u043D\u043E\u0433\u0440\u0443\u043F\u043D\u0438\u043A\u0438" },
  { path: "/student/history", label: "\u0406\u0441\u0442\u043E\u0440\u0456\u044F" },
]

export const ADMIN_MENU: NavItem[] = [
  { path: "/admin/students", label: "\u0421\u0442\u0443\u0434\u0435\u043D\u0442\u0438" },
  { path: "/admin/groups", label: "\u0413\u0440\u0443\u043F\u0438" },
  { path: "/admin/study-plans", label: "\u041D\u0430\u0432\u0447\u0430\u043B\u044C\u043D\u0456 \u043F\u043B\u0430\u043D\u0438" },
  { path: "/admin/disciplines", label: "\u041F\u0440\u0435\u0434\u043C\u0435\u0442\u0438" },
  { path: "/admin/structure", label: "\u0421\u0442\u0440\u0443\u043A\u0442\u0443\u0440\u0430" },
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
