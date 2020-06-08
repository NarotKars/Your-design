CREATE PROCEDURE [dbo].[GetImagesByCompany]
	@companyId int
AS
	SET NOCOUNT ON;
	select Images.Id, Status, isnull(Text, 'your text here') as Text, Photo from Images join Products on Images.Id=Products.ImageId 
	join Users on Products.CompanyId=Users.Id
	where Images.[Status]!='deleted' and Users.Id=@companyId
GO
