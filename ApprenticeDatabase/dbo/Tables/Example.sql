CREATE TABLE [dbo].[Example] 
(
	[ExampleId] INT NOT NULL IDENTITY(1,1),
    [Value] NVARCHAR (250) NOT NULL,
    CONSTRAINT [PK_ExampleId] PRIMARY KEY ([ExampleId])
)
GO