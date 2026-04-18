using UniversityHistory.Application.DTOs;
using UniversityHistory.Domain.Entities;
using UniversityHistory.Domain.Enums;

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

    public static ExternalTransfer ToEntity(this CreateTransferDto dto, Guid studentId, TransferType transferType)
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
        return new AcademicLeaveDto(
            leave.LeaveId,
            leave.StartDate,
            leave.EndDate,
            leave.Reason,
            leave.ReturnReason);
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

    public static StudentInternalTransferSummaryDto ToSummaryDto(this StudentGroupTransfer transfer)
    {
        var differenceItems = transfer.DifferenceItems;

        return new StudentInternalTransferSummaryDto(
            transfer.TransferId,
            transfer.TransferDate,
            transfer.Reason,
            transfer.OldEnrollmentId,
            transfer.OldEnrollment.Group.GroupCode,
            transfer.NewEnrollmentId,
            transfer.NewEnrollment.Group.GroupCode,
            differenceItems.Count,
            differenceItems.Count(static item => item.Status == DifferenceItemStatus.Pending),
            differenceItems.Count(static item => item.Status == DifferenceItemStatus.Completed),
            differenceItems.Count(static item => item.Status == DifferenceItemStatus.Waived));
    }

    public static StudentMovementDto ToDto(
        this IEnumerable<AcademicLeave> leaves,
        IEnumerable<ExternalTransfer> transfers,
        IEnumerable<StudentGroupTransfer> internalTransfers)
    {
        return new StudentMovementDto(
            leaves.Select(static leave => leave.ToDto()),
            transfers.Select(static transfer => transfer.ToDto()),
            internalTransfers
                .Select(static transfer => transfer.ToSummaryDto())
                .OrderByDescending(static transfer => transfer.TransferDate));
    }
}

