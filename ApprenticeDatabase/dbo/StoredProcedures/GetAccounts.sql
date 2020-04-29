/*
========================================================================================================================
Author:			Lonan Rynn-Pring
Create date:	03/04/2020
Description:	Gets all accounts.

Sample call:
	EXEC [dbo].[GetAccounts]
========================================================================================================================
*/
CREATE PROCEDURE [dbo].[GetAccounts]
AS
BEGIN
	SELECT
		[AccountId],
		[FirstName], 
		[Surname], 
		[Title], 
		[Email],
		[DateCreated],
		[DateLastUpdated]
	FROM [dbo].[Account]
END