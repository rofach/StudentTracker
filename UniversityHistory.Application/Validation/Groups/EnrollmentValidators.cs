using FluentValidation;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Validation.Common;

namespace UniversityHistory.Application.Validation.Groups;

public class EnrollStudentDtoValidator : AppValidator<EnrollStudentDto>
{
    public EnrollStudentDtoValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty();

        RuleFor(x => x.GroupId)
            .NotEmpty();

        RuleFor(x => x.DateFrom)
            .NotDefaultDate();

        RuleFor(x => x.ReasonStart)
            .NotEmpty()
            .MaximumLength(50);

        When(x => x.SubgroupId.HasValue, () =>
        {
            RuleFor(x => x.SubgroupId!.Value)
                .NotEmpty();
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
            .NotEmpty();

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
                .NotEmpty();
        });
    }
}

public class AssignSubgroupDtoValidator : AppValidator<AssignSubgroupDto>
{
    public AssignSubgroupDtoValidator()
    {
        RuleFor(x => x.SubgroupId)
            .NotEmpty();
    }
}


