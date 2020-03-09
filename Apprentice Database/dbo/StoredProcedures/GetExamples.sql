/*
========================================================================================================================
Author:			Patrick McHugh
Create date:	09/03/2020
Description:	Gets all examples.

Sample call:
	EXEC [dbo].[GetExamples]
========================================================================================================================
*/
CREATE PROCEDURE [dbo].[GetExamples]
AS
BEGIN
	SELECT
		[ExampleId],
		[Value]
	FROM [dbo].[Example]
END