import { useEffect, useState } from 'react'
import { getErrorMessage } from '../api/http'
import { DEFAULT_PAGE_SIZE, getAverageGrade, getGrades, getStudents, getTimeline } from '../api/studentApi'
import type {
  AverageGradeDto,
  AverageGradeFilters,
  GradeDto,
  StudentDto,
  TimelineEventDto,
} from '../types'

const EMPTY_FILTERS: AverageGradeFilters = {
  semesterNo: '',
  disciplineId: '',
  academicYearStart: '',
}

export function useStudentHistoryData() {
  const [students, setStudents] = useState<StudentDto[]>([])
  const [studentsPage, setStudentsPage] = useState(1)
  const [studentsTotalCount, setStudentsTotalCount] = useState(0)
  const [selectedStudentId, setSelectedStudentId] = useState<number | null>(null)

  const [grades, setGrades] = useState<GradeDto[]>([])
  const [gradesPage, setGradesPage] = useState(1)
  const [gradesTotalCount, setGradesTotalCount] = useState(0)
  const [hasLoadedGrades, setHasLoadedGrades] = useState(false)
  const [isLoadingGrades, setIsLoadingGrades] = useState(false)

  const [average, setAverage] = useState<AverageGradeDto | null>(null)
  const [hasLoadedAverage, setHasLoadedAverage] = useState(false)
  const [isLoadingAverage, setIsLoadingAverage] = useState(false)

  const [timelineEvents, setTimelineEvents] = useState<TimelineEventDto[]>([])
  const [timelinePage, setTimelinePage] = useState(1)
  const [timelineTotalCount, setTimelineTotalCount] = useState(0)
  const [hasLoadedTimeline, setHasLoadedTimeline] = useState(false)
  const [isLoadingTimeline, setIsLoadingTimeline] = useState(false)

  const [isLoadingStudents, setIsLoadingStudents] = useState(false)
  const [error, setError] = useState<string | null>(null)
  const [filters, setFilters] = useState<AverageGradeFilters>(EMPTY_FILTERS)

  useEffect(() => {
    let isActive = true

    async function loadStudents() {
      try {
        setIsLoadingStudents(true)
        setError(null)

        const data = await getStudents(studentsPage, DEFAULT_PAGE_SIZE)

        if (!isActive) {
          return
        }

        setStudents(data.items)
        setStudentsTotalCount(data.totalCount)
        setSelectedStudentId((currentStudentId) => {
          if (data.items.length === 0) {
            return null
          }

          const hasSelectedStudent = currentStudentId !== null
            && data.items.some((student) => student.studentId === currentStudentId)

          if (hasSelectedStudent) {
            return currentStudentId
          }

          return data.items[0].studentId
        })
      } catch (error) {
        if (isActive) {
          setError(getErrorMessage(error))
        }
      } finally {
        if (isActive) {
          setIsLoadingStudents(false)
        }
      }
    }

    void loadStudents()

    return () => {
      isActive = false
    }
  }, [studentsPage])

  useEffect(() => {
    if (selectedStudentId === null) {
      return
    }

    const studentId = selectedStudentId
    let isActive = true

    async function loadStudentGrades() {
      try {
        setIsLoadingGrades(true)
        setError(null)

        const data = await getGrades(studentId, gradesPage, DEFAULT_PAGE_SIZE)

        if (!isActive) {
          return
        }

        setGrades(data.items)
        setGradesTotalCount(data.totalCount)
        setHasLoadedGrades(true)
      } catch (error) {
        if (isActive) {
          setError(getErrorMessage(error))
          setGrades([])
          setGradesTotalCount(0)
        }
      } finally {
        if (isActive) {
          setIsLoadingGrades(false)
        }
      }
    }

    void loadStudentGrades()

    return () => {
      isActive = false
    }
  }, [selectedStudentId, gradesPage])

  useEffect(() => {
    if (selectedStudentId === null) {
      return
    }

    const studentId = selectedStudentId
    let isActive = true

    async function loadStudentTimeline() {
      try {
        setIsLoadingTimeline(true)
        setError(null)

        const data = await getTimeline(studentId, timelinePage, DEFAULT_PAGE_SIZE)

        if (!isActive) {
          return
        }

        setTimelineEvents(data.items)
        setTimelineTotalCount(data.totalCount)
        setHasLoadedTimeline(true)
      } catch (error) {
        if (isActive) {
          setError(getErrorMessage(error))
          setTimelineEvents([])
          setTimelineTotalCount(0)
        }
      } finally {
        if (isActive) {
          setIsLoadingTimeline(false)
        }
      }
    }

    void loadStudentTimeline()

    return () => {
      isActive = false
    }
  }, [selectedStudentId, timelinePage])

  useEffect(() => {
    if (selectedStudentId === null) {
      return
    }

    const studentId = selectedStudentId
    let isActive = true

    async function loadStudentAverage() {
      try {
        setIsLoadingAverage(true)
        setError(null)

        const data = await getAverageGrade(studentId, filters)

        if (!isActive) {
          return
        }

        setAverage(data)
        setHasLoadedAverage(true)
      } catch (error) {
        if (isActive) {
          setError(getErrorMessage(error))
          setAverage(null)
        }
      } finally {
        if (isActive) {
          setIsLoadingAverage(false)
        }
      }
    }

    void loadStudentAverage()

    return () => {
      isActive = false
    }
  }, [selectedStudentId])

  function handleStudentChange(studentId: number) {
    setSelectedStudentId(studentId)
    setGradesPage(1)
    setTimelinePage(1)
    setHasLoadedGrades(false)
    setHasLoadedTimeline(false)
    setHasLoadedAverage(false)
    setAverage(null)
  }

  function updateFilter<K extends keyof AverageGradeFilters>(key: K, value: AverageGradeFilters[K]) {
    setFilters((currentFilters) => ({
      ...currentFilters,
      [key]: value,
    }))
  }

  function setSemesterNo(value: string) {
    updateFilter('semesterNo', value)
  }

  function setDisciplineId(value: string) {
    updateFilter('disciplineId', value)
  }

  function setAcademicYearStart(value: string) {
    updateFilter('academicYearStart', value)
  }

  function refreshAverage() {
    if (selectedStudentId === null) {
      return
    }

    void (async () => {
      try {
        setIsLoadingAverage(true)
        setError(null)

        const data = await getAverageGrade(selectedStudentId, filters)

        setAverage(data)
        setHasLoadedAverage(true)
      } catch (error) {
        setError(getErrorMessage(error))
        setAverage(null)
      } finally {
        setIsLoadingAverage(false)
      }
    })()
  }

  function refreshTimeline() {
    if (selectedStudentId === null) {
      return
    }

    void (async () => {
      try {
        setIsLoadingTimeline(true)
        setError(null)

        const data = await getTimeline(selectedStudentId, timelinePage, DEFAULT_PAGE_SIZE)

        setTimelineEvents(data.items)
        setTimelineTotalCount(data.totalCount)
        setHasLoadedTimeline(true)
      } catch (error) {
        setError(getErrorMessage(error))
        setTimelineEvents([])
        setTimelineTotalCount(0)
      } finally {
        setIsLoadingTimeline(false)
      }
    })()
  }

  return {
    pageSize: DEFAULT_PAGE_SIZE,
    students,
    studentsPage,
    studentsTotalCount,
    selectedStudentId,
    isLoadingStudents,
    grades,
    gradesPage,
    gradesTotalCount,
    hasLoadedGrades,
    isLoadingGrades,
    average,
    hasLoadedAverage,
    isLoadingAverage,
    timelineEvents,
    timelinePage,
    timelineTotalCount,
    hasLoadedTimeline,
    isLoadingTimeline,
    error,
    semesterNo: filters.semesterNo,
    disciplineId: filters.disciplineId,
    academicYearStart: filters.academicYearStart,
    setStudentsPage,
    setGradesPage,
    setTimelinePage,
    handleStudentChange,
    setSemesterNo,
    setDisciplineId,
    setAcademicYearStart,
    refreshAverage,
    refreshTimeline,
  }
}
