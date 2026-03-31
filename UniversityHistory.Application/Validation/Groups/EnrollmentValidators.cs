using FluentValidation;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Validation.Common;

namespace UniversityHistory.Application.Validation.Groups;

public class EnrollStudentDtoValidator : AppValidator<EnrollStudentDto>
{
    public EnrollStudentDtoValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0);

        RuleFor(x => x.GroupId)
            .GreaterThan(0);

        RuleFor(x => x.DateFrom)
            .NotDefaultDate();

        RuleFor(x => x.ReasonStart)
            .NotEmpty()
            .MaximumLength(50);

        When(x => x.SubgroupId.HasValue, () =>
        {
            RuleFor(x => x.SubgroupId!.Value)
                .GreaterThan(0);
        });
    }
}

public class CloseEnrollmentDtoValidator : AppValidator<CloseEnrollmentDto>
{
    public CloseEnrollmentDtoValidator()
    {
        RuleFor(x => x.DateTo)
            .NotDefaultDate();

        RuleFor(x => x.ReasonEnd)
            .NotEmpty()
            .MaximumLength(50);
    }
}

public class MoveStudentDtoValidator : AppValidator<MoveStudentDto>
{
    public MoveStudentDtoValidator()
    {
        RuleFor(x => x.NewGroupId)
            .GreaterThan(0);

        RuleFor(x => x.MoveDate)
            .NotDefaultDate();

        RuleFor(x => x.ReasonEnd)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ReasonStart)
            .NotEmpty()
            .MaximumLength(50);

        When(x => x.NewSubgroupId.HasValue, () =>
        {
            RuleFor(x => x.NewSubgroupId!.Value)
                .GreaterThan(0);
        });
    }
}

public class AssignSubgroupDtoValidator : AppValidator<AssignSubgroupDto>
{
    public AssignSubgroupDtoValidator()
    {
        RuleFor(x => x.SubgroupId)
            .GreaterThan(0);
    }
}
