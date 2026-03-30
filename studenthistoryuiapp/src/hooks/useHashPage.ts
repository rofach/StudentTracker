import { useEffect, useState } from 'react'
import type { AppPage } from '../types'

function resolvePageFromHash(hash: string): AppPage {
  if (hash === '#/timeline') {
    return 'timeline'
  }

  return 'dashboard'
}

export function useHashPage(): AppPage {
  const [page, setPage] = useState<AppPage>(resolvePageFromHash(window.location.hash))

  useEffect(() => {
    function handleHashChange() {
      setPage(resolvePageFromHash(window.location.hash))
    }

    window.addEventListener('hashchange', handleHashChange)

    return () => {
      window.removeEventListener('hashchange', handleHashChange)
    }
  }, [])

  return page
}
