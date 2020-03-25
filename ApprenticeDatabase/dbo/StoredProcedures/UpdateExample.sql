/*
========================================================================================================================
Author:			Patrick McHugh
Create date:	09/03/2020
Description:	Updates an example.

Sample call:
	DECLARE @ExampleId INT = (SELECT TOP 1 [ExampleId] FROM [dbo].[Example] ORDER BY 1 DESC);
	DECLARE @Value NVARCHAR(250) = 'Hello there.';
	EXEC [dbo].[UpdateExample] @ExampleId, @Value
========================================================================================================================
*/
CREATE PROCEDURE [dbo].[UpdateExample]
	@ExampleId INT,
	@Value NVARCHAR(250)
AS
BEGIN
	UPDATE [dbo].[Example]
	SET
		[Value] = @Value
	WHERE [ExampleId] = @ExampleId
END