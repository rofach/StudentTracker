type PaginationControlsProps = {
  currentPage: number
  pageSize: number
  totalCount: number
  disabled?: boolean
  onPageChange: (page: number) => void
}

export function PaginationControls({
  currentPage,
  pageSize,
  totalCount,
  disabled = false,
  onPageChange,
}: PaginationControlsProps) {
  const totalPages = Math.max(1, Math.ceil(totalCount / pageSize))
  const canGoBack = currentPage > 1
  const canGoForward = currentPage < totalPages

  return (
    <div className="pager">
      <button
        className="pager-button"
        type="button"
        disabled={disabled || !canGoBack}
        onClick={() => onPageChange(currentPage - 1)}
      >
        Попередня
      </button>
      <p className="pager-status">
        Сторінка {currentPage} з {totalPages}
      </p>
      <button
        className="pager-button"
        type="button"
        disabled={disabled || !canGoForward}
        onClick={() => onPageChange(currentPage + 1)}
      >
        Наступна
      </button>
    </div>
  )
}
