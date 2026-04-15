using UniversityHistory.Application.DTOs;
using UniversityHistory.Domain.Entities;

namespace UniversityHistory.Application.Mappings;

public static class MovementMappingExtensions
{
    public static AcademicLeave ToEntity(this CreateLeaveDto dto)
    {
        return new AcademicLeave
        {
            EnrollmentId = dto.EnrollmentId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Reason = dto.Reason
        };
    }

    public static ExternalTransfer ToEntity(this CreateTransferDto dto, int studentId, Domain.Enums.TransferType transferType)
    {
        return new ExternalTransfer
        {
            StudentId = studentId,
            InstitutionId = dto.InstitutionId,
            TransferType = transferType,
            TransferDate = dto.TransferDate,
            Notes = dto.Notes
        };
    }

    public static AcademicLeaveDto ToDto(this AcademicLeave leave)
    {
        return new AcademicLeaveDto(leave.LeaveId, leave.StartDate, leave.EndDate, leave.Reason);
    }

    public static ExternalTransferDto ToDto(this ExternalTransfer transfer)
    {
        return transfer.ToDto(transfer.Institution.InstitutionName);
    }

    public static ExternalTransferDto ToDto(this ExternalTransfer transfer, string institutionName)
    {
        return new ExternalTransferDto(
            transfer.TransferId,
            transfer.TransferType.ToString(),
            transfer.TransferDate,
            institutionName,
            transfer.Notes);
    }

    public static StudentMovementDto ToDto(
        this IEnumerable<AcademicLeave> leaves,
        IEnumerable<ExternalTransfer> transfers)
    {
        return new StudentMovementDto(
            leaves.Select(static leave => leave.ToDto()),
            transfers.Select(static transfer => transfer.ToDto()));
    }
}
