import type { AverageSummaryState } from '../../types'
import { Spinner } from '../Spinner'

type AverageSummaryCardProps = {
  averageState: AverageSummaryState
}

export function AverageSummaryCard({ averageState }: AverageSummaryCardProps) {
  const averageValue = averageState.data?.average
  const formattedAverage =
    averageValue === null || averageValue === undefined ? '-' : averageValue.toFixed(2)
  const academicYearLabel = averageState.hasLoaded
    ? averageState.data?.academicYearLabel ?? 'усі роки'
    : '-'

  return (
    <section className="card summary">
      <h2>Середній бал</h2>
      <div className="metric-wrap">
        <div className="metric">
          <span className="metric-value">{formattedAverage}</span>
          <span className="metric-label">AVG</span>
        </div>
        {averageState.isLoading && <Spinner big />}
      </div>
      <p>Кількість оцінок у вибірці: {averageState.data?.gradeCount ?? 0}</p>
      <p>Навчальний рік: {academicYearLabel}</p>
    </section>
  )
}
