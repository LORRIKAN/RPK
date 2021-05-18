SELECT Canal.CanalId, Canal.Brand, Parameter.Name, CanalGeometryParameters.ParameterValue, Parameter.MeasureUnit
FROM Canal
JOIN CanalGeometryParameters ON Canal.CanalId = CanalGeometryParameters.CanalId
JOIN Parameter ON Parameter.ParameterId = CanalGeometryParameters.ParameterId