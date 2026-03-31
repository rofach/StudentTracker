import type { PaginationState } from '../types'

type PaginationControlsProps = {
  pagination: PaginationState
  disabled?: boolean
  handlePageChange: (page: number) => void
}

export function PaginationControls({
  pagination,
  disabled = false,
  handlePageChange,
}: PaginationControlsProps) {
  const { currentPage, pageSize, totalCount } = pagination
  const totalPages = Math.max(1, Math.ceil(totalCount / pageSize))
  const hasPreviousPage = currentPage > 1
  const hasNextPage = currentPage < totalPages

  function handlePreviousPageClick() {
    if (!hasPreviousPage) {
      return
    }

    handlePageChange(currentPage - 1)
  }

  function handleNextPageClick() {
    if (!hasNextPage) {
      return
    }

    handlePageChange(currentPage + 1)
  }

  return (
    <div className="pager">
      <button
        className="pager-button"
        type="button"
        disabled={disabled || !hasPreviousPage}
        onClick={handlePreviousPageClick}
      >
        Попередня
      </button>
      <p className="pager-status">
        Сторінка {currentPage} з {totalPages}
      </p>
      <button
        className="pager-button"
        type="button"
        disabled={disabled || !hasNextPage}
        onClick={handleNextPageClick}
      >
        Наступна
      </button>
    </div>
  )
}
