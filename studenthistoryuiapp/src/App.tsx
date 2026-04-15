import "./styles/tokens.css"
import "./styles/layout.css"
import "./styles/forms.css"
import "./styles/tables.css"
import "./styles/states.css"
import "./styles/pages.css"
import { CURRENT_STUDENT_ID } from "./config"
import { usePathRoute } from "./hooks/usePathRoute"
import { AppShell } from "./layout/AppShell"
import { ADMIN_MENU, STUDENT_MENU, getActiveMenuPath, getAppArea } from "./layout/navigation"
import { parseAdminStudentRoute, parseAdminStudyPlanRoute } from "./layout/routeParsers"
import { AdminDisciplinesPage } from "./pages/admin/AdminDisciplinesPage"
import { AdminGroupsPage } from "./pages/admin/AdminGroupsPage"
import { AdminStructurePage } from "./pages/admin/AdminStructurePage"
import { AdminStudentCreatePage } from "./pages/admin/AdminStudentCreatePage"
import { AdminStudentEditPage } from "./pages/admin/AdminStudentEditPage"
import { AdminStudentOperationsPage } from "./pages/admin/AdminStudentOperationsPage"
import { AdminStudentsPage } from "./pages/admin/AdminStudentsPage"
import { AdminStudentViewPage } from "./pages/admin/AdminStudentViewPage"
import { AdminStudyPlanCreatePage } from "./pages/admin/AdminStudyPlanCreatePage"
import { AdminStudyPlanDetailPage } from "./pages/admin/AdminStudyPlanDetailPage"
import { AdminStudyPlansPage } from "./pages/admin/AdminStudyPlansPage"
import { StudentClassmatesPage } from "./pages/student/StudentClassmatesPage"
import { StudentHistoryPage } from "./pages/student/StudentHistoryPage"
import { StudentOverviewPage } from "./pages/student/StudentOverviewPage"
import { StudentSubjectsPage } from "./pages/student/StudentSubjectsPage"

function renderStudentRoute(path: string) {
  switch (path) {
    case "/student/overview":
      return <StudentOverviewPage studentId={CURRENT_STUDENT_ID} />
    case "/student/subjects":
      return <StudentSubjectsPage studentId={CURRENT_STUDENT_ID} />
    case "/student/classmates":
      return <StudentClassmatesPage studentId={CURRENT_STUDENT_ID} />
    case "/student/history":
      return <StudentHistoryPage studentId={CURRENT_STUDENT_ID} />
    default:
      return <StudentOverviewPage studentId={CURRENT_STUDENT_ID} />
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

export default function App() {
  const { path, navigate } = usePathRoute()
  const area = getAppArea(path)

  const menuItems = area === "admin" ? ADMIN_MENU : STUDENT_MENU
  const activeMenuPath = getActiveMenuPath(path)
  const activeItem = menuItems.find((item) => item.path === activeMenuPath) ?? menuItems[0]
  const content = area === "admin" ? renderAdminRoute(path, navigate) : renderStudentRoute(path)

  return (
    <AppShell
      areaTitle={
        area === "admin"
          ? "\u0410\u0434\u043C\u0456\u043D \u043F\u0430\u043D\u0435\u043B\u044C"
          : "\u041A\u0430\u0431\u0456\u043D\u0435\u0442 \u0441\u0442\u0443\u0434\u0435\u043D\u0442\u0430"
      }
      areaSubtitle={
        area === "admin"
          ? "\u0423\u043F\u0440\u0430\u0432\u043B\u0456\u043D\u043D\u044F \u043D\u0430\u0432\u0447\u0430\u043B\u044C\u043D\u0438\u043C \u043F\u0440\u043E\u0446\u0435\u0441\u043E\u043C"
          : "\u041F\u0435\u0440\u0441\u043E\u043D\u0430\u043B\u044C\u043D\u0438\u0439 \u043F\u0435\u0440\u0435\u0433\u043B\u044F\u0434 \u0434\u0430\u043D\u0438\u0445"
      }
      menuItems={menuItems}
      activePath={activeItem.path}
      onNavigate={navigate}
      topBar={
        <div className="topbar">
          <div>
            <div className="topbar__title">{activeItem.label}</div>
            <div className="topbar__meta">
              {area === "student"
                ? `\u0441\u0442\u0443\u0434\u0435\u043D\u0442 ID ${CURRENT_STUDENT_ID}`
                : "\u0420\u0435\u0436\u0438\u043C \u0430\u0434\u043C\u0456\u043D\u0456\u0441\u0442\u0440\u0430\u0442\u043E\u0440\u0430"}
            </div>
          </div>

          <div className="topbar__actions">
            <button
              type="button"
              className={area === "student" ? "area-switch area-switch--active" : "area-switch"}
              onClick={() => navigate("/student/overview")}
            >
              {"\u0421\u0442\u0443\u0434\u0435\u043D\u0442"}
            </button>
            <button
              type="button"
              className={area === "admin" ? "area-switch area-switch--active" : "area-switch"}
              onClick={() => navigate("/admin/students")}
            >
              {"\u0410\u0434\u043C\u0456\u043D"}
            </button>
          </div>
        </div>
      }
      content={content}
    />
  )
}
