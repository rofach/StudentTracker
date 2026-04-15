using FluentValidation;
using UniversityHistory.Application.DTOs;
using UniversityHistory.Application.Validation.Common;

namespace UniversityHistory.Application.Validation.Disciplines;

public class CreateDisciplineDtoValidator : AppValidator<CreateDisciplineDto>
{
    public CreateDisciplineDtoValidator()
    {
        RuleFor(x => x.DisciplineName)
            .NotEmpty()
            .MaximumLength(200);
    }
}

public class UpdateDisciplineDtoValidator : AppValidator<UpdateDisciplineDto>
{
    public UpdateDisciplineDtoValidator()
    {
        RuleFor(x => x.DisciplineName)
            .NotEmpty()
            .MaximumLength(200);
    }
}


