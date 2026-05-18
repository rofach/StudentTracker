import "./styles/tokens.css"
import "./components/common/FormControls.css"
import "./components/common/PageLayout.css"
import "./components/common/TableView.css"
import "./App.css"
import { PageSectionTitleProvider } from "./components/common/PageHeader"
import { Spinner } from "./components/common/Spinner"
import { StatusState } from "./components/common/StatusState"
import { ToastProvider } from "./components/common/ToastCenter"
import { AuthProvider, useAuth } from "./auth/AuthContext"
import { usePathRoute } from "./hooks/usePathRoute"
import { AppShell } from "./layout/AppShell"
import {
  ADMIN_MENU,
  STUDENT_MENU,
  canAccessPath,
  getActiveMenuPath,
  getAppArea,
  getDefaultPathForRole,
} from "./layout/navigation"
import { parseAdminStudentRoute, parseAdminStudyPlanRoute } from "./layout/routeParsers"
import { AdminDisciplinesPage } from "./pages/admin/AdminDisciplinesPage"
import { AdminGroupsPage } from "./pages/admin/AdminGroupsPage"
import { AdminMovementReportsPage } from "./pages/admin/AdminMovementReportsPage"
import { AdminStructurePage } from "./pages/admin/AdminStructurePage"
import { AdminStudentCreatePage } from "./pages/admin/AdminStudentCreatePage"
import { AdminStudentEditPage } from "./pages/admin/AdminStudentEditPage"
import { AdminStudentOperationsPage } from "./pages/admin/AdminStudentOperationsPage"
import { AdminStudentsPage } from "./pages/admin/AdminStudentsPage"
import { AdminStudentViewPage } from "./pages/admin/AdminStudentViewPage"
import { AdminStudyPlanCreatePage } from "./pages/admin/AdminStudyPlanCreatePage"
import { AdminStudyPlanDetailPage } from "./pages/admin/AdminStudyPlanDetailPage"
import { AdminStudyPlansPage } from "./pages/admin/AdminStudyPlansPage"
import { LoginPage } from "./pages/auth/LoginPage"
import { StudentClassmatesPage } from "./pages/student/StudentClassmatesPage"
import { StudentHistoryPage } from "./pages/student/StudentHistoryPage"
import { StudentOverviewPage } from "./pages/student/StudentOverviewPage"
import { StudentSubjectsPage } from "./pages/student/StudentSubjectsPage"

function renderStudentRoute(path: string, studentId: string) {
  switch (path) {
    case "/student/overview":
      return <StudentOverviewPage studentId={studentId} />
    case "/student/subjects":
      return <StudentSubjectsPage studentId={studentId} />
    case "/student/classmates":
      return <StudentClassmatesPage studentId={studentId} />
    case "/student/history":
      return <StudentHistoryPage studentId={studentId} />
    default:
      return <StudentOverviewPage studentId={studentId} />
  }
}

function renderAdminRoute(path: string, navigate: (path: string) => void) {
  if (path.startsWith("/admin/students")) {
    const route = parseAdminStudentRoute(path)

    switch (route.kind) {
      case "create":
        return <AdminStudentCreatePage navigate={navigate} />
      case "view":
        return <AdminStudentViewPage studentId={route.studentId} navigate={navigate} />
      case "edit":
        return <AdminStudentEditPage studentId={route.studentId} navigate={navigate} />
      case "operations":
        return <AdminStudentOperationsPage studentId={route.studentId} navigate={navigate} />
      case "list":
      default:
        return <AdminStudentsPage navigate={navigate} />
    }
  }

  if (path.startsWith("/admin/study-plans")) {
    const route = parseAdminStudyPlanRoute(path)

    switch (route.kind) {
      case "create":
        return <AdminStudyPlanCreatePage navigate={navigate} />
      case "detail":
        return <AdminStudyPlanDetailPage planId={route.planId} navigate={navigate} />
      case "list":
      default:
        return <AdminStudyPlansPage navigate={navigate} />
    }
  }

  switch (path) {
    case "/admin/movements":
      return <AdminMovementReportsPage />
    case "/admin/groups":
      return <AdminGroupsPage navigate={navigate} />
    case "/admin/disciplines":
      return <AdminDisciplinesPage />
    case "/admin/structure":
      return <AdminStructurePage />
    default:
      return <AdminStudentsPage navigate={navigate} />
  }
}

function AppContent() {
  const { user, isLoading, logout } = useAuth()
  const { path, navigate } = usePathRoute()

  if (isLoading) {
    return <Spinner label="Перевірка сесії..." />
  }

  if (!user) {
    return <LoginPage navigate={navigate} />
  }

  if (!canAccessPath(user.role, path)) {
    const nextPath = getDefaultPathForRole(user.role)
    if (nextPath !== path) {
      navigate(nextPath)
      return <Spinner label="Переадресація..." />
    }
  }

  if (path === "/login") {
    const nextPath = getDefaultPathForRole(user.role)
    navigate(nextPath)
    return <Spinner label="Переадресація..." />
  }

  const area = getAppArea(path)
  const menuItems = area === "admin" ? ADMIN_MENU : STUDENT_MENU
  const activeMenuPath = getActiveMenuPath(path)
  const activeItem = menuItems.find((item) => item.path === activeMenuPath) ?? menuItems[0]
  const content =
    area === "admin"
      ? renderAdminRoute(path, navigate)
      : user.studentId
        ? renderStudentRoute(path, user.studentId)
        : <StatusState tone="error" message="Для студентського акаунта не прив'язано studentId." />

  return (
    <AppShell
      areaTitle={area === "admin" ? "Адмін-панель" : "Кабінет студента"}
      areaSubtitle=""
      menuItems={menuItems}
      activePath={activeItem.path}
      onNavigate={navigate}
      topBar={
        <div className="topbar">
          <div>
            <div className="topbar__title">{activeItem.label}</div>
            <div className="topbar__meta">
              {area === "student"
                ? user.email ?? user.userName
                : `${user.userName}${user.email ? ` • ${user.email}` : ""}`}
            </div>
          </div>

          <div className="topbar__actions">
            <button
              type="button"
              className="area-switch"
              onClick={() => {
                logout()
                navigate("/login")
              }}
            >
              Вийти
            </button>
          </div>
        </div>
      }
      content={<PageSectionTitleProvider title={activeItem.label}>{content}</PageSectionTitleProvider>}
    />
  )
}

export default function App() {
  return (
    <ToastProvider>
      <AuthProvider>
        <AppContent />
      </AuthProvider>
    </ToastProvider>
  )
}
