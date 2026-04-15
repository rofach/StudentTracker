using FluentValidation;

namespace UniversityHistory.Application.Validation.Common;

public abstract class AppValidator<T> : AbstractValidator<T>
{
    protected AppValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
    }
}


