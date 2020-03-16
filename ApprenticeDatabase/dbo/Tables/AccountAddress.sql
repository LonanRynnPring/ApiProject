CREATE TABLE [dbo].[AccountAddress] 
(
	[AccountId] INT NOT NULL,
	[AddressId] INT NOT NULL,
	CONSTRAINT [FK_AccountAddress_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Account] ([AccountId]),
	CONSTRAINT [FK_AccountAddress_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([AddressId]),
)
GO