namespace RPK.View.DataValidation
{
    public class DoubleTypeParameterValidator : DataValidatorBase
    {
        public DoubleTypeParameterValidator()
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
                ParameterString = ParameterString?.Replace('.', ',');
                ParsedValue = double.Parse(ParameterString!);
                return (true, null);
            }
            catch
            {
                return (false, "Заданное значение не дробное");
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
            if (ParsedValue is 0 or <= double.Epsilon)
                return (false, "Значение не может равняться нулю");
            else
                return (true, null);
        }

        double parsedValue;
        protected double ParsedValue { get => parsedValue; private set { parsedValue = value; Result = value; } }
    }
}
