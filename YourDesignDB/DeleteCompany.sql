CREATE PROCEDURE [dbo].[DeleteCompany]
	@id BIGINT
AS
	UPDATE Users set IsValid='deleted' WHERE Id=@id

	UPDATE Images SET [Status]='deleted' FROM Images JOIN Products ON Images.Id=Products.ImageId WHERE Products.CompanyId=@id
RETURN 0
