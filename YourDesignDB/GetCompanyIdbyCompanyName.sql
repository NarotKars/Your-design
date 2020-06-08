CREATE PROCEDURE [dbo].[GetCompanyIdbyCompanyName]
	@name VARCHAR(30),
	@id INT = 0
AS
BEGIN
	SET NOCOUNT ON;
	SELECT @id=Id FROM Users WHERE [Name]=@name
	RETURN @id
END
