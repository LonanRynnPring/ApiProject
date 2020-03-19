CREATE TABLE [dbo].[Card] 
(
	[CardId] INT NOT NULL IDENTITY(1,1),
	[AccountNumber] BIGINT	NOT NULL,
	[Sortcode] INT NOT NULL,
	[CardType] NVARCHAR(50) NOT NULL,
	[BankId] INT NOT NULL,
	[DateCreated] DATETIME2(7) CONSTRAINT [DF_Card_DateCreated] DEFAULT (GETUTCDATE()) NOT NULL,
	[DateLastUpdated] DATETIME2(7) CONSTRAINT [DF_Card_DateLastUpdated] DEFAULT (GETUTCDATE()) NOT NULL,
	CONSTRAINT [PK_Card] PRIMARY KEY ([CardId]),
	CONSTRAINT [FK_Bank_BankId] FOREIGN KEY ([BankId]) REFERENCES [Bank] ([BankId])	
)
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The primary key and identity value of the Card row.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Card',
    @level2type = N'COLUMN',
    @level2name = N'CardId';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The account number of the Card.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Card',
    @level2type = N'COLUMN',
    @level2name = N'AccountNumber';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The sortcode of the Card.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Card',
    @level2type = N'COLUMN',
    @level2name = N'Sortcode';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The type of the Card.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Card',
    @level2type = N'COLUMN',
    @level2name = N'CardType';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The foreign key that corresponds to the Bank.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Card',
    @level2type = N'COLUMN',
    @level2name = N'BankId';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The UTC datetime of the creation of the Card.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Card',
    @level2type = N'COLUMN',
    @level2name = N'DateCreated';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The UTC datetime of the last update of the Card.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Card',
    @level2type = N'COLUMN',
    @level2name = N'DateLastUpdated';
GO