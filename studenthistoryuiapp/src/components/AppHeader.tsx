import type { AppPage } from '../types'

type AppHeaderProps = {
  page: AppPage
}

const navigationItems: Array<{ href: string; label: string; page: AppPage }> = [
  { href: '#/', label: 'Огляд', page: 'dashboard' },
  { href: '#/timeline', label: 'Таймлайн', page: 'timeline' },
]

export function AppHeader({ page }: AppHeaderProps) {
  return (
    <header className="hero">
      <div className="hero-top">
        <div>
          <h1>Облік студентів</h1>
        </div>

        <nav className="tabs" aria-label="Навігація сторінками">
          {navigationItems.map((item) => {
            const tabClassName = item.page === page ? 'tab is-active' : 'tab'

            return (
              <a key={item.page} href={item.href} className={tabClassName}>
                {item.label}
              </a>
            )
          })}
        </nav>
      </div>
    </header>
  )
}
