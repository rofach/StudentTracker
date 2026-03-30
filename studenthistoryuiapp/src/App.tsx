import './App.css'
import { AppHeader } from './components/AppHeader'
import { DashboardPage } from './components/DashboardPage'
import { TimelinePage } from './components/TimelinePage'
import { useHashPage } from './hooks/useHashPage'
import { useStudentHistoryData } from './hooks/useStudentHistoryData'

function App() {
  const page = useHashPage()
  const data = useStudentHistoryData()

  return (
    <div className="page">
      <AppHeader page={page} />

      {page === 'dashboard' ? (
        <DashboardPage
          students={data.students}
          studentsPage={data.studentsPage}
          studentsPageSize={data.pageSize}
          studentsTotalCount={data.studentsTotalCount}
          selectedStudentId={data.selectedStudentId}
          isLoadingStudents={data.isLoadingStudents}
          semesterNo={data.semesterNo}
          disciplineId={data.disciplineId}
          academicYearStart={data.academicYearStart}
          isLoadingAverage={data.isLoadingAverage}
          isLoadingGrades={data.isLoadingGrades}
          average={data.average}
          hasLoadedAverage={data.hasLoadedAverage}
          grades={data.grades}
          gradesPage={data.gradesPage}
          gradesPageSize={data.pageSize}
          gradesTotalCount={data.gradesTotalCount}
          hasLoadedGrades={data.hasLoadedGrades}
          error={data.error}
          onStudentChange={data.handleStudentChange}
          onStudentsPageChange={data.setStudentsPage}
          onSemesterNoChange={data.setSemesterNo}
          onDisciplineIdChange={data.setDisciplineId}
          onAcademicYearStartChange={data.setAcademicYearStart}
          onRefreshAverage={data.refreshAverage}
          onGradesPageChange={data.setGradesPage}
        />
      ) : (
        <TimelinePage
          students={data.students}
          studentsPage={data.studentsPage}
          studentsPageSize={data.pageSize}
          studentsTotalCount={data.studentsTotalCount}
          selectedStudentId={data.selectedStudentId}
          isLoadingStudents={data.isLoadingStudents}
          isLoadingTimeline={data.isLoadingTimeline}
          hasLoadedTimeline={data.hasLoadedTimeline}
          timelineEvents={data.timelineEvents}
          timelinePage={data.timelinePage}
          timelinePageSize={data.pageSize}
          timelineTotalCount={data.timelineTotalCount}
          onStudentChange={data.handleStudentChange}
          onStudentsPageChange={data.setStudentsPage}
          onRefreshTimeline={data.refreshTimeline}
          onTimelinePageChange={data.setTimelinePage}
        />
      )}
    </div>
  )
}

export default App
