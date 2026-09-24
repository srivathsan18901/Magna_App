SELECT TOP 5 
    Id, 
    RegisterAddress, 
    UiControlName, 
    LogPropertyName 
FROM dbo.PlcRegisterMappings
ORDER BY Id;
