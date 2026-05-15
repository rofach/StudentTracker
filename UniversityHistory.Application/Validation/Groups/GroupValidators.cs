using FluentValidation;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Validation.Common;

namespace UniversityHistory.Application.Validation.Groups;

public class CreateGroupDtoValidator : AppValidator<CreateGroupDto>
{
    public CreateGroupDtoValidator()
    {
        RuleFor(x => x.GroupCode)
            .NotEmpty()
            .MaximumLength(20);

        RuleFor(x => x.DepartmentId)
            .NotEmpty();

        RuleFor(x => x.DateCreated)
            .NotDefaultDate();
    }
}

public class UpdateGroupDtoValidator : AppValidator<UpdateGroupDto>
{
    public UpdateGroupDtoValidator()
    {
        RuleFor(x => x.GroupCode)
            .NotEmpty()
            .MaximumLength(20);
    }
}
