using RPK.Model;
using RPK.Repository;
using RPK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace RPK.Presenter
{
    public class ResearcherFormPresenter
    {
        public ResearcherForm ResearcherForm { get; set; }

        public DatabaseContext Repository { get; set; }

        public MathModel MathModel { get; set; }

        private IEnumerable<View.Parameter> Parameters { get; set; }

        public ResearcherFormPresenter(ResearcherForm researcherForm, DatabaseContext repository, MathModel mathModel)
        {
            Repository = repository;
            MathModel = mathModel;
            MathModel.SetParametersValues += MathModel_SetParametersValues;

            ResearcherForm = researcherForm;
            ResearcherForm.SetVariableParameters += ResearcherForm_SetVariableParameters;
            ResearcherForm.SetSolvingParameters += ResearcherForm_SetSolvingParameters;
            ResearcherForm.CalculationRequired += ResearcherForm_CalculationRequired;
        }

        private void ResearcherForm_CalculationRequired(ref CalculationParameters calculationParameters, out CalculationResults calculationResults)
        {
            Parameters = calculationParameters.Parameters;

            MathModel.Calculate(ref calculationParameters, out calculationResults);
        }

        private void MathModel_SetParametersValues(IEnumerable<View.Parameter> parametersToSetValues)
        {
            foreach (View.Parameter unsetParameter in parametersToSetValues)
            {
                unsetParameter.Value = Parameters.First(param => param.Name == unsetParameter.Name && param.Designation == unsetParameter.Designation).Value;
            }
        }

        private IEnumerable<View.Parameter> ResearcherForm_SetSolvingParameters(Material material, Canal canal)
        {
            double canalLength = canal.CanalGeometryParameters.
                FirstOrDefault(p => p.Parameter is { Name: "Длина" } or { Designation: "L" })
                .ParameterValue;

            yield return new ParameterWithBounds
                (
                    ParameterId: 0,
                    Name: "Шаг решения",
                    Designation: "d",
                    MeasureUnit: null,
                    Value: null,
                    LowerBound: 0,
                    UpperBound: canalLength
                );
        }

        private IEnumerable<VariableParameter> ResearcherForm_SetVariableParameters(Material material, Canal canal)
        {
            return Repository.VariableParameters
                .Where(variableParameter => variableParameter.MaterialId == material.MaterialId &&
                    variableParameter.CanalId == canal.CanalId);
        }

        [STAThread]
        public void Run()
        {
            ResearcherForm.SetInitialData(Repository.Canals, Repository.Materials);
            Application.Run(ResearcherForm);
        }
    }
}