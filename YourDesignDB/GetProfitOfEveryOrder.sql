CREATE PROCEDURE [dbo].[GetProfitOfEveryOrder]
AS
	SET NOCOUNT ON;
	SELECT SUM(SellingPrice-BuyingPrice) as OrderProfit
	FROM ProductsInOrders JOIN Products ON Products.Id=ProductsInOrders.ProductId
	GROUP BY OrderId
GO