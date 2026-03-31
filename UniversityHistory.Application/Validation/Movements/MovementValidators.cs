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

        When(x => !string.IsNullOrWhiteSpace(x.Reason), () =>
        {
            RuleFor(x => x.Reason!)
                .MaximumLength(200);
        });
    }
}
