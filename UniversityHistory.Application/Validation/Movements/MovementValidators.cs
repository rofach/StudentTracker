using FluentValidation;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Validation.Common;
using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Application.Validation.Movements;

public class CreateTransferDtoValidator : AppValidator<CreateTransferDto>
{
    public CreateTransferDtoValidator()
    {
        RuleFor(x => x.InstitutionId)
            .GreaterThan(0);

        RuleFor(x => x.TransferType)
            .NotEmpty()
            .IsEnumName(typeof(TransferType), caseSensitive: false);

        RuleFor(x => x.TransferDate)
            .NotDefaultDate();

        When(x => !string.IsNullOrWhiteSpace(x.Notes), () =>
        {
            RuleFor(x => x.Notes!)
                .MaximumLength(200);
        });
    }
}

public class CreateLeaveDtoValidator : AppValidator<CreateLeaveDto>
{
    public CreateLeaveDtoValidator()
    {
        RuleFor(x => x.EnrollmentId)
            .GreaterThan(0);

        RuleFor(x => x.StartDate)
            .NotDefaultDate();

        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(200);

        When(x => x.EndDate.HasValue, () =>
        {
            RuleFor(x => x.EndDate!.Value)
                .Must((dto, endDate) => endDate >= dto.StartDate)
                .WithMessage("EndDate must be on or after StartDate.");
        });
    }
}

public class CloseAcademicLeaveDtoValidator : AppValidator<CloseAcademicLeaveDto>
{
    public CloseAcademicLeaveDtoValidator()
    {
        RuleFor(x => x.EndDate)
            .NotDefaultDate();

        When(x => !string.IsNullOrWhiteSpace(x.ReturnReason), () =>
        {
            RuleFor(x => x.ReturnReason!)
                .MaximumLength(200);
        });
    }
}
