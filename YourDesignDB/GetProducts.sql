CREATE PROCEDURE [dbo].[GetProducts]
	@productId BIGINT = NULL
AS
	IF(@productId) IS NULL
		SELECT Products.Id, Users.[Name] as CompanyName, Photo, SellingPrice, Images.Status, Categories.Name as CategoryName, BuyingPrice, Categories.Id as CategoryId, Users.Id as CompanyId
		FROM Products JOIN Images ON Images.Id=Products.ImageId
		JOIN Users ON Users.Id=Products.CompanyId
		JOIN Categories ON Categories.Id=Products.CategoryId
		WHERE [Status]!='deleted' AND [Status]!='customer' AND IsValid='notDeleted'
	ELSE
		SELECT Products.Id, Users.[Name] as CompanyName, Photo, SellingPrice, Images.Status, BuyingPrice, Categories.Id as CategoryId, Users.Id as CompanyId
		FROM Products JOIN Images ON Images.Id=Products.ImageId
		JOIN Users ON Users.Id=Products.CompanyId
		JOIN Categories ON Categories.Id=Products.CategoryId
		WHERE [Status]!='deleted' AND Products.Id=@productId AND IsValid='notDeleted'
GO
