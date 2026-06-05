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

public class CreateTransferredStudentDtoValidator : AppValidator<CreateTransferredStudentDto>
{
    public CreateTransferredStudentDtoValidator()
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

        RuleFor(x => x.InstitutionId)
            .NotEmpty()
            .When(x => string.IsNullOrWhiteSpace(x.NewInstitutionName))
            .WithMessage("Оберіть заклад або вкажіть назву нового.");

        RuleFor(x => x.NewInstitutionName)
            .NotEmpty()
            .When(x => !x.InstitutionId.HasValue || x.InstitutionId == Guid.Empty)
            .WithMessage("Вкажіть назву нового закладу.");

        RuleFor(x => x.GroupId)
            .NotEmpty();

        RuleFor(x => x.DateFrom)
            .NotDefaultDate();
    }
}

public class ExpelStudentDtoValidator : AppValidator<ExpelStudentDto>
{
    public ExpelStudentDtoValidator()
    {
        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(200);
    }
}

public class GraduateStudentDtoValidator : AppValidator<GraduateStudentDto>
{
    public GraduateStudentDtoValidator()
    {
        RuleFor(x => x.Reason)
            .NotEmpty()
            .MaximumLength(200);
    }
}


