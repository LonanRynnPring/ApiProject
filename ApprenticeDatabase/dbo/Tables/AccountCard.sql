CREATE TABLE [dbo].[AccountCard] 
(
	[AccountId] INT NOT NULL,
	[CardId] INT NOT NULL,
	[DateCreated] DATETIME2(7) CONSTRAINT [DF_AccountCard_DateCreated] DEFAULT (GETUTCDATE()) NOT NULL,
	[DateLastUpdated] DATETIME2(7) CONSTRAINT [DF_AccountCard_DateLastUpdated] DEFAULT (GETUTCDATE()) NOT NULL,
	CONSTRAINT [FK_AccountCard_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([AccountId]),
	CONSTRAINT [FK_AccountCard_AddressId] FOREIGN KEY ([CardId]) REFERENCES [Card] ([CardId])
)
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The foreign key that corresponds to the Account.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountCard',
    @level2type = N'COLUMN',
    @level2name = N'AccountId';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The foreign key that corresponds to the Card.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountCard',
    @level2type = N'COLUMN',
    @level2name = N'CardId';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The UTC datetime of the creation of the link between Account and Card.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountCard',
    @level2type = N'COLUMN',
    @level2name = N'DateCreated';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The UTC datetime of the last update of the link between Account and Card.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountCard',
    @level2type = N'COLUMN',
    @level2name = N'DateLastUpdated';
GO