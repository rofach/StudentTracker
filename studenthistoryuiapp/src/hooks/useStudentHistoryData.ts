import { useEffect, useMemo, useRef, useState } from 'react'
import { getErrorMessage } from '../api/http'
import {
  DEFAULT_PAGE_SIZE,
  getAverageGrade,
  getGrades,
  getStudentDisciplines,
  getStudents,
  getTimeline,
} from '../api/studentApi'
import type {
  AsyncCollectionState,
  AverageGradeFilters,
  AverageSummaryState,
  GradeDto,
  PaginationState,
  StudentDisciplineOptionDto,
  StudentDto,
  StudentSelectionState,
  TimelineEventDto,
} from '../types'

const emptyAverageFilters: AverageGradeFilters = {
  semesterNo: '',
  disciplineId: '',
  academicYearStart: '',
}

function buildPaginationState(currentPage: number, totalCount: number): PaginationState {
  return {
    currentPage,
    pageSize: DEFAULT_PAGE_SIZE,
    totalCount,
  }
}

function getNextSelectedStudentId(
  students: StudentDto[],
  currentStudentId: number | null,
): number | null {
  if (students.length === 0) {
    return null
  }

  const hasCurrentStudent =
    currentStudentId !== null && students.some((student) => student.studentId === currentStudentId)

  if (hasCurrentStudent) {
    return currentStudentId
  }

  return students[0].studentId
}

function getSemesterOptions(disciplineOptions: StudentDisciplineOptionDto[]): number[] {
  return Array.from(new Set(disciplineOptions.map((discipline) => discipline.semesterNo))).sort(
    (left, right) => left - right,
  )
}

function getVisibleDisciplineOptions(
  disciplineOptions: StudentDisciplineOptionDto[],
  semesterNo: string,
): StudentDisciplineOptionDto[] {
  const filteredDisciplines = semesterNo
    ? disciplineOptions.filter((discipline) => discipline.semesterNo === Number(semesterNo))
    : disciplineOptions

  const disciplineMap = new Map<number, StudentDisciplineOptionDto>()

  filteredDisciplines.forEach((discipline) => {
    const existingDiscipline = disciplineMap.get(discipline.disciplineId)

    if (!existingDiscipline) {
      disciplineMap.set(discipline.disciplineId, discipline)
      return
    }

    if (!existingDiscipline.hasGrade && discipline.hasGrade) {
      disciplineMap.set(discipline.disciplineId, {
        ...existingDiscipline,
        hasGrade: true,
      })
    }
  })

  return Array.from(disciplineMap.values()).sort((left, right) =>
    left.disciplineName.localeCompare(right.disciplineName),
  )
}

export function useStudentHistoryData() {
  const previousSelectedStudentIdRef = useRef<number | null>(null)

  const [students, setStudents] = useState<StudentDto[]>([])
  const [studentsPage, setStudentsPage] = useState(1)
  const [studentsTotalCount, setStudentsTotalCount] = useState(0)
  const [selectedStudentId, setSelectedStudentId] = useState<number | null>(null)

  const [grades, setGrades] = useState<GradeDto[]>([])
  const [gradesPage, setGradesPage] = useState(1)
  const [gradesTotalCount, setGradesTotalCount] = useState(0)
  const [hasLoadedGrades, setHasLoadedGrades] = useState(false)
  const [isLoadingGrades, setIsLoadingGrades] = useState(false)

  const [averageData, setAverageData] = useState<AverageSummaryState['data']>(null)
  const [hasLoadedAverage, setHasLoadedAverage] = useState(false)
  const [isLoadingAverage, setIsLoadingAverage] = useState(false)

  const [timelineEvents, setTimelineEvents] = useState<TimelineEventDto[]>([])
  const [timelinePage, setTimelinePage] = useState(1)
  const [timelineTotalCount, setTimelineTotalCount] = useState(0)
  const [hasLoadedTimeline, setHasLoadedTimeline] = useState(false)
  const [isLoadingTimeline, setIsLoadingTimeline] = useState(false)

  const [disciplineOptions, setDisciplineOptions] = useState<StudentDisciplineOptionDto[]>([])
  const [isLoadingDisciplineOptions, setIsLoadingDisciplineOptions] = useState(false)

  const [isLoadingStudents, setIsLoadingStudents] = useState(false)
  const [errorMessage, setErrorMessage] = useState<string | null>(null)
  const [averageFilters, setAverageFilters] = useState<AverageGradeFilters>(emptyAverageFilters)

  function handleRequestError(error: unknown) {
    setErrorMessage(getErrorMessage(error))
  }

  function resetStudentDetails() {
    setGrades([])
    setGradesTotalCount(0)
    setHasLoadedGrades(false)
    setIsLoadingGrades(false)
    setAverageData(null)
    setHasLoadedAverage(false)
    setIsLoadingAverage(false)
    setTimelineEvents([])
    setTimelineTotalCount(0)
    setHasLoadedTimeline(false)
    setIsLoadingTimeline(false)
    setDisciplineOptions([])
    setIsLoadingDisciplineOptions(false)
    setAverageFilters(emptyAverageFilters)
  }

  async function loadStudents(shouldApplyUpdates: () => boolean = () => true) {
    try {
      setIsLoadingStudents(true)
      setErrorMessage(null)

      const studentsResult = await getStudents(studentsPage, DEFAULT_PAGE_SIZE)

      if (!shouldApplyUpdates()) {
        return
      }

      setStudents(studentsResult.items)
      setStudentsTotalCount(studentsResult.totalCount)
      setSelectedStudentId((currentStudentId) =>
        getNextSelectedStudentId(studentsResult.items, currentStudentId),
      )
    } catch (error) {
      if (!shouldApplyUpdates()) {
        return
      }

      handleRequestError(error)
    } finally {
      if (shouldApplyUpdates()) {
        setIsLoadingStudents(false)
      }
    }
  }

  async function loadGradesForStudent(
    studentId: number,
    page: number,
    shouldApplyUpdates: () => boolean = () => true,
  ) {
    try {
      setIsLoadingGrades(true)
      setErrorMessage(null)

      const gradesResult = await getGrades(studentId, page, DEFAULT_PAGE_SIZE)

      if (!shouldApplyUpdates()) {
        return
      }

      setGrades(gradesResult.items)
      setGradesTotalCount(gradesResult.totalCount)
      setHasLoadedGrades(true)
    } catch (error) {
      if (!shouldApplyUpdates()) {
        return
      }

      handleRequestError(error)
      setGrades([])
      setGradesTotalCount(0)
    } finally {
      if (shouldApplyUpdates()) {
        setIsLoadingGrades(false)
      }
    }
  }

  async function loadTimelineForStudent(
    studentId: number,
    page: number,
    shouldApplyUpdates: () => boolean = () => true,
  ) {
    try {
      setIsLoadingTimeline(true)
      setErrorMessage(null)

      const timelineResult = await getTimeline(studentId, page, DEFAULT_PAGE_SIZE)

      if (!shouldApplyUpdates()) {
        return
      }

      setTimelineEvents(timelineResult.items)
      setTimelineTotalCount(timelineResult.totalCount)
      setHasLoadedTimeline(true)
    } catch (error) {
      if (!shouldApplyUpdates()) {
        return
      }

      handleRequestError(error)
      setTimelineEvents([])
      setTimelineTotalCount(0)
    } finally {
      if (shouldApplyUpdates()) {
        setIsLoadingTimeline(false)
      }
    }
  }

  async function loadAverageForStudent(
    studentId: number,
    filters: AverageGradeFilters,
    shouldApplyUpdates: () => boolean = () => true,
  ) {
    try {
      setIsLoadingAverage(true)
      setErrorMessage(null)

      const averageResult = await getAverageGrade(studentId, filters)

      if (!shouldApplyUpdates()) {
        return
      }

      setAverageData(averageResult)
      setHasLoadedAverage(true)
    } catch (error) {
      if (!shouldApplyUpdates()) {
        return
      }

      handleRequestError(error)
      setAverageData(null)
    } finally {
      if (shouldApplyUpdates()) {
        setIsLoadingAverage(false)
      }
    }
  }

  async function loadDisciplineOptionsForStudent(
    studentId: number,
    shouldApplyUpdates: () => boolean = () => true,
  ) {
    try {
      setIsLoadingDisciplineOptions(true)
      setErrorMessage(null)

      const nextDisciplineOptions = await getStudentDisciplines(studentId)

      if (!shouldApplyUpdates()) {
        return
      }

      setDisciplineOptions(nextDisciplineOptions)
    } catch (error) {
      if (!shouldApplyUpdates()) {
        return
      }

      handleRequestError(error)
      setDisciplineOptions([])
    } finally {
      if (shouldApplyUpdates()) {
        setIsLoadingDisciplineOptions(false)
      }
    }
  }

  useEffect(() => {
    let isActive = true

    void loadStudents(() => isActive)

    return () => {
      isActive = false
    }
  }, [studentsPage])

  useEffect(() => {
    if (selectedStudentId === previousSelectedStudentIdRef.current) {
      return
    }

    previousSelectedStudentIdRef.current = selectedStudentId
    setGradesPage(1)
    setTimelinePage(1)
    setHasLoadedGrades(false)
    setHasLoadedTimeline(false)
    setHasLoadedAverage(false)
    setAverageData(null)
    setAverageFilters(emptyAverageFilters)
    setDisciplineOptions([])
  }, [selectedStudentId])

  useEffect(() => {
    if (selectedStudentId !== null) {
      return
    }

    resetStudentDetails()
  }, [selectedStudentId])

  useEffect(() => {
    if (selectedStudentId === null) {
      return
    }

    let isActive = true

    void loadGradesForStudent(selectedStudentId, gradesPage, () => isActive)

    return () => {
      isActive = false
    }
  }, [selectedStudentId, gradesPage])

  useEffect(() => {
    if (selectedStudentId === null) {
      return
    }

    let isActive = true

    void loadTimelineForStudent(selectedStudentId, timelinePage, () => isActive)

    return () => {
      isActive = false
    }
  }, [selectedStudentId, timelinePage])

  useEffect(() => {
    if (selectedStudentId === null) {
      return
    }

    let isActive = true

    void loadAverageForStudent(selectedStudentId, emptyAverageFilters, () => isActive)

    return () => {
      isActive = false
    }
  }, [selectedStudentId])

  useEffect(() => {
    if (selectedStudentId === null) {
      return
    }

    let isActive = true

    void loadDisciplineOptionsForStudent(selectedStudentId, () => isActive)

    return () => {
      isActive = false
    }
  }, [selectedStudentId])

  const semesterOptions = useMemo(
    () => getSemesterOptions(disciplineOptions),
    [disciplineOptions],
  )

  const visibleDisciplineOptions = useMemo(
    () => getVisibleDisciplineOptions(disciplineOptions, averageFilters.semesterNo),
    [disciplineOptions, averageFilters.semesterNo],
  )

  useEffect(() => {
    if (averageFilters.disciplineId === '') {
      return
    }

    const hasSelectedDiscipline = visibleDisciplineOptions.some(
      (discipline) => String(discipline.disciplineId) === averageFilters.disciplineId,
    )

    if (hasSelectedDiscipline) {
      return
    }

    setAverageFilters((currentFilters) => ({
      ...currentFilters,
      disciplineId: '',
    }))
  }, [averageFilters.disciplineId, visibleDisciplineOptions])

  function handleStudentChange(studentId: number) {
    setSelectedStudentId(studentId)
  }

  function handleStudentsPageChange(page: number) {
    setStudentsPage(page)
  }

  function handleGradesPageChange(page: number) {
    setGradesPage(page)
  }

  function handleTimelinePageChange(page: number) {
    setTimelinePage(page)
  }

  function updateAverageFilter<K extends keyof AverageGradeFilters>(
    key: K,
    value: AverageGradeFilters[K],
  ) {
    setAverageFilters((currentFilters) => ({
      ...currentFilters,
      [key]: value,
    }))
    setHasLoadedAverage(false)
    setAverageData(null)
  }

  function handleSemesterFilterChange(value: string) {
    updateAverageFilter('semesterNo', value)
  }

  function handleDisciplineFilterChange(value: string) {
    updateAverageFilter('disciplineId', value)
  }

  function handleAverageRefresh() {
    if (selectedStudentId === null) {
      return
    }

    void loadAverageForStudent(selectedStudentId, averageFilters)
  }

  function handleTimelineRefresh() {
    if (selectedStudentId === null) {
      return
    }

    void loadTimelineForStudent(selectedStudentId, timelinePage)
  }

  const studentSelection: StudentSelectionState = {
    students,
    pagination: buildPaginationState(studentsPage, studentsTotalCount),
    selectedStudentId,
    isLoading: isLoadingStudents,
  }

  const averageState: AverageSummaryState = {
    data: averageData,
    hasLoaded: hasLoadedAverage,
    isLoading: isLoadingAverage,
  }

  const gradesState: AsyncCollectionState<GradeDto> = {
    items: grades,
    pagination: buildPaginationState(gradesPage, gradesTotalCount),
    hasLoaded: hasLoadedGrades,
    isLoading: isLoadingGrades,
  }

  const timelineState: AsyncCollectionState<TimelineEventDto> = {
    items: timelineEvents,
    pagination: buildPaginationState(timelinePage, timelineTotalCount),
    hasLoaded: hasLoadedTimeline,
    isLoading: isLoadingTimeline,
  }

  return {
    studentSelection,
    selectionHandlers: {
      handleStudentChange,
      handleStudentsPageChange,
    },
    averageFilters,
    averageState,
    gradesState,
    timelineState,
    semesterOptions,
    visibleDisciplineOptions,
    isLoadingDisciplineOptions,
    errorMessage,
    dashboardHandlers: {
      handleSemesterFilterChange,
      handleDisciplineFilterChange,
      handleAverageRefresh,
      handleGradesPageChange,
    },
    timelineHandlers: {
      handleTimelineRefresh,
      handleTimelinePageChange,
    },
  }
}
