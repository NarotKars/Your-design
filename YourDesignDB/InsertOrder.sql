CREATE PROCEDURE [dbo].[InsertOrder]
	@orderDate DATETIME2,
	@status VARCHAR(15),
	@amount MONEY,
	@customerId BIGINT,
	@id BIGINT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Orders(OrderDate, [Status], Amount, CustomerId)
	VALUES(@orderDate, @status, @amount, @customerId)
	SET @id=SCOPE_IDENTITY()
	RETURN @id
END
