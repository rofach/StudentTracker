import type { AsyncCollectionState, TimelineEventDto } from '../../types'
import { formatRange, mapEventTypeLabel } from '../../utils/formatters'
import { LoadingOverlay } from '../LoadingOverlay'
import { PaginationControls } from '../PaginationControls'

type TimelineEventsCardProps = {
  timelineState: AsyncCollectionState<TimelineEventDto>
  handleTimelinePageChange: (page: number) => void
}

export function TimelineEventsCard({
  timelineState,
  handleTimelinePageChange,
}: TimelineEventsCardProps) {
  const { items: timelineEvents, pagination, hasLoaded, isLoading } = timelineState
  const hasTimelineEvents = timelineEvents.length > 0
  const shouldShowEmptyState = !isLoading && hasLoaded && !hasTimelineEvents
  const shouldShowPagination = pagination.totalCount > 0

  return (
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

        {isLoading && (
          <LoadingOverlay className="surface-overlay" message="Завантажуємо події..." />
        )}
      </div>

      {shouldShowEmptyState && (
        <p className="card-message">Для цього студента подій таймлайна поки немає.</p>
      )}

      {shouldShowPagination && (
        <PaginationControls
          pagination={pagination}
          disabled={isLoading}
          handlePageChange={handleTimelinePageChange}
        />
      )}
    </section>
  )
}
