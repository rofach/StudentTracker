import "./PageHeader.css"
import { createContext, useContext, type ReactNode } from "react"

const PageSectionTitleContext = createContext<string | null>(null)

type PageHeaderProps = {
  title: string
  description?: string
  actions?: ReactNode
}

type PageSectionTitleProviderProps = {
  title: string
  children: ReactNode
}

export function PageSectionTitleProvider({ title, children }: PageSectionTitleProviderProps) {
  return <PageSectionTitleContext.Provider value={title}>{children}</PageSectionTitleContext.Provider>
}

export function PageHeader({ title, description, actions }: PageHeaderProps) {
  const sectionTitle = useContext(PageSectionTitleContext)
  const showTitle = sectionTitle?.trim() !== title.trim()

  if (!showTitle && !description && !actions) {
    return null
  }

  return (
    <div className="page-header">
      <div className="page-header__content">
        {showTitle ? <h1 className="page-header__title">{title}</h1> : null}
        {description ? <p className="page-header__description">{description}</p> : null}
      </div>

      {actions ? <div className="page-header__actions">{actions}</div> : null}
    </div>
  )
}
