import './App.css'
import { AppHeader } from './components/AppHeader'
import { DashboardPage } from './components/DashboardPage'
import { TimelinePage } from './components/TimelinePage'
import { useHashPage } from './hooks/useHashPage'
import { useStudentHistoryData } from './hooks/useStudentHistoryData'

function App() {
  const page = useHashPage()
  const data = useStudentHistoryData()
  const isDashboardPage = page === 'dashboard'
  const activePageContent = isDashboardPage ? (
    <DashboardPage
      studentSelection={data.studentSelection}
      selectionHandlers={data.selectionHandlers}
      filters={data.averageFilters}
      averageState={data.averageState}
      gradesState={data.gradesState}
      semesterOptions={data.semesterOptions}
      disciplineOptions={data.visibleDisciplineOptions}
      isLoadingDisciplineOptions={data.isLoadingDisciplineOptions}
      errorMessage={data.errorMessage}
      handlers={data.dashboardHandlers}
    />
  ) : (
    <TimelinePage
      studentSelection={data.studentSelection}
      selectionHandlers={data.selectionHandlers}
      timelineState={data.timelineState}
      handlers={data.timelineHandlers}
    />
  )

  return (
    <div className="page">
      <AppHeader page={page} />
      {activePageContent}
    </div>
  )
}

export default App
