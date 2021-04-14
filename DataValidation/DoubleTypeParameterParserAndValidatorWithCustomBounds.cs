#nullable enable

namespace RPK.DataValidation
{
    public class DoubleTypeParameterParserAndValidatorWithCustomBounds : DoubleTypeParameterParserAndValidator
    {
        public DoubleTypeParameterParserAndValidatorWithCustomBounds()
        {
            InitializeCheckConditions();
        }

        public double LowerBound { get; set; }

        public double UpperBound { get; set; }

        private void InitializeCheckConditions()
        {
            ValidateConditions.Remove(NotEqualsZeroCondition);
            ValidateConditions.Remove(NotLessThanZeroCondition);
            ValidateConditions.Add(NotLessThanLowerBoundCondition);
            ValidateConditions.Add(NotMoreThanUpperBoundCondition);
        }

        protected (bool result, string? errorMessage) NotLessThanLowerBoundCondition(object parsedValue)
        {
            if ((double)parsedValue < LowerBound)
                return (false, $"Значение не может быть меньше {LowerBound}");
            else
                return (true, null);
        }

        protected (bool result, string? errorMessage) NotMoreThanUpperBoundCondition(object parsedValue)
        {
            if ((double)parsedValue > UpperBound)
                return (false, $"Значение не может быть больше {UpperBound}");
            else
                return (true, null);
        }
    }
}