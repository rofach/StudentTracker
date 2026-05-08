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
            .RequiredPersonName();

        RuleFor(x => x.LastName)
            .RequiredPersonName();

        RuleFor(x => x.Patronymic)
            .OptionalPersonName();

        RuleFor(x => x.Email)
            .RequiredEmailAddressEx();

        When(x => x.BirthDate.HasValue, () =>
        {
            RuleFor(x => x.BirthDate!.Value)
                .NotInFuture();
        });

        RuleFor(x => x.Phone)
            .OptionalPhoneNumber();
    }
}

public class StudentUpdateDtoValidator : AppValidator<StudentUpdateDto>
{
    public StudentUpdateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .RequiredPersonName();

        RuleFor(x => x.LastName)
            .RequiredPersonName();

        RuleFor(x => x.Patronymic)
            .OptionalPersonName();

        RuleFor(x => x.Email)
            .RequiredEmailAddressEx();

        When(x => x.BirthDate.HasValue, () =>
        {
            RuleFor(x => x.BirthDate!.Value)
                .NotInFuture();
        });

        RuleFor(x => x.Phone)
            .OptionalPhoneNumber();
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


