CREATE PROCEDURE [dbo].[InsertProductsInOrders]
	@customerId BIGINT,
	@orderId BIGINT = NULL,
	@productId BIGINT,
	@id BIGINT = NULL OUTPUT,
	@price DECIMAL = NULL
AS
	SET NOCOUNT ON;
	SELECT @orderId=Id FROM Orders WHERE Status='notOrdered' and CustomerId=@customerId
	INSERT INTO ProductsInOrders(OrderId,ProductId,[Status])
	VALUES(@orderId,@productId,'new')
	SET @id=SCOPE_IDENTITY()

	SELECT @price=SellingPrice FROM Products WHERE Id=@productId
	UPDATE Orders SET Amount = Amount + @price WHERE Id=@orderId

GO