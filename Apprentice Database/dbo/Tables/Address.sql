﻿CREATE TABLE [dbo].[Address] 
(
	[AddressId] INT NOT NULL IDENTITY(1,1),
	[FirstLine] NVARCHAR(50) NOT NULL,
	[SecondLine] NVARCHAR(50),
	[Postcode] NVARCHAR(50) NOT NULL,
	[City] NVARCHAR(50),
	CONSTRAINT [PK_AddressId] PRIMARY KEY ([AddressId])
)
GO