CREATE PROCEDURE [dbo].[GetOrderDetailsByOrderId]
	@orderId BIGINT
AS
	SELECT Products.Id, SellingPrice, Photo, [Name] , ProductsInOrders.Id as productInOrderId FROM ProductsInOrders
	JOIN Products on ProductsInOrders.ProductId=Products.Id
	JOIN Images on Images.Id=Products.ImageId
	JOIN Users on Users.Id=CompanyId
	WHERE OrderId=@orderId
GO
