USE MagnaDB;
GO

-- Functional Test registers (D103 to D156)
UPDATE PlcRegisterMappings
SET LogGroup = 'FT'
WHERE RegisterAddress BETWEEN 'D103' AND 'D156';

-- Travel & Endurance Test registers (D157 to D192)
UPDATE PlcRegisterMappings
SET LogGroup = 'TET'
WHERE RegisterAddress BETWEEN 'D157' AND 'D192';

-- Verify
SELECT RegisterAddress, ParameterName, ValueType, LogGroup
FROM PlcRegisterMappings
ORDER BY Id;
GO