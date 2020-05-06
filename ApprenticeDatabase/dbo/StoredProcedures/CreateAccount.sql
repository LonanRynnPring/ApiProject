/*
========================================================================================================================
Author:			Lonan Rynn-Pring
Create date:	02/04/2020
Description:	Creates an account.

Sample call:
	DECLARE @FirstName NVARCHAR(50) = 'Jane';
	DECLARE @Surname NVARCHAR(50) = 'Roe';
	DECLARE @Title NVARCHAR(50) = 'Ms';
	DECLARE @Email NVARCHAR(255) = 'janeroe@email.com';
	EXEC [dbo].[CreateAccount] @FirstName, @Surname, @Title, @Email;
========================================================================================================================
*/
CREATE PROCEDURE [dbo].[CreateAccount]
	@FirstName NVARCHAR(50),
	@Surname NVARCHAR(50),
	@Title NVARCHAR(50),
	@Email NVARCHAR(255)
AS
BEGIN
	INSERT INTO [dbo].[Account]
	([FirstName], [Surname], [Title], [Email])
	VALUES 
	(@FirstName, @Surname, @Title, @Email);

	RETURN SCOPE_IDENTITY();
END