#nullable enable
using System;
using System.Collections.Generic;

namespace RPK.DataValidation
{
    public abstract class DataParserAndValidatorBase
    {
        public event Action<object>? Validated;

        public event Action<string?>? Invalidated;

        protected abstract (object? parsedResult, string? errorMessage) Parse(string stringToParseAndValidate);

        protected HashSet<Func<object, (bool result, string? errorMessage)>> ValidateConditions { get; } = new();

        public virtual void ValidateParameter(string stringToParseAndValidate)
        {
            (object? parsedValue, string? errorMessage) = Parse(stringToParseAndValidate);

            if (parsedValue is null)
            {
                Invalidated!.Invoke(errorMessage);
                return;
            }

            foreach (Func<object, (bool result, string? errorMessage)> condition in ValidateConditions)
            {
                (bool result, string? invalidatedErrorMessage) = condition(parsedValue);

                if (result is false)
                {
                    Invalidated!.Invoke(invalidatedErrorMessage);
                    return;
                }
            }

            if (parsedValue is not null)
                Validated!.Invoke(parsedValue);
        }
    }
}