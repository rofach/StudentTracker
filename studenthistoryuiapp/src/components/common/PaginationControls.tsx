type PaginationControlsProps = {
  page: number
  pageSize: number
  totalCount: number
  onPageChange: (page: number) => void
  onPageSizeChange?: (pageSize: number) => void
}

export function PaginationControls({
  page,
  pageSize,
  totalCount,
  onPageChange,
  onPageSizeChange,
}: PaginationControlsProps) {
  const totalPages = Math.max(1, Math.ceil(totalCount / pageSize))
  const canGoBack = page > 1
  const canGoNext = page < totalPages

  return (
    <div className="pagination">
      <button type="button" onClick={() => onPageChange(1)} disabled={!canGoBack}>
        {"<<"}
      </button>
      <button type="button" onClick={() => onPageChange(page - 1)} disabled={!canGoBack}>
        {"<"}
      </button>
      <span className="pagination__info">
        Сторінка {page} з {totalPages}
      </span>
      <button type="button" onClick={() => onPageChange(page + 1)} disabled={!canGoNext}>
        {">"}
      </button>
      <button type="button" onClick={() => onPageChange(totalPages)} disabled={!canGoNext}>
        {">>"}
      </button>
      {onPageSizeChange ? (
        <label className="pagination__size">
          Рядків:
          <select
            value={pageSize}
            onChange={(event) => onPageSizeChange(Number(event.target.value))}
          >
            <option value={10}>10</option>
            <option value={20}>20</option>
            <option value={50}>50</option>
          </select>
        </label>
      ) : null}
    </div>
  )
}
