import type { StudentDto, TimelineEventDto } from '../types'
import { formatRange, mapEventTypeLabel } from '../utils/formatters'
import { Spinner } from './Spinner'

type TimelinePageProps = {
  students: StudentDto[]
  selectedStudentId: number | null
  isLoadingStudents: boolean
  isLoadingTimeline: boolean
  hasLoadedTimeline: boolean
  timelineEvents: TimelineEventDto[]
  onStudentChange: (studentId: number) => void
  onRefreshTimeline: () => void
}

export function TimelinePage({
  students,
  selectedStudentId,
  isLoadingStudents,
  isLoadingTimeline,
  hasLoadedTimeline,
  timelineEvents,
  onStudentChange,
  onRefreshTimeline,
}: TimelinePageProps) {
  return (
    <main className="timeline-grid">
      <section className="card controls">
        <h2>Параметри таймлайна</h2>
        <label>
          Студент
          <div className="field-row">
            <select
              value={selectedStudentId ?? ''}
              onChange={(event) => onStudentChange(Number(event.target.value))}
              disabled={isLoadingStudents || students.length === 0}
            >
              {students.map((student) => (
                <option key={student.studentId} value={student.studentId}>
                  {student.firstName} {student.lastName} ({student.status})
                </option>
              ))}
            </select>
            {isLoadingStudents && <Spinner />}
          </div>
        </label>
        <button
          className="primary"
          type="button"
          disabled={selectedStudentId === null || isLoadingTimeline}
          onClick={onRefreshTimeline}
        >
          {isLoadingTimeline ? 'Оновлення...' : 'Оновити таймлайн'}
        </button>
      </section>

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
      </section>
    </main>
  )
}
