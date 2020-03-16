/*
========================================================================================================================
Author:			Patrick McHugh
Create date:	04/03/2020
Description:	Creates an example.

Sample call:
	DECLARE @Value NVARCHAR(250) = 'Hello there.';
	EXEC [dbo].[CreateExample] @Value
========================================================================================================================
*/
CREATE PROCEDURE [dbo].[CreateExample]
	@Value NVARCHAR(250)
AS
BEGIN
	INSERT INTO [dbo].[Example]
	([Value])
	VALUES 
	(@Value);

	RETURN SCOPE_IDENTITY();
END