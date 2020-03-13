CREATE TABLE [dbo].[Account] 
(
	[AccountId] INT NOT NULL IDENTITY(1,1),
	[CardId] INT,
	[FirstName] NVARCHAR(50) NOT NULL,
	[Surname] NVARCHAR(50) NOT NULL,
	[Title] NVARCHAR(50) NOT NULL,
	[Email] NVARCHAR(255) NOT NULL,	
	CONSTRAINT [PK_AccountId] PRIMARY KEY ([AccountId]),
	CONSTRAINT [FK_Account_CardId] FOREIGN KEY ([CardId]) REFERENCES [Card] ([CardId])	
)
GO