/*
========================================================================================================================
Author:			Lonan Rynn-Pring
Create date:	02/04/2020
Description:	Deletes an account.

Sample call:
	DECLARE @AccountId INT = (SELECT TOP 1 [AccountId] FROM [dbo].[Account] ORDER BY 1 DESC)
	EXEC [dbo].[DeleteAccount] @AccountId
========================================================================================================================
*/
CREATE PROCEDURE [dbo].[DeleteAccount]
	@AccountId INT
AS
BEGIN
	DELETE
	FROM [dbo].[Account]
	WHERE [AccountId] = @AccountId
END