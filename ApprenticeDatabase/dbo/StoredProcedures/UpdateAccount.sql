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
	@FirstName NVARCHAR(50),
	@Surname NVARCHAR(50),
	@Title NVARCHAR(50),
	@Email NVARCHAR(255)
AS
BEGIN
	IF @FirstName IS NOT NULL
	BEGIN
		UPDATE [dbo].[Account]
		SET [FirstName] = @FirstName,
			[DateLastUpdated] = GETUTCDATE()
		WHERE [AccountId] = @AccountId
	END

	IF @Surname IS NOT NULL
	BEGIN
		UPDATE [dbo].[Account]
		SET [Surname] = @Surname,
			[DateLastUpdated] = GETUTCDATE()
		WHERE [AccountId] = @AccountId
	END

	IF @Title IS NOT NULL
	BEGIN
		UPDATE [dbo].[Account]
		SET [Title] = @Title,
			[DateLastUpdated] = GETUTCDATE()
		WHERE [AccountId] = @AccountId
	END

	IF @Email IS NOT NULL
	BEGIN
		UPDATE [dbo].[Account]
		SET [Email] = @Email,
			[DateLastUpdated] = GETUTCDATE()
		WHERE [AccountId] = @AccountId
	END
		
END