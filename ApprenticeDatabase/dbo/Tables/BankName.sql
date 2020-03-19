CREATE TABLE [dbo].[Bank]
(
	[BankId] INT NOT NULL,
	[BankName] NVARCHAR(50) NOT NULL,	
    CONSTRAINT [PK_Bank] PRIMARY KEY ([BankId])
);
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The primary key and identity value of the BankName row.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Bank',
    @level2type = N'COLUMN',
    @level2name = N'BankId';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The bank name of the Bank.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Bank',
    @level2type = N'COLUMN',
    @level2name = N'BankName';
GO