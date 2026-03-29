import { type FormEvent, useEffect, useState } from 'react'
import './App.css'
import { AppHeader } from './components/AppHeader'
import { DashboardPage } from './components/DashboardPage'
import { TimelinePage } from './components/TimelinePage'
import type {
  AppPage,
  AverageGradeDto,
  GradeDto,
  PagedResult,
  StudentDto,
  TimelineEventDto,
} from './types'

const DEFAULT_PAGE_SIZE = 20

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
  const [studentsPage, setStudentsPage] = useState(1)
  const [studentsTotalCount, setStudentsTotalCount] = useState(0)
  const [selectedStudentId, setSelectedStudentId] = useState<number | null>(null)

  const [grades, setGrades] = useState<GradeDto[]>([])
  const [gradesPage, setGradesPage] = useState(1)
  const [gradesTotalCount, setGradesTotalCount] = useState(0)

  const [average, setAverage] = useState<AverageGradeDto | null>(null)

  const [timelineEvents, setTimelineEvents] = useState<TimelineEventDto[]>([])
  const [timelinePage, setTimelinePage] = useState(1)
  const [timelineTotalCount, setTimelineTotalCount] = useState(0)

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
        setError(null)

        const data = await fetchJson<PagedResult<StudentDto>>(
          `/api/students?page=${studentsPage}&pageSize=${DEFAULT_PAGE_SIZE}`,
        )

        if (cancelled) return

        setStudents(data.items)
        setStudentsTotalCount(data.totalCount)
        setSelectedStudentId((currentStudentId) => {
          if (data.items.length === 0) return null
          if (currentStudentId !== null && data.items.some((student) => student.studentId === currentStudentId)) {
            return currentStudentId
          }
          return data.items[0].studentId
        })
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
  }, [studentsPage])

  const loadGrades = async (studentId: number, currentPage: number) => {
    try {
      setIsLoadingGrades(true)
      setError(null)

      const data = await fetchJson<PagedResult<GradeDto>>(
        `/api/students/${studentId}/grades?page=${currentPage}&pageSize=${DEFAULT_PAGE_SIZE}`,
      )

      setGrades(data.items)
      setGradesTotalCount(data.totalCount)
      setHasLoadedGrades(true)
    } catch (e) {
      setError((e as Error).message)
      setGrades([])
      setGradesTotalCount(0)
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

  const loadTimeline = async (studentId: number, currentPage: number) => {
    try {
      setIsLoadingTimeline(true)
      setError(null)

      const data = await fetchJson<PagedResult<TimelineEventDto>>(
        `/api/students/${studentId}/timeline?page=${currentPage}&pageSize=${DEFAULT_PAGE_SIZE}`,
      )

      setTimelineEvents(data.items)
      setTimelineTotalCount(data.totalCount)
      setHasLoadedTimeline(true)
    } catch (e) {
      setError((e as Error).message)
      setTimelineEvents([])
      setTimelineTotalCount(0)
    } finally {
      setIsLoadingTimeline(false)
    }
  }

  useEffect(() => {
    if (selectedStudentId === null) return
    void loadGrades(selectedStudentId, gradesPage)
  }, [selectedStudentId, gradesPage])

  useEffect(() => {
    if (selectedStudentId === null) return
    void loadTimeline(selectedStudentId, timelinePage)
  }, [selectedStudentId, timelinePage])

  useEffect(() => {
    if (selectedStudentId === null) return
    void loadAverage(selectedStudentId)
  }, [selectedStudentId])

  const handleAverageSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault()
    if (selectedStudentId !== null) {
      void loadAverage(selectedStudentId)
    }
  }

  const handleStudentChange = (studentId: number) => {
    setSelectedStudentId(studentId)
    setGradesPage(1)
    setTimelinePage(1)
    setHasLoadedGrades(false)
    setHasLoadedTimeline(false)
  }

  const handleRefreshTimeline = () => {
    if (selectedStudentId !== null) {
      void loadTimeline(selectedStudentId, timelinePage)
    }
  }

  return (
    <div className="page">
      <AppHeader page={page} />

      {page === 'dashboard' ? (
        <DashboardPage
          students={students}
          studentsPage={studentsPage}
          studentsPageSize={DEFAULT_PAGE_SIZE}
          studentsTotalCount={studentsTotalCount}
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
          gradesPage={gradesPage}
          gradesPageSize={DEFAULT_PAGE_SIZE}
          gradesTotalCount={gradesTotalCount}
          hasLoadedGrades={hasLoadedGrades}
          error={error}
          onStudentChange={handleStudentChange}
          onStudentsPageChange={setStudentsPage}
          onSemesterNoChange={setSemesterNo}
          onDisciplineIdChange={setDisciplineId}
          onAcademicYearStartChange={setAcademicYearStart}
          onAverageSubmit={handleAverageSubmit}
          onGradesPageChange={setGradesPage}
        />
      ) : (
        <TimelinePage
          students={students}
          studentsPage={studentsPage}
          studentsPageSize={DEFAULT_PAGE_SIZE}
          studentsTotalCount={studentsTotalCount}
          selectedStudentId={selectedStudentId}
          isLoadingStudents={isLoadingStudents}
          isLoadingTimeline={isLoadingTimeline}
          hasLoadedTimeline={hasLoadedTimeline}
          timelineEvents={timelineEvents}
          timelinePage={timelinePage}
          timelinePageSize={DEFAULT_PAGE_SIZE}
          timelineTotalCount={timelineTotalCount}
          onStudentChange={handleStudentChange}
          onStudentsPageChange={setStudentsPage}
          onRefreshTimeline={handleRefreshTimeline}
          onTimelinePageChange={setTimelinePage}
        />
      )}
    </div>
  )
}

export default App
