using RPK.Model;
using RPK.Repository;
using RPK.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            ResearcherForm.CalculationRequired += (calculationParameters) => 
                ResearcherForm_CalculationRequiredAsync(calculationParameters).Result;
            ResearcherForm.SetAllocatedMemory += ResearcherForm_SetAllocatedMemory;
        }

        private long ResearcherForm_SetAllocatedMemory()
        {
            // 1. Obtain the current application process
            Process currentProcess = Process.GetCurrentProcess();

            // 2. Obtain the used memory by the process
            long usedMemory = currentProcess.PrivateMemorySize64;

            return usedMemory;
        }

        private async Task<CalculationResults> ResearcherForm_CalculationRequiredAsync(CalculationParameters calculationParameters)
        {
            Parameters = calculationParameters.Parameters;

            return await MathModel.CalculateAsync(calculationParameters);
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