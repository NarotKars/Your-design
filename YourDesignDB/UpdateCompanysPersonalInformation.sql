CREATE PROCEDURE [dbo].[UpdateCompanysPersonalInformation]
	@id BIGINT,
	@name VARCHAR(30),
	@phoneNum VARCHAR(13)
AS
	UPDATE Users SET PhoneNumber=@phoneNum WHERE Id=@id
	UPDATE Companies SET [Name]=@name WHERE Id=@id
GO