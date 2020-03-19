CREATE TABLE [dbo].[Address] 
(
	[AddressId] INT NOT NULL IDENTITY(1,1),
	[FirstLine] NVARCHAR(250) NOT NULL,
	[SecondLine] NVARCHAR(250) NOT NULL,
	[Postcode] NVARCHAR(50) NOT NULL,
	[City] NVARCHAR(50) NOT NULL,
	[DateCreated] DATETIME2(7) CONSTRAINT [DF_Address_DateCreated] DEFAULT (GETUTCDATE()) NOT NULL,
	[DateLastUpdated] DATETIME2(7) CONSTRAINT [DF_Address_DateLastUpdated] DEFAULT (GETUTCDATE()) NOT NULL,
	CONSTRAINT [PK_Address] PRIMARY KEY ([AddressId])
)
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The primary key and identity value of the Address row.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Address',
    @level2type = N'COLUMN',
    @level2name = N'AddressId';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The first line of the Address.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Address',
    @level2type = N'COLUMN',
    @level2name = N'FirstLine';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The second line of the Address.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Address',
    @level2type = N'COLUMN',
    @level2name = N'SecondLine';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The postcode of the Address.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Address',
    @level2type = N'COLUMN',
    @level2name = N'Postcode';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The city of the Address.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Address',
    @level2type = N'COLUMN',
    @level2name = N'City';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The UTC datetime of the creation of the Address.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Address',
    @level2type = N'COLUMN',
    @level2name = N'DateCreated';
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The UTC datetime of the last update of the Address.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Address',
    @level2type = N'COLUMN',
    @level2name = N'DateLastUpdated';
GO