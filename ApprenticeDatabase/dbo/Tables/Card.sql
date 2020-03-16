CREATE TABLE [dbo].[Card] 
(
	[CardId] INT NOT NULL IDENTITY(1,1),
	[AccountNumber] BIGINT	NOT NULL,
	[Sortcode] INT NOT NULL,
	[CardType] NVARCHAR(50) NOT NULL,
	[BankNameId] INT NOT NULL,
	CONSTRAINT [PK_CardId] PRIMARY KEY ([CardId]),
	CONSTRAINT [FK_BankName_BankNameId] FOREIGN KEY ([BankNameId]) REFERENCES [BankName] ([BankNameId])	
)
GO