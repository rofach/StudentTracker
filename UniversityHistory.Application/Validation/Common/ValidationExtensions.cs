using FluentValidation;

namespace UniversityHistory.Application.Validation.Common;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, DateOnly> NotDefaultDate<T>(this IRuleBuilder<T, DateOnly> ruleBuilder)
    {
        return ruleBuilder
            .Must(date => date != default)
            .WithMessage("{PropertyName} is required.");
    }
}
