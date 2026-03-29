import type { AppPage } from '../types'

type AppHeaderProps = {
  page: AppPage
}

export function AppHeader({ page }: AppHeaderProps) {
  return (
    <header className="hero">
      <div className="hero-top">
        <div>
          {/*<p className="eyebrow">Student History Dashboard</p>*/}
          <h1>Облік студентів</h1>
        </div>
        <nav className="tabs">
          <a href="#/" className={page === 'dashboard' ? 'tab is-active' : 'tab'}>
            Огляд
          </a>
          <a href="#/timeline" className={page === 'timeline' ? 'tab is-active' : 'tab'}>
            Таймлайн
          </a>
        </nav>
      </div>
    </header>
  )
}
