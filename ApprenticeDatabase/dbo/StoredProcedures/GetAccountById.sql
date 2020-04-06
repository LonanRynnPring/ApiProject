/*
========================================================================================================================
Author:			Lonan Rynn-Pring
Create date:	03/04/2020
Description:	Gets an account by the accountId.

Sample call:
	DECLARE @AccountId INT = (SELECT TOP 1 [AccountId] FROM [dbo].[Account] ORDER BY 1 DESC)
	EXEC [dbo].[GetAccountById] @AccountId
========================================================================================================================
*/
CREATE PROCEDURE [dbo].[GetAccountById]
	@AccountId INT
AS
BEGIN
	SELECT
		[AccountId],
		[FirstName], 
		[Surname], 
		[Title], 
		[Email]
	FROM [dbo].[Account]
	WHERE [AccountId] = @AccountId
END