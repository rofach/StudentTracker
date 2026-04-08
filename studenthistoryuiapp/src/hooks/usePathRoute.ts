import { useEffect, useState } from "react"
import { normalizePath } from "../layout/navigation"

type UsePathRouteResult = {
  path: string
  navigate: (nextPath: string) => void
}

export function usePathRoute(): UsePathRouteResult {
  const [path, setPath] = useState<string>(() => normalizePath(window.location.pathname))

  useEffect(() => {
    const normalized = normalizePath(window.location.pathname)
    if (normalized !== window.location.pathname) {
      window.history.replaceState({}, "", normalized)
      setPath(normalized)
    }

    const onPopState = (): void => {
      setPath(normalizePath(window.location.pathname))
    }

    window.addEventListener("popstate", onPopState)
    return () => window.removeEventListener("popstate", onPopState)
  }, [])

  const navigate = (nextPath: string): void => {
    const normalized = normalizePath(nextPath)
    if (normalized === path) {
      return
    }

    window.history.pushState({}, "", normalized)
    setPath(normalized)
  }

  return { path, navigate }
}
