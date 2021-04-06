#nullable enable
using System;
using System.Collections.Generic;

namespace RPK.View.DataValidation
{
    public abstract class DataValidatorBase
    {
        protected string? ParameterString { get; set; }

        protected HashSet<Func<(bool result, string? errorMessage)>> CheckConditions { get; } = new();

        public virtual (object? result, string? errorMessage) ValidateParameter(string parameterString)
        {
            ParameterString = parameterString;

            foreach (Func<(bool, string?)> condition in CheckConditions)
            {
                (bool result, string? errorMessage) = condition();
                if (result is false)
                    return (null, errorMessage);
            }

            return (Result, null);
        }

        protected object? Result { get; set; }

#nullable disable
    }
}