#nullable enable
using RPK.Model;
using RPK.Model.MathModel;

namespace RPK.Researcher.View
{
    public record Parameter
    {
        public Parameter(long? parameterId, string name, string designation, string measureUnit, object? value)
        {
            ParameterId = parameterId;
            Name = name;
            Designation = designation;
            MeasureUnit = measureUnit;
            Value = value;
        }

        public long? ParameterId { get; init; }

        public string Name { get; init; }

        public string Designation { get; init; }

        public string MeasureUnit { get; init; }

        public object? Value { get; set; }

        public static explicit operator Parameter(EmpiricalCoefficientOfMathModel empiricalCoefficientOfMathModel)
        {
            return InitializeParameter(empiricalCoefficientOfMathModel.Parameter, empiricalCoefficientOfMathModel.ParameterValue);
        }

        public static explicit operator Parameter(ParameterOfMaterialProperty parameterOfMaterialProperty)
        {
            return InitializeParameter(parameterOfMaterialProperty.Parameter, parameterOfMaterialProperty.ParameterValue);
        }

        public static explicit operator Parameter(CanalGeometryParameter canalGeometryParameter)
        {
            return InitializeParameter(canalGeometryParameter.Parameter, canalGeometryParameter.ParameterValue);
        }

        private static Parameter InitializeParameter(Model.MathModel.Parameter modelParameter, object? parameterValue)
        {
            return new Parameter
            (
                parameterId: modelParameter.ParameterId,
                name: modelParameter.Name,
                measureUnit: modelParameter.MeasureUnit,
                designation: modelParameter.Designation,
                value: parameterValue
            );
        }
    }

    public record ParameterWithBounds(long? ParameterId, string Name, string Designation, string MeasureUnit,
        object? Value, double LowerBound, double UpperBound, bool ShowBounds) :
        Parameter(ParameterId, Name, Designation, MeasureUnit, Value)
    {
        public static explicit operator ParameterWithBounds(VariableParameter variableParameter)
        {
            return new ParameterWithBounds
            (
                ParameterId: variableParameter.Parameter.ParameterId,
                Name: variableParameter.Parameter.Name,
                Designation: variableParameter.Parameter.Designation,
                MeasureUnit: variableParameter.Parameter.MeasureUnit,
                Value: null,
                LowerBound: variableParameter.ValueLowerBound,
                UpperBound: variableParameter.ValueUpperBound,
                ShowBounds: true
            );
        }
    }

    public static class ToStringExtensions
    {
        public static string ToString(this Material material) => material.Name;

        public static string ToString(this Canal canal) => canal.Brand;
    }
}