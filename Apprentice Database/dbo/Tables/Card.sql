﻿CREATE TABLE [dbo].[Card] 
(
	[CardId] INT NOT NULL IDENTITY(1,1),
	[AccountNumber] INT	NOT NULL,
	[Sortcode] INT NOT NULL,
	[CardType] NVARCHAR(50) NOT NULL,
	[BankName] NVARCHAR(50) NOT NULL,
	CONSTRAINT [PK_CardId] PRIMARY KEY ([CardId])
)
GO