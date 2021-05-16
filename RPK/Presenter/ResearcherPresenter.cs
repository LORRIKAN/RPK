using RPK.Model.MathModel;
using RPK.Model.Users;
using RPK.Presenter;
using RPK.Repository.MathModel;
using RPK.Researcher.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPK.Researcher.Presenter
{
    public class ResearcherPresenter : RolePresenterBase
    {
        private ResearcherForm ResearcherForm { get; set; }

        public override Form Form { get => ResearcherForm; }

        private MathModelContext Repository { get; set; }

        private MathModel MathModel { get; set; }

        private FileExportService FileExportService { get; set; }

        private IEnumerable<View.Parameter> Parameters { get; set; }

        public ResearcherPresenter(ResearcherForm researcherForm, MathModelContext repository, MathModel mathModel,
            FileExportService fileExportService, Role role)
        {
            FileExportService = fileExportService;
            Repository = repository;
            MathModel = mathModel;
            MathModel.SetParametersValues += MathModel_SetParametersValues;

            ResearcherForm = researcherForm;
            ResearcherForm.SetVariableParameters += ResearcherForm_SetVariableParameters;
            ResearcherForm.SetSolvingParameters += ResearcherForm_SetSolvingParameters;
            ResearcherForm.CalculationRequiredAsync += ResearcherForm_CalculationRequiredAsync;
            ResearcherForm.ExportToFileAsync += ResearcherForm_ExportToFileAsync;
            ResearcherForm.SetAllocatedMemory += ResearcherForm_SetAllocatedMemory;

            Role = role;
        }

        public override event Action ReloginRequired;

        private async Task<bool> ResearcherForm_ExportToFileAsync(DataToExport dataToExport, string filePath)
        {
            return await FileExportService.ExportToExcelFileAsync(dataToExport, filePath);
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
            CanalGeometryParameter canalGeometryParameter = canal.CanalGeometryParameters.
                FirstOrDefault(p => p.Parameter is { Name: "Длина" } or { Designation: "L" });

            if (canalGeometryParameter is null)
                yield break;

            double canalLength = canalGeometryParameter.ParameterValue;

            yield return new ParameterWithBounds
                (
                    ParameterId: 0,
                    Name: "Шаг решения",
                    Designation: "d",
                    MeasureUnit: "м",
                    Value: canalLength / 20,
                    LowerBound: 0,
                    UpperBound: canalLength,
                    ShowBounds: false
                );
        }

        private IEnumerable<VariableParameter> ResearcherForm_SetVariableParameters(Material material, Canal canal)
        {
            return Repository.VariableParameters
                .Where(variableParameter => variableParameter.MaterialId == material.Id &&
                    variableParameter.CanalId == canal.Id);
        }

        public override Form Run()
        {
            ResearcherForm.SetInitialData(Repository.Canals, Repository.Materials);
            ResearcherForm.ReloginRequired += ReloginRequired;
            return base.Run();
        }

        public override Form Run(User user)
        {
            ResearcherForm.SetUserDescription(user.Login, user.Role.RoleName);

            return this.Run();
        }
    }
}