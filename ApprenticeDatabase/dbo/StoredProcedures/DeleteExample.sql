/*
========================================================================================================================
Author:			Patrick McHugh
Create date:	09/03/2020
Description:	Deletes an example.

Sample call:
	DECLARE @ExampleId INT = (SELECT TOP 1 [ExampleId] FROM [dbo].[Example] ORDER BY 1 DESC)
	EXEC [dbo].[DeleteExample] @ExampleId
========================================================================================================================
*/
CREATE PROCEDURE [dbo].[DeleteExample]
	@ExampleId INT
AS
BEGIN
	DELETE
	FROM [dbo].[Example]
	WHERE [ExampleId] = @ExampleId
END