using FluentValidation;
using System.Text.RegularExpressions;

namespace UniversityHistory.Application.Validation.Common;

public static class ValidationExtensions
{
    private static readonly Regex PersonNameRegex = new(
        @"^[\p{L}\s'\-]+$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public static IRuleBuilderOptions<T, DateOnly> NotDefaultDate<T>(this IRuleBuilder<T, DateOnly> ruleBuilder)
    {
        return ruleBuilder
            .Must(date => date != default)
            .WithMessage("{PropertyName} is required.");
    }

    public static IRuleBuilderOptions<T, DateOnly> NotInFuture<T>(this IRuleBuilder<T, DateOnly> ruleBuilder)
    {
        return ruleBuilder
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("{PropertyName} cannot be in the future.");
    }

    public static IRuleBuilderOptions<T, string> RequiredPersonName<T>(this IRuleBuilder<T, string> ruleBuilder, int maxLength = 50)
    {
        return ruleBuilder
            .NotEmpty()
            .MaximumLength(maxLength)
            .Matches(PersonNameRegex)
            .WithMessage("{PropertyName} contains invalid characters.");
    }

    public static IRuleBuilderOptions<T, string?> OptionalPersonName<T>(this IRuleBuilder<T, string?> ruleBuilder, int maxLength = 50)
    {
        return ruleBuilder
            .Must(value => string.IsNullOrWhiteSpace(value) || value.Trim().Length <= maxLength)
            .WithMessage($"{{PropertyName}} must be {maxLength} characters or fewer.")
            .Must(value => string.IsNullOrWhiteSpace(value) || PersonNameRegex.IsMatch(value.Trim()))
            .WithMessage("{PropertyName} contains invalid characters.");
    }

    public static IRuleBuilderOptions<T, string?> RequiredEmailAddressEx<T>(this IRuleBuilder<T, string?> ruleBuilder, int maxLength = 100)
    {
        return ruleBuilder
            .NotEmpty()
            .MaximumLength(maxLength)
            .EmailAddress();
    }

    public static IRuleBuilderOptions<T, string?> OptionalPhoneNumber<T>(this IRuleBuilder<T, string?> ruleBuilder, int maxLength = 20)
    {
        return ruleBuilder
            .Must(value =>
            {
                if (string.IsNullOrWhiteSpace(value))
                    return true;

                var trimmed = value.Trim();
                if (trimmed.Length > maxLength)
                    return false;

                if (trimmed.Any(static ch => !char.IsDigit(ch) && ch is not '+' and not '-' and not ' ' and not '(' and not ')'))
                    return false;

                var digitCount = trimmed.Count(char.IsDigit);
                return digitCount is >= 10 and <= 15;
            })
            .WithMessage("{PropertyName} must be a valid phone number.");
    }

    public static IRuleBuilderOptions<T, string?> OptionalText<T>(this IRuleBuilder<T, string?> ruleBuilder, int maxLength)
    {
        return ruleBuilder
            .Must(value => string.IsNullOrWhiteSpace(value) || value.Trim().Length <= maxLength)
            .WithMessage($"{{PropertyName}} must be {maxLength} characters or fewer.");
    }
}


