import type { ReactNode } from 'react'
import type { StudentDto } from '../types'
import { PaginationControls } from './PaginationControls'
import { Spinner } from './Spinner'

type StudentSelectionPanelProps = {
  title: string
  students: StudentDto[]
  studentsPage: number
  studentsPageSize: number
  studentsTotalCount: number
  selectedStudentId: number | null
  isLoadingStudents: boolean
  onStudentChange: (studentId: number) => void
  onStudentsPageChange: (page: number) => void
  children?: ReactNode
}

export function StudentSelectionPanel({
  title,
  students,
  studentsPage,
  studentsPageSize,
  studentsTotalCount,
  selectedStudentId,
  isLoadingStudents,
  onStudentChange,
  onStudentsPageChange,
  children,
}: StudentSelectionPanelProps) {
  return (
    <section className="card controls">
      <h2>{title}</h2>

      <label>
        {'\u0421\u0442\u0443\u0434\u0435\u043D\u0442'}
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

      <PaginationControls
        currentPage={studentsPage}
        pageSize={studentsPageSize}
        totalCount={studentsTotalCount}
        disabled={isLoadingStudents}
        onPageChange={onStudentsPageChange}
      />

      {children}
    </section>
  )
}
