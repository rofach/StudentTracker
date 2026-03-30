import type { StudentDto, TimelineEventDto } from '../types'
import { formatRange, mapEventTypeLabel } from '../utils/formatters'
import { PaginationControls } from './PaginationControls'
import { Spinner } from './Spinner'
import { StudentSelectionPanel } from './StudentSelectionPanel'

type TimelinePageProps = {
  students: StudentDto[]
  studentsPage: number
  studentsPageSize: number
  studentsTotalCount: number
  selectedStudentId: number | null
  isLoadingStudents: boolean
  isLoadingTimeline: boolean
  hasLoadedTimeline: boolean
  timelineEvents: TimelineEventDto[]
  timelinePage: number
  timelinePageSize: number
  timelineTotalCount: number
  onStudentChange: (studentId: number) => void
  onStudentsPageChange: (page: number) => void
  onRefreshTimeline: () => void
  onTimelinePageChange: (page: number) => void
}

export function TimelinePage({
  students,
  studentsPage,
  studentsPageSize,
  studentsTotalCount,
  selectedStudentId,
  isLoadingStudents,
  isLoadingTimeline,
  hasLoadedTimeline,
  timelineEvents,
  timelinePage,
  timelinePageSize,
  timelineTotalCount,
  onStudentChange,
  onStudentsPageChange,
  onRefreshTimeline,
  onTimelinePageChange,
}: TimelinePageProps) {
  return (
    <main className="timeline-grid">
      <StudentSelectionPanel
        title="Параметри таймлайна"
        students={students}
        studentsPage={studentsPage}
        studentsPageSize={studentsPageSize}
        studentsTotalCount={studentsTotalCount}
        selectedStudentId={selectedStudentId}
        isLoadingStudents={isLoadingStudents}
        onStudentChange={onStudentChange}
        onStudentsPageChange={onStudentsPageChange}
      >
        <button
          className="primary"
          type="button"
          disabled={selectedStudentId === null || isLoadingTimeline}
          onClick={onRefreshTimeline}
        >
          {isLoadingTimeline ? 'Оновлення...' : 'Оновити таймлайн'}
        </button>
      </StudentSelectionPanel>

      <section className="card timeline-card">
        <h2>Таймлайн студента</h2>
        <div className="timeline-shell">
          {timelineEvents.map((event, index) => (
            <article key={`${event.eventType}-${event.dateFrom}-${index}`} className="timeline-item">
              <div className="timeline-dot" />
              <div className="timeline-content">
                <p className="timeline-type">{mapEventTypeLabel(event.eventType)}</p>
                <p className="timeline-desc">{event.description}</p>
                <p className="timeline-range">{formatRange(event.dateFrom, event.dateTo)}</p>
              </div>
            </article>
          ))}

          {isLoadingTimeline && (
            <div className="timeline-overlay">
              <Spinner big />
              <p>Завантажуємо події...</p>
            </div>
          )}
        </div>

        {!isLoadingTimeline && hasLoadedTimeline && timelineEvents.length === 0 && (
          <p>Для цього студента подій таймлайна поки немає.</p>
        )}

        {timelineTotalCount > 0 && (
          <PaginationControls
            currentPage={timelinePage}
            pageSize={timelinePageSize}
            totalCount={timelineTotalCount}
            disabled={isLoadingTimeline}
            onPageChange={onTimelinePageChange}
          />
        )}
      </section>
    </main>
  )
}
