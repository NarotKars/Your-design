CREATE PROCEDURE [dbo].[GetCountOfOrdersOfEveryCompany]
AS
	SET NOCOUNT ON;
	SELECT [Name], Count(CompanyId) as countOfOrders
	FROM ProductsInOrders JOIN Products on Products.Id=ProductsInOrders.ProductId 
						  JOIN Users on Users.Id=Products.CompanyId
	GROUP BY [Name]
GO
