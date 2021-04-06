namespace RPK.View.DataValidation
{
    class LongTypeParameterValidator : DataValidatorBase
    {
        public LongTypeParameterValidator()
        {
            AddCheckConditions();
        }

        private void AddCheckConditions()
        {
            CheckConditions.Add(ParseCondition);
            CheckConditions.Add(NotLessThanZeroCondition);
            CheckConditions.Add(NotEqualsZeroCondition);
        }

#nullable enable
        protected (bool result, string? errorMessage) ParseCondition()
        {
            try
            {
                ParsedValue = long.Parse(ParameterString!);
                return (true, null);
            }
            catch
            {
                return (false, "Заданное значение не целочисленно");
            }
        }

        protected (bool result, string? errorMessage) NotLessThanZeroCondition()
        {
            if (ParsedValue < 0)
                return (false, "Значение не может быть меньше нуля");
            else
                return (true, null);
        }

        protected (bool result, string? errorMessage) NotEqualsZeroCondition()
        {
            if (ParsedValue is not 0)
                return (true, null);
            else
                return (false, "Значение не может равняться нулю");
        }

        long parsedValue;
        protected long ParsedValue { get => parsedValue; private set { parsedValue = value; Result = value; } }
    }
}