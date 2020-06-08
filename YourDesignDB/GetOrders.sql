CREATE PROCEDURE [dbo].[GetOrders]
	@date DATETIME2 = NULL,
	@customerId BIGINT = NULL
AS
	SET NOCOUNT ON;
	IF( @date ) IS NOT NULL
	SELECT Orders.Id, OrderDate, Amount, [Status], City, Street, Number
	FROM Orders JOIN Addresses on Addresses.Id=Orders.AddressId
	WHERE Cast(OrderDate as date) = cast(@date as date)
	ELSE IF(@customerId) IS NOT NULL
	SELECT Orders.Id, OrderDate, Amount, [Status], isnull(City,'unknown') as City, isnull(Street, 'unknown') as Street, isnull(Number,'unknown') as Number
	FROM Orders LEFT JOIN Addresses on  Orders.AddressId=Addresses.Id 
	WHERE CustomerId=@customerId
	ELSE
	SELECT Orders.Id, OrderDate, Amount, [Status], City, Street, Number
	FROM Orders JOIN Addresses on Addresses.Id=Orders.AddressId
GO
