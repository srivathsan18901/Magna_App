USE MagnaDB;
GO

ALTER TABLE dbo.PlcRegisterMappings
ADD LogPropertyName NVARCHAR(100) NULL;
GO