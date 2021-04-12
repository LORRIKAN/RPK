#nullable enable

namespace RPK.DataValidation
{
    public class DoubleTypeParameterParserAndValidator : DataParserAndValidatorBase
    {
        public DoubleTypeParameterParserAndValidator()
        {
            AddCheckConditions();
        }

        private void AddCheckConditions()
        {
            ValidateConditions.Add(NotLessThanZeroCondition);
            ValidateConditions.Add(NotEqualsZeroCondition);
        }

        protected override (object? parsedResult, string? errorMessage) Parse(string stringToParseAndValidate)
        {
            try
            {
                stringToParseAndValidate = stringToParseAndValidate.Replace('.', ',');
                object? parsedValue = double.Parse(stringToParseAndValidate);
                return (parsedValue, null);
            }
            catch
            {
                return (null, "Заданное значение не дробное");
            }
        }

        protected (bool result, string? errorMessage) NotLessThanZeroCondition(object parsedValue)
        {
            if (parsedValue is < 0)
                return (false, "Значение не может быть меньше нуля");
            else
                return (true, null);
        }

        protected (bool result, string? errorMessage) NotEqualsZeroCondition(object parsedValue)
        {
            if (parsedValue is 0 or <= double.Epsilon)
                return (false, "Значение не может равняться нулю");
            else
                return (true, null);
        }
    }
}
