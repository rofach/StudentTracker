using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Interfaces.Services;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Exceptions;
using UniversityHistory.Domain.Interfaces.Repositories;

namespace UniversityHistory.Application.Services;

public class MovementService : IMovementService
{
    private readonly IStudentRepository _studentRepo;
    private readonly IAcademicLeaveRepository _leaveRepo;
    private readonly IExternalTransferRepository _transferRepo;

    public MovementService(IStudentRepository studentRepo,
        IAcademicLeaveRepository leaveRepo,
        IExternalTransferRepository transferRepo)
    {
        _studentRepo = studentRepo;
        _leaveRepo = leaveRepo;
        _transferRepo = transferRepo;
    }

    public async Task<StudentMovementDto> GetMovementsAsync(int studentId, CancellationToken ct = default)
    {
        _ = await _studentRepo.GetByIdAsync(studentId, ct)
            ?? throw new NotFoundException(nameof(Student), studentId);

        var leaves = await _leaveRepo.GetByStudentIdAsync(studentId, ct);
        var transfers = await _transferRepo.GetByStudentIdAsync(studentId, ct);

        return new StudentMovementDto(
            Leaves: leaves.Select(l => new AcademicLeaveDto(l.LeaveId, l.StartDate, l.EndDate, l.Reason)),
            Transfers: transfers.Select(t => new ExternalTransferDto(
                t.TransferId, t.TransferType.ToString(),
                t.TransferDate, t.Institution.InstitutionName, t.Notes))
        );
    }
}
