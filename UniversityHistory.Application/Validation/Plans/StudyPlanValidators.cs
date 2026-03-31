using FluentValidation;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Validation.Common;
using UniversityHistory.Domain.Enums;

namespace UniversityHistory.Application.Validation.Plans;

public class AssignPlanDtoValidator : AppValidator<AssignPlanDto>
{
    public AssignPlanDtoValidator()
    {
        RuleFor(x => x.PlanId)
            .GreaterThan(0);

        RuleFor(x => x.DateFrom)
            .NotDefaultDate();
    }
}

public class CreateStudyPlanDtoValidator : AppValidator<CreateStudyPlanDto>
{
    public CreateStudyPlanDtoValidator()
    {
        RuleFor(x => x.SpecialtyCode)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.ValidFrom)
            .NotDefaultDate();

        When(x => !string.IsNullOrWhiteSpace(x.PlanName), () =>
        {
            RuleFor(x => x.PlanName!)
                .MaximumLength(100);
        });
    }
}

public class UpdateStudyPlanDtoValidator : AppValidator<UpdateStudyPlanDto>
{
    public UpdateStudyPlanDtoValidator()
    {
        RuleFor(x => x.SpecialtyCode)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.ValidFrom)
            .NotDefaultDate();

        When(x => !string.IsNullOrWhiteSpace(x.PlanName), () =>
        {
            RuleFor(x => x.PlanName!)
                .MaximumLength(100);
        });
    }
}

public class AddPlanDisciplineDtoValidator : AppValidator<AddPlanDisciplineDto>
{
    public AddPlanDisciplineDtoValidator()
    {
        RuleFor(x => x.DisciplineId)
            .GreaterThan(0);

        RuleFor(x => x.SemesterNo)
            .GreaterThan(0);

        RuleFor(x => x.ControlType)
            .NotEmpty()
            .IsEnumName(typeof(ControlType), caseSensitive: false);

        RuleFor(x => x.Hours)
            .GreaterThan(0);

        RuleFor(x => x.Credits)
            .GreaterThan(0);
    }
}

public class UpdatePlanDisciplineDtoValidator : AppValidator<UpdatePlanDisciplineDto>
{
    public UpdatePlanDisciplineDtoValidator()
    {
        RuleFor(x => x.SemesterNo)
            .GreaterThan(0);

        RuleFor(x => x.ControlType)
            .NotEmpty()
            .IsEnumName(typeof(ControlType), caseSensitive: false);

        RuleFor(x => x.Hours)
            .GreaterThan(0);

        RuleFor(x => x.Credits)
            .GreaterThan(0);
    }
}
