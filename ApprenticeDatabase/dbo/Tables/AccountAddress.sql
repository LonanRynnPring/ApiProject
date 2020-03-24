CREATE TABLE [dbo].[AccountAddress] 
(
	[AccountId] INT NOT NULL,
	[AddressId] INT NOT NULL,
	[DateCreated] DATETIME2(7) CONSTRAINT [DF_AccountAddress_DateCreated] DEFAULT (GETUTCDATE()) NOT NULL,
	[DateLastUpdated] DATETIME2(7) CONSTRAINT [DF_AccountAddress_DateLastUpdated] DEFAULT (GETUTCDATE()) NOT NULL,
	CONSTRAINT [FK_AccountAddress_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([AccountId]),
	CONSTRAINT [FK_AccountAddress_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([AddressId])
)
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The foreign key that corresponds to the Account.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountAddress',
    @level2type = N'COLUMN',
    @level2name = N'AccountId';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The foreign key that corresponds to the Address.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountAddress',
    @level2type = N'COLUMN',
    @level2name = N'AddressId';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The UTC datetime of the creation of the link between Account and Address.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountAddress',
    @level2type = N'COLUMN',
    @level2name = N'DateCreated';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The UTC datetime of the last update of the link between Account and Address.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'AccountAddress',
    @level2type = N'COLUMN',
    @level2name = N'DateLastUpdated';
GO