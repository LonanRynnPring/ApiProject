CREATE TABLE [dbo].[Account] 
(
	[AccountId] INT NOT NULL IDENTITY(1,1),
	[FirstName] NVARCHAR(50) NOT NULL,
	[Surname] NVARCHAR(50) NOT NULL,
	[Title] NVARCHAR(50) NOT NULL,
	[Email] NVARCHAR(255) NOT NULL,
	[DateCreated] DATETIME2(7) CONSTRAINT [DF_Account_DateCreated] DEFAULT (GETUTCDATE()) NOT NULL,
	[DateLastUpdated] DATETIME2(7) CONSTRAINT [DF_Account_DateLastUpdated] DEFAULT (GETUTCDATE()) NOT NULL,	
	CONSTRAINT [PK_Account] PRIMARY KEY ([AccountId])
)
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The primary key and identity value of the Account row.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Account',
    @level2type = N'COLUMN',
    @level2name = N'AccountId';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The first name of the Account.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Account',
    @level2type = N'COLUMN',
    @level2name = N'FirstName';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The surname of the Account.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Account',
    @level2type = N'COLUMN',
    @level2name = N'Surname';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The title of the Account.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Account',
    @level2type = N'COLUMN',
    @level2name = N'Title';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The email of the Account.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Account',
    @level2type = N'COLUMN',
    @level2name = N'Email';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The UTC datetime of the creation of the Account.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Account',
    @level2type = N'COLUMN',
    @level2name = N'DateCreated';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The UTC datetime of the last update of the Account.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Account',
    @level2type = N'COLUMN',
    @level2name = N'DateLastUpdated';
GO