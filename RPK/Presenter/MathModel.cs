using RPK.View;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using static System.Math;

namespace RPK.Presenter
{
    public class MathModel
    {
        private Parameter<double> W { get; set; }
            = new(null, "Ширина", "W", "м", default);

        private Parameter<double> H { get; set; }
            = new(null, "Глубина", "H", "м", default);

        private Parameter<double> L { get; set; }
            = new(null, "Длина", "L", "м", default);

        private Parameter<double> p { get; set; }
            = new(null, "Плотность", "p", "кг/м^3", default);

        private Parameter<double> c { get; set; }
            = new(null, "Удельная теплоёмкость", "c", "Дж/(кг⋅°C)", default);

        private Parameter<double> T0 { get; set; }
            = new(null, "Температура плавления", "T0", "°C", default);

        private Parameter<double> Vu { get; set; }
            = new(null, "Скорость крышки", "Vu", "м/c", default);

        private Parameter<double> Tu { get; set; }
            = new(null, "Температура крышки", "Tu", "°C", default);

        private Parameter<double> u0 { get; set; }
            = new(null, "Коэффициент констистенции материала при температуре приведения", "u0", "Па⋅с^n", default);

        private Parameter<double> b { get; set; }
            = new(null, "Температурный коэффициент вязкости материала", "b", "1/°C", default);

        private Parameter<double> Tr { get; set; }
            = new(null, "Температура приведения", "Tr", "°C", default);

        private Parameter<double> n { get; set; }
            = new(null, "Индекс течения материала", "n", string.Empty, default);

        private Parameter<double> au { get; set; }
            = new(null, "Коэффициент теплоотдачи от крышки канала к материалу", "au", "Вт/(м^2⋅°C)", default);

        private Parameter<double> step { get; set; }
            = new(null, "Шаг решения", "d", string.Empty, default);

        private IntermediateResult<double> F { get; set; }

        private IntermediateResult<double> gamma { get; set; }

        private IntermediateResult<double> qGamma { get; set; }

        private IntermediateResult<double> qAlpha { get; set; }

        private IntermediateResult<double> Qch { get; set; }

        private IteratableResult<double> Ti { get; set; }

        private IteratableResult<double> Ni { get; set; }

        private FinalResult<double> Q { get; set; }

        private FinalResult<double> T { get; set; }

        private FinalResult<double> N { get; set; }

        private double ProgressIndicatorIncrement { get; set; }

        private Action<double> ProgressIndicatorIncrementor;

        public event Action<IEnumerable<Parameter>> SetParametersValues;

        private CancellationToken CancellationToken { get; set; }

        public async Task<CalculationResults> CalculateAsync(CalculationParameters calculationParameters)
        {
            var calculationResults = new CalculationResults
            {
                ResultsTable = new List<(double coordinate, double tempreture, double viscosity)>()
            };

            ProgressIndicatorIncrementor = calculationParameters.ProgressIncrementor;

            CancellationToken = calculationParameters.CancellationToken;
            (IEnumerable<ResultBase> intermediateResults, IEnumerable<IteratableResultBase> iteratableResults, IEnumerable<FinalResultBase> finalResults) = SetParameters();

            int stepsCount = (int)(L / step);

            ProgressIndicatorIncrement =
                (double)calculationParameters.ProgressMaxValueForCalculation /
                (intermediateResults.Count() + finalResults.Count() + stepsCount * iteratableResults.Count());

            foreach (ResultBase intermediateResult in intermediateResults)
                await CalculateParameterAsync(intermediateResult);

            for (double z = 0; z <= L; z += step)
            {
                try
                {
                    await CalculateParameterAsync(Ti, z);

                    await CalculateParameterAsync(Ni, z);
                }
                catch
                {
                    throw;
                }

                calculationResults.ResultsTable.Add((z, Ti[z], Ni[z]));
            }

            foreach (ResultBase finalResult in finalResults)
                await CalculateParameterAsync(finalResult);

            calculationResults.CanalProductivity = Q;
            calculationResults.QualityIndicators = (T, N);

            return calculationResults;
        }

        private async Task CalculateParameterAsync(ResultBase interMediateResult)
        {
            try
            {
                CancellationToken.ThrowIfCancellationRequested();
            }
            catch { throw; }

            interMediateResult.CalculateValue();

            await Task.Run(() => ProgressIndicatorIncrementor(ProgressIndicatorIncrement));
        }

        private async Task CalculateParameterAsync(IteratableResultBase iteratableResult, object step)
        {
            try
            {
                CancellationToken.ThrowIfCancellationRequested();
            }
            catch { throw; }

            iteratableResult.CalculateAndAddValue(step);

            await Task.Run(() => ProgressIndicatorIncrementor(ProgressIndicatorIncrement));
        }

        private (IEnumerable<ResultBase> intermediateResults, IEnumerable<IteratableResultBase> iteratableResults, IEnumerable<FinalResultBase> finalResults)
            SetParameters()
        {
            List<Parameter> parameters = new();

            foreach (PropertyInfo parameterProp in this.GetType().GetRuntimeProperties().Where(prop => prop.GetValue(this) is Parameter parameter))
            {
                parameters.Add((Parameter)parameterProp.GetValue(this));
            }

            SetParametersValues(parameters);

            F = new IntermediateResult<double>(() => 0.125 * Pow(H / W, 2)
                - 0.625 * (H / W) + 1);

            gamma = new IntermediateResult<double>(() => Vu / H);

            qGamma = new IntermediateResult<double>(() => H * W * u0 * Pow(gamma, n + 1));

            qAlpha = new IntermediateResult<double>(() => W * au * ((1 / b) - Tu - Tr));

            Qch = new IntermediateResult<double>(() => ((H * W * Vu) / 2) * F);

            Ti = new IteratableResult<double>(z => Tr + (1 / b) * Log(((b * qGamma + W * au) / (b * qAlpha)) * (1 - Exp(-(((double)z * b * qAlpha) / (p * c * Qch)))) +
                    Exp(b * (T0 - Tr - ((double)z * qAlpha) / (p * c * Qch)))));

            Ni = new IteratableResult<double>(z => u0 * Exp(-b * (Ti[z] - Tr)) * Pow(gamma, n - 1));

            Q = new FinalResult<double>(() => p * Qch);

            T = new FinalResult<double>(() => Ti.Values.Last().Value);

            N = new FinalResult<double>(() => Ni.Values.Last().Value);

            IEnumerable<ResultBase> intermediateResults = this.GetType().GetRuntimeProperties()
                .Where(prop => prop.GetValue(this) is ResultBase and not FinalResultBase)
                .Select(prop => (ResultBase)prop.GetValue(this));

            IEnumerable<IteratableResultBase> iteratableResults = this.GetType().GetRuntimeProperties()
                .Where(prop => prop.GetValue(this) is IteratableResultBase)
                .Select(prop => (IteratableResultBase)prop.GetValue(this));

            IEnumerable<FinalResultBase> finalResults = this.GetType().GetRuntimeProperties()
                .Where(prop => prop.GetValue(this) is FinalResultBase)
                .Select(prop => (FinalResultBase)prop.GetValue(this));

            return (intermediateResults, iteratableResults, finalResults);
        }

        abstract class ResultBase
        {
            public abstract void CalculateValue();
        }

        class IntermediateResult<ValueType> : ResultBase
        {
            public IntermediateResult(Func<ValueType> calculationFunc)
            {
                CalculationFunc = calculationFunc;
            }

            public Func<ValueType> CalculationFunc { get; set; }

            public ValueType Value { get; private set; }

            public static implicit operator ValueType(IntermediateResult<ValueType> InterMediateResult) => InterMediateResult.Value;

            public override void CalculateValue()
            {
                Value = CalculationFunc();
            }
        }

        abstract class IteratableResultBase
        {
            public abstract void CalculateAndAddValue(object step);
        }

        class IteratableResult<ValueType> : IteratableResultBase
        {
            public ValueType this[object step] { get => Values[step]; }

            public IteratableResult(Func<object, ValueType> calculationFunc)
            {
                CalculationFunc = calculationFunc;
            }

            public Func<object, ValueType> CalculationFunc { get; set; }

            public Dictionary<object, ValueType> Values { get; } = new();

            public override void CalculateAndAddValue(object step)
            {
                ValueType calculatedValue = CalculationFunc(step);

                Values.Add(step, calculatedValue);
            }
        }

        abstract class FinalResultBase : ResultBase { }

        class FinalResult<ValueType> : FinalResultBase
        {
            public FinalResult(Func<ValueType> calculationFunc)
            {
                CalculationFunc = calculationFunc;
            }

            public Func<ValueType> CalculationFunc { get; set; }

            public ValueType Value { get; private set; }

            public static implicit operator ValueType(FinalResult<ValueType> InterMediateResult) => InterMediateResult.Value;

            public override void CalculateValue()
            {
                Value = CalculationFunc();
            }
        }

#nullable enable
        record Parameter<ValueType> : Parameter
        {
            public Parameter(long? ParameterId, string Name, string Designation, string MeasureUnit, ValueType? Value)
                : base(ParameterId, Name, Designation, MeasureUnit, Value)
            {
            }

            public ValueType? ParametrizedValue { get => (ValueType?)Value; set => Value = value; }

            public static implicit operator ValueType(Parameter<ValueType> parameter) => parameter.ParametrizedValue!;
        }
    }
}