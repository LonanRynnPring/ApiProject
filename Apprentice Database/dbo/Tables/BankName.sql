CREATE TABLE [dbo].[BankName]
(
	[BankNameId] INT NOT NULL,
	[BankName] NVARCHAR(50) NOT NULL,	
    CONSTRAINT [PK_BankName] PRIMARY KEY ([BankNameId])
);
GO