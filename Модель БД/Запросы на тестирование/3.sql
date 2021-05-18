SELECT Canal.Brand, Material.Name, Parameter.Name, VariableParameter.ValueLowerBound, VariableParameter.ValueUpperBound, 
Parameter.MeasureUnit
FROM Canal
JOIN VariableParameter ON Canal.CanalId = VariableParameter.CanalId
JOIN Parameter ON Parameter.ParameterId = VariableParameter.ParameterId
JOIN Material ON VariableParameter.MaterialId = Material.MaterialId