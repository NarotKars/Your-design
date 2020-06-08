CREATE PROCEDURE [dbo].[GetOrderDetailsByCompany]
	@companyId int
AS
	
	SELECT ProductsInOrders.Id,Photo,ProductsInOrders.[Status], isnull(Orders.PhoneNumber,'unknown') as PhoneNumber, Users.[Name]
	FROM ProductsInOrders
	JOIN Products ON Products.Id=ProductsInOrders.ProductId
	JOIN Images ON Products.ImageId=Images.Id
	JOIN Orders ON Orders.Id=ProductsInOrders.OrderId
	JOIN Users ON Users.Id=Orders.CustomerId
	WHERE Products.CompanyId=@companyId and ( ProductsInOrders.[Status]='new' or ProductsInOrders.[Status]='accepted')
GO
