/*
========================================================================================================================
Author:			Patrick McHugh
Create date:	09/03/2020
Description:	Gets an example by the Id.

Sample call:
	DECLARE @ExampleId INT = (SELECT TOP 1 [ExampleId] FROM [dbo].[Example] ORDER BY 1 DESC)
	EXEC [dbo].[GetExampleById] @ExampleId
========================================================================================================================
*/
CREATE PROCEDURE [dbo].[GetExampleById]
	@ExampleId INT
AS
BEGIN
	SELECT
		[ExampleId],
		[Value]
	FROM [dbo].[Example]
	WHERE [ExampleId] = @ExampleId
END