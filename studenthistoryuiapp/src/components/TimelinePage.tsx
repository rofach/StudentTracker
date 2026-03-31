import type { AsyncCollectionState, StudentSelectionState, TimelineEventDto } from '../types'
import type { StudentSelectionHandlers } from './StudentSelectionPanel'
import { StudentSelectionPanel } from './StudentSelectionPanel'
import { TimelineEventsCard } from './timeline/TimelineEventsCard'

type TimelineHandlers = {
  handleTimelineRefresh: () => void
  handleTimelinePageChange: (page: number) => void
}

type TimelinePageProps = {
  studentSelection: StudentSelectionState
  selectionHandlers: StudentSelectionHandlers
  timelineState: AsyncCollectionState<TimelineEventDto>
  handlers: TimelineHandlers
}

export function TimelinePage({
  studentSelection,
  selectionHandlers,
  timelineState,
  handlers,
}: TimelinePageProps) {
  const refreshButtonLabel = timelineState.isLoading ? 'Оновлення...' : 'Оновити таймлайн'
  const isRefreshDisabled = studentSelection.selectedStudentId === null || timelineState.isLoading

  return (
    <main className="timeline-grid">
      <StudentSelectionPanel
        title="Параметри таймлайна"
        selection={studentSelection}
        handlers={selectionHandlers}
      >
        <button
          className="primary"
          type="button"
          disabled={isRefreshDisabled}
          onClick={handlers.handleTimelineRefresh}
        >
          {refreshButtonLabel}
        </button>
      </StudentSelectionPanel>

      <TimelineEventsCard
        timelineState={timelineState}
        handleTimelinePageChange={handlers.handleTimelinePageChange}
      />
    </main>
  )
}
