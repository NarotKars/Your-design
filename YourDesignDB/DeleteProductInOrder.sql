CREATE PROCEDURE [dbo].[DeleteProductInOrder]
	@id BIGINT,
	@sellingPrice DECIMAL = NULL,
	@orderId BIGINT = NULL,
	@checkOrder BIGINT = NULL
AS
	SELECT @sellingPrice=SellingPrice, @orderId=ProductsInOrders.OrderId
	FROM ProductsInOrders
	JOIN Products On Products.Id=ProductsInOrders.ProductId
	WHERE ProductsInOrders.Id=@id

	
	DELETE FROM ProductsInOrders WHERE Id=@id
	SELECT @checkOrder=ProductsInOrders.OrderId FROM ProductsInOrders WHERE OrderId=@orderId
	IF(@checkOrder) IS NULL
	DELETE FROM Orders WHERE Id=@orderId
	ELSE
	UPDATE Orders SET Amount = (Amount - @sellingPrice) WHERE Id=@orderId
RETURN @id
