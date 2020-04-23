/*
========================================================================================================================
Author:			Lonan Rynn-Pring
Create date:	02/04/2020
Description:	Updates an account.

Sample call:
	DECLARE @AccountId INT = (SELECT TOP 1 [AccountId] FROM [dbo].[Account] ORDER BY 1 DESC);
	DECLARE @FirstName NVARCHAR(50) = 'Jess';
	DECLARE @Title NVARCHAR(50) = 'Mrs';
	EXEC [dbo].[UpdateAccount] @AccountId, @FirstName, @Title
========================================================================================================================
*/
CREATE PROCEDURE [dbo].[UpdateAccount]
	@AccountId INT,
	@FirstName NVARCHAR(50) = null,
	@Surname NVARCHAR(50) = null,
	@Title NVARCHAR(50) = null,
	@Email NVARCHAR(255) = null
AS
BEGIN
	
	UPDATE [dbo].[Account]
	SET [FirstName] = ISNULL(@FirstName, FirstName),
		[Surname] = ISNULL(@Surname, Surname),
		[Title] = ISNULL(@Title, Title),
		[Email] = ISNULL(@Email, Email),
		[DateLastUpdated] = GETUTCDATE()
	WHERE [AccountId] = @AccountId
		
END