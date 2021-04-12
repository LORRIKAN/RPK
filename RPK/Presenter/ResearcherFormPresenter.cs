using RPK.Model;
using RPK.Repository;
using RPK.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RPK.Presenter
{
    public class ResearcherFormPresenter
    {
        public ResearcherForm ResearcherForm { get; set; }
        public DatabaseContext Repository { get; set; }

        public ResearcherFormPresenter(ResearcherForm researcherForm, DatabaseContext repository)
        {
            Repository = repository;

            ResearcherForm = researcherForm;
            ResearcherForm.SetVariableParameters += ResearcherForm_SetVariableParameters;
            ResearcherForm.SetSolvingParameters += ResearcherForm_SetSolvingParameters;
        }

        private IEnumerable<View.Parameter> ResearcherForm_SetSolvingParameters(Material material, Canal canal)
        {
            double canalLength = canal.CanalGeometryParameters.
                FirstOrDefault(p => p.Parameter is { Name: "Длина"} or { Designation: "L" } )
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