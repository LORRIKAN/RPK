using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using RPK.View;

namespace RPK.Presenter
{
    public class FileExportService
    {
        public async Task<bool> ExportToExcelFileAsync(DataToExport dataToExport, string filePath)
        {
            bool success = false;

            await Task.Run(() =>
            {
                try
                {
                    using var wb = new XLWorkbook();
                    IXLWorksheet ws = wb.AddWorksheet();

                    string canalType = dataToExport.CanalCharacteristics.canalType;

                    IList<Parameter> canalGeometryParameters = dataToExport.CanalCharacteristics.Item2;

                    ws.Cell("A2").SetValue("Тип канала");
                    ws.Cell("A3").SetValue(canalType);

                    for (int i = 0; i < canalGeometryParameters.Count; i++)
                    {
                        ws.Cell(2, i + 4).SetValue(canalGeometryParameters[i].Name);
                        ws.Cell(3, i + 4).SetValue(canalGeometryParameters[i].Value + $" {canalGeometryParameters[i].MeasureUnit}");
                    }

                    string materialType = dataToExport.MaterialCharacteristics.materialType;

                    IList<Parameter> materialPropertyParameters = dataToExport.MaterialCharacteristics.Item2;

                    ws.Cell("A5").SetValue("Тип материала");
                    ws.Cell("A6").SetValue(materialType);

                    for (int i = 0; i < materialPropertyParameters.Count; i++)
                    {
                        ws.Cell(5, i + 4).SetValue(materialPropertyParameters[i].Name);
                        ws.Cell(6, i + 4).SetValue(materialPropertyParameters[i].Value + $" {materialPropertyParameters[i].MeasureUnit}");
                    }

                    IXLColumn lastMergedColumn = ws.LastColumnUsed();

                    ws.Range(ws.Cell(1, 1), ws.LastColumnUsed().Cell(1)).Merge().SetValue("Входные параметры");

                    IXLCell xLCell = ws.LastColumnUsed().ColumnRight().ColumnRight().Cell(2);

                    IList<Parameter> variableParameters = dataToExport.VariableParameters;

                    for (int i = 0; i < variableParameters.Count; i++)
                    {
                        xLCell.SetValue(variableParameters[i].Name);
                        xLCell = xLCell.CellBelow().SetValue(variableParameters[i].Value + $" {variableParameters[i].MeasureUnit}");

                        xLCell = xLCell.CellRight().CellAbove();
                    }


                    ws.Range(lastMergedColumn.ColumnRight().ColumnRight().Cell(1),
                        ws.LastColumnUsed().Cell(1)).Merge().SetValue("Варьируемые параметры");

                    lastMergedColumn = ws.LastColumnUsed();

                    xLCell = ws.LastColumnUsed().ColumnRight().ColumnRight().Cell(2);

                    IList<Parameter> empiricalParametersOfMathModel = dataToExport.EmpiricalParametersOfMathModel;

                    for (int i = 0; i < empiricalParametersOfMathModel.Count; i++)
                    {
                        xLCell.SetValue(empiricalParametersOfMathModel[i].Name);
                        xLCell = xLCell.CellBelow().SetValue(empiricalParametersOfMathModel[i].Value + $" {empiricalParametersOfMathModel[i].MeasureUnit}");

                        xLCell = xLCell.CellRight().CellAbove();
                    }

                    ws.Range(lastMergedColumn.ColumnRight().ColumnRight().Cell(1),
                        ws.LastColumnUsed().Cell(1)).Merge().SetValue("Эмпирические коэффициенты математической модели");

                    IDictionary<string, IList<Parameter>> discreteOutputParameters = dataToExport.DiscreteOutputParameters;

                    xLCell = xLCell.CellBelow().CellBelow().CellBelow().CellBelow().CellBelow().CellBelow().CellBelow().CellBelow()
                        .WorksheetRow().Cell(1);

                    foreach (KeyValuePair<string, IList<Parameter>> keyValuePair in discreteOutputParameters)
                    {
                        IXLCell firstCell = xLCell;
                        foreach (Parameter parameter in keyValuePair.Value)
                        {
                            xLCell.SetValue(parameter.Name);
                            xLCell = xLCell.CellBelow().SetValue($"{parameter.Value} {parameter.MeasureUnit}");

                            xLCell = xLCell.CellAbove().CellRight();
                        }

                        if (keyValuePair.Value.Count <= 0)
                            continue;

                        if (keyValuePair.Value.Count == 1)
                        {
                            xLCell = xLCell.CellRight();
                            continue;
                        }

                        ws.Range(firstCell.CellAbove(), ws.LastRowUsed().LastCellUsed().CellAbove().CellAbove()).
                            Merge().SetValue(keyValuePair.Key);

                        xLCell = xLCell.CellRight();
                    }

                    xLCell = xLCell.CellLeft().CellLeft().CellAbove().CellAbove();

                    ws.Range(xLCell, xLCell.WorksheetRow().Cell(1)).Merge().SetValue("Результаты");

                    xLCell = ws.LastRowUsed().Cell(1).CellBelow().CellBelow();

                    ws.Range(xLCell, xLCell.CellRight().CellRight().CellRight()).Merge().SetValue("Таблица результатов");

                    IList<(Parameter coordinate, Parameter temperature, Parameter viscosity)> resultsTable = dataToExport.ContiniousResults;

                    xLCell.CellBelow().SetValue($"{resultsTable[0].coordinate.Name}, {resultsTable[0].coordinate.MeasureUnit}")
                        .CellRight().SetValue($"{resultsTable[0].temperature.Name}, {resultsTable[0].temperature.MeasureUnit}")
                        .CellRight().SetValue($"{resultsTable[0].viscosity.Name}, {resultsTable[0].viscosity.MeasureUnit}");

                    xLCell = xLCell.CellBelow().CellBelow();

                    NumberFormatInfo nfi = new NumberFormatInfo
                    {
                        NumberDecimalSeparator = "."
                    };

                    foreach (var (coordinate, temperature, viscosity) in resultsTable)
                    {
                        xLCell.SetValue(((double)coordinate.Value)
                            .ToString($"F{dataToExport.CoordinatePrecision}", nfi))
                            .SetDataType(XLDataType.Number)
                            .CellRight().SetValue(((double)temperature.Value).ToString($"F2", nfi))
                            .SetDataType(XLDataType.Number)
                            .CellRight().SetValue(((double)viscosity.Value).ToString("F2", nfi))
                            .SetDataType(XLDataType.Number);

                        xLCell = xLCell.CellBelow();
                    }

                    //ws.RowsUsed().AdjustToContents();
                    //ws.ColumnsUsed().AdjustToContents();

                    //using var stream = new MemoryStream();
                    //dataToExport.TemperaturePlot.Save(stream, ImageFormat.Png);
                    //ws.AddPicture(stream).MoveTo(ws.Cell("H8"))
                    //    .WithSize(dataToExport.TemperaturePlot.Width, dataToExport.TemperaturePlot.Height);

                    //using var stream1 = new MemoryStream();
                    //dataToExport.ViscosityPlot.Save(stream1, ImageFormat.Png);
                    //ws.AddPicture(stream1).MoveTo(ws.Cell("O8"))
                    //    .WithSize(dataToExport.ViscosityPlot.Width, dataToExport.ViscosityPlot.Height);

                    wb.SaveAs(filePath);

                    success = true;
                }
                catch { success = false; }
            });

            return success;
        }
    }
}