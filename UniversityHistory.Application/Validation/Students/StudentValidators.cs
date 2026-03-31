using FluentValidation;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Validation.Common;
using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Application.Validation.Students;

public class StudentCreateDtoValidator : AppValidator<StudentCreateDto>
{
    public StudentCreateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        When(x => x.BirthDate.HasValue, () =>
        {
            RuleFor(x => x.BirthDate!.Value)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("BirthDate cannot be in the future.");
        });

        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email!)
                .MaximumLength(100)
                .EmailAddress();
        });

        When(x => !string.IsNullOrWhiteSpace(x.Phone), () =>
        {
            RuleFor(x => x.Phone!)
                .MaximumLength(20);
        });
    }
}

public class StudentUpdateDtoValidator : AppValidator<StudentUpdateDto>
{
    public StudentUpdateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(50);

        When(x => x.BirthDate.HasValue, () =>
        {
            RuleFor(x => x.BirthDate!.Value)
                .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("BirthDate cannot be in the future.");
        });

        When(x => !string.IsNullOrWhiteSpace(x.Email), () =>
        {
            RuleFor(x => x.Email!)
                .MaximumLength(100)
                .EmailAddress();
        });

        When(x => !string.IsNullOrWhiteSpace(x.Phone), () =>
        {
            RuleFor(x => x.Phone!)
                .MaximumLength(20);
        });
    }
}

public class ChangeStatusDtoValidator : AppValidator<ChangeStatusDto>
{
    public ChangeStatusDtoValidator()
    {
        RuleFor(x => x.Status)
            .NotEmpty()
            .IsEnumName(typeof(StudentStatus), caseSensitive: false);
    }
}
