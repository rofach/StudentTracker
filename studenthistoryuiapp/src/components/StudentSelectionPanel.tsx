import type { ChangeEvent, ReactNode } from 'react'
import type { StudentSelectionState } from '../types'
import { PaginationControls } from './PaginationControls'
import { Spinner } from './Spinner'

export type StudentSelectionHandlers = {
  handleStudentChange: (studentId: number) => void
  handleStudentsPageChange: (page: number) => void
}

type StudentSelectionPanelProps = {
  title: string
  selection: StudentSelectionState
  handlers: StudentSelectionHandlers
  children?: ReactNode
}

export function StudentSelectionPanel({
  title,
  selection,
  handlers,
  children,
}: StudentSelectionPanelProps) {
  const { students, pagination, selectedStudentId, isLoading } = selection
  const { handleStudentChange, handleStudentsPageChange } = handlers
  const hasStudents = students.length > 0
  const selectedValue = selectedStudentId ?? ''

  function handleSelectionChange(event: ChangeEvent<HTMLSelectElement>) {
    const nextStudentId = Number(event.target.value)

    if (Number.isNaN(nextStudentId)) {
      return
    }

    handleStudentChange(nextStudentId)
  }

  return (
    <section className="card controls">
      <h2>{title}</h2>

      <label>
        Студент
        <div className="field-row">
          <select
            value={selectedValue}
            onChange={handleSelectionChange}
            disabled={isLoading || !hasStudents}
          >
            {!hasStudents && <option value="">Студентів поки немає</option>}
            {students.map((student) => (
              <option key={student.studentId} value={student.studentId}>
                {student.firstName} {student.lastName} ({student.status})
              </option>
            ))}
          </select>
          {isLoading && <Spinner />}
        </div>
      </label>

      <PaginationControls
        pagination={pagination}
        disabled={isLoading}
        handlePageChange={handleStudentsPageChange}
      />

      {children}
    </section>
  )
}
