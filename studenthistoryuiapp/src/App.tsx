import { type FormEvent, useEffect, useState } from 'react'
import './App.css'
import { AppHeader } from './components/AppHeader'
import { DashboardPage } from './components/DashboardPage'
import { TimelinePage } from './components/TimelinePage'
import type {
  AppPage,
  AverageGradeDto,
  GradeDto,
  StudentDto,
  TimelineEventDto,
} from './types'

function getPageFromHash(hash: string): AppPage {
  if (hash === '#/timeline') return 'timeline'
  return 'dashboard'
}

async function fetchJson<T>(url: string): Promise<T> {
  const response = await fetch(url)
  if (!response.ok) {
    throw new Error(`Запит до ${url} завершився помилкою (${response.status})`)
  }

  return (await response.json()) as T
}

function App() {
  const [page, setPage] = useState<AppPage>(getPageFromHash(window.location.hash))
  const [students, setStudents] = useState<StudentDto[]>([])
  const [selectedStudentId, setSelectedStudentId] = useState<number | null>(null)
  const [grades, setGrades] = useState<GradeDto[]>([])
  const [average, setAverage] = useState<AverageGradeDto | null>(null)
  const [timelineEvents, setTimelineEvents] = useState<TimelineEventDto[]>([])
  const [hasLoadedGrades, setHasLoadedGrades] = useState(false)
  const [hasLoadedAverage, setHasLoadedAverage] = useState(false)
  const [hasLoadedTimeline, setHasLoadedTimeline] = useState(false)
  const [isLoadingStudents, setIsLoadingStudents] = useState(false)
  const [isLoadingGrades, setIsLoadingGrades] = useState(false)
  const [isLoadingAverage, setIsLoadingAverage] = useState(false)
  const [isLoadingTimeline, setIsLoadingTimeline] = useState(false)
  const [error, setError] = useState<string | null>(null)

  const [semesterNo, setSemesterNo] = useState('')
  const [disciplineId, setDisciplineId] = useState('')
  const [academicYearStart, setAcademicYearStart] = useState('')

  useEffect(() => {
    const onHashChange = () => setPage(getPageFromHash(window.location.hash))
    window.addEventListener('hashchange', onHashChange)
    return () => window.removeEventListener('hashchange', onHashChange)
  }, [])

  useEffect(() => {
    let cancelled = false

    const loadStudents = async () => {
      try {
        setIsLoadingStudents(true)
        const data = await fetchJson<StudentDto[]>('/api/students')
        if (cancelled) return
        setStudents(data)
        if (data.length > 0) {
          setSelectedStudentId(data[0].studentId)
        }
      } catch (e) {
        if (!cancelled) setError((e as Error).message)
      } finally {
        if (!cancelled) setIsLoadingStudents(false)
      }
    }

    void loadStudents()
    return () => {
      cancelled = true
    }
  }, [])

  const loadGrades = async (studentId: number) => {
    try {
      setIsLoadingGrades(true)
      const data = await fetchJson<GradeDto[]>(`/api/students/${studentId}/grades`)
      setGrades(data)
      setHasLoadedGrades(true)
    } catch (e) {
      setError((e as Error).message)
    } finally {
      setIsLoadingGrades(false)
    }
  }

  const loadAverage = async (studentId: number) => {
    try {
      setIsLoadingAverage(true)
      setError(null)

      const params = new URLSearchParams()
      if (semesterNo.trim()) params.set('semesterNo', semesterNo.trim())
      if (disciplineId.trim()) params.set('disciplineId', disciplineId.trim())
      if (academicYearStart.trim()) params.set('academicYearStart', academicYearStart.trim())

      const queryString = params.toString()
      const url = queryString
        ? `/api/students/${studentId}/grades/average?${queryString}`
        : `/api/students/${studentId}/grades/average`

      const data = await fetchJson<AverageGradeDto>(url)
      setAverage(data)
      setHasLoadedAverage(true)
    } catch (e) {
      setError((e as Error).message)
      setAverage(null)
    } finally {
      setIsLoadingAverage(false)
    }
  }

  const loadTimeline = async (studentId: number) => {
    try {
      setIsLoadingTimeline(true)
      const data = await fetchJson<TimelineEventDto[]>(`/api/students/${studentId}/timeline`)
      setTimelineEvents(data)
      setHasLoadedTimeline(true)
    } catch (e) {
      setError((e as Error).message)
      setTimelineEvents([])
    } finally {
      setIsLoadingTimeline(false)
    }
  }

  useEffect(() => {
    if (selectedStudentId === null) return
    void loadGrades(selectedStudentId)
    void loadAverage(selectedStudentId)
    void loadTimeline(selectedStudentId)
  }, [selectedStudentId])

  const handleAverageSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    if (selectedStudentId !== null) {
      void loadAverage(selectedStudentId)
    }
  }

  const handleStudentChange = (studentId: number) => {
    setSelectedStudentId(studentId)
  }

  const handleRefreshTimeline = () => {
    if (selectedStudentId !== null) {
      void loadTimeline(selectedStudentId)
    }
  }

  return (
    <div className="page">
      <AppHeader page={page} />

      {page === 'dashboard' ? (
        <DashboardPage
          students={students}
          selectedStudentId={selectedStudentId}
          isLoadingStudents={isLoadingStudents}
          semesterNo={semesterNo}
          disciplineId={disciplineId}
          academicYearStart={academicYearStart}
          isLoadingAverage={isLoadingAverage}
          isLoadingGrades={isLoadingGrades}
          average={average}
          hasLoadedAverage={hasLoadedAverage}
          grades={grades}
          hasLoadedGrades={hasLoadedGrades}
          error={error}
          onStudentChange={handleStudentChange}
          onSemesterNoChange={setSemesterNo}
          onDisciplineIdChange={setDisciplineId}
          onAcademicYearStartChange={setAcademicYearStart}
          onAverageSubmit={handleAverageSubmit}
        />
      ) : (
        <TimelinePage
          students={students}
          selectedStudentId={selectedStudentId}
          isLoadingStudents={isLoadingStudents}
          isLoadingTimeline={isLoadingTimeline}
          hasLoadedTimeline={hasLoadedTimeline}
          timelineEvents={timelineEvents}
          onStudentChange={handleStudentChange}
          onRefreshTimeline={handleRefreshTimeline}
        />
      )}
    </div>
  )
}

export default App
