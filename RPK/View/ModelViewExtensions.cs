#nullable enable
using RPK.Model;

namespace RPK.View
{
    public record Parameter(long ParameterId, string Name, string Designation, string MeasureUnit, object? Value)
    {
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

        private static Parameter InitializeParameter(Model.Parameter modelParameter, object? parameterValue)
        {
            return new Parameter
            (
                ParameterId: modelParameter.ParameterId,
                Name: modelParameter.Name,
                MeasureUnit: modelParameter.MeasureUnit,
                Designation: modelParameter.Designation,
                Value: parameterValue
            );
        }
    }

    public record ParameterWithBounds(long ParameterId, string Name, string Designation, string MeasureUnit, 
        object? Value, double LowerBound, double UpperBound) : 
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
                UpperBound: variableParameter.ValueUpperBound
            );
        }
    }

    public static class ToStringExtensions
    {
        public static string ToString(this Material material) => $"{material.MaterialId}. {material.Name}";

        public static string ToString(this Canal canal) => $"{canal.CanalId}. {canal.Brand}";
    }
}