CREATE PROCEDURE [dbo].[UpdateStatusOfProductsInOrders]
	@status VARCHAR(30),
	@id BIGINT,
	@orderId BIGINT = NULL,
	@checkStatusDone BIGINT = NULL,
	@checkStatusAccepted BIGINT = NULL
AS
	UPDATE ProductsInOrders SET [Status]=@status WHERE Id=@id

	SELECT @orderId=ProductsInOrders.OrderId
	FROM ProductsInOrders
	WHERE Id=@id
	
	SELECT @checkStatusAccepted = ProductsInOrders.Id
	FROM ProductsInOrders
	WHERE OrderId=@orderId and (ProductsInOrders.[Status]='new' or ProductsInOrders.[Status]='done')

	IF (@checkStatusAccepted) IS NULL
	UPDATE Orders SET [Status]='accepted' WHERE Id=@orderId

	SELECT @checkStatusDone = ProductsInOrders.Id
	FROM ProductsInOrders
	WHERE OrderId=@orderId and (ProductsInOrders.Status='new' or ProductsInOrders.Status='accepted')

	IF (@checkStatusDone) IS NULL
	UPDATE Orders SET [Status]='done' WHERE Id=@orderId






GO
