import type { ReactNode } from "react"
import type { NavItem } from "../types/navigation"

type AppShellProps = {
  areaTitle: string
  areaSubtitle: string
  menuItems: NavItem[]
  activePath: string
  onNavigate: (path: string) => void
  topBar: ReactNode
  content: ReactNode
}

export function AppShell({
  areaTitle,
  areaSubtitle,
  menuItems,
  activePath,
  onNavigate,
  topBar,
  content,
}: AppShellProps) {
  return (
    <div className="app-shell">
      <aside className="sidebar">
        <div className="sidebar__brand">
          <h1>{areaTitle}</h1>
          <p>{areaSubtitle}</p>
        </div>

        <nav
          className="sidebar__menu"
          aria-label="\u0413\u043E\u043B\u043E\u0432\u043D\u0435 \u043C\u0435\u043D\u044E"
        >
          {menuItems.map((item) => (
            <button
              key={item.path}
              type="button"
              className={`menu-item ${activePath === item.path ? "menu-item--active" : ""}`}
              onClick={() => onNavigate(item.path)}
            >
              {item.label}
            </button>
          ))}
        </nav>
      </aside>

      <main className="workspace">
        <header className="workspace__topbar">{topBar}</header>
        <section className="workspace__content">{content}</section>
      </main>
    </div>
  )
}
