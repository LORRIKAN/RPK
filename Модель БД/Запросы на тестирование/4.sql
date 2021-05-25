SELECT Material.MaterialId, Material.Name, Parameter.Name, ParameterOfMaterialProperty.ParameterValue, Parameter.MeasureUnit
FROM Material
JOIN ParameterOfMaterialProperty ON Material.MaterialId = ParameterOfMaterialProperty.MaterialId
JOIN Parameter ON Parameter.ParameterId = ParameterOfMaterialProperty.ParameterId
WHERE Material.Name = 'Полистирол'