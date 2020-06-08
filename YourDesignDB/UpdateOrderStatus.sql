CREATE PROCEDURE [dbo].[UpdateOrderStatus]
	@orderId BIGINT=NULL,
	@status VARCHAR(15),
	@city VARCHAR(20)=NULL,
	@street VARCHAR(30)=NULL,
	@number VARCHAR(10)=NULL,
	@addressId INT = NULL,
	@phoneNumber VARCHAR(13) = NULL,
	@customerId BIGINT
AS
	IF(@city) IS NULL
		UPDATE Orders SET [Status]=@status WHERE Id=@orderId
	ELSE
	BEGIN
		SELECT @addressId=Id FROM Addresses WHERE City=@city and Street=@street and Number=@number
		IF(@addressId) IS NULL
		BEGIN
			INSERT INTO Addresses(City,Street,Number)
			VALUES (@city,@street,@number)
			SET @addressId=SCOPE_IDENTITY()
		END
		IF(@orderId=0)
			SELECT @orderId=Orders.Id FROM Orders WHERE [Status]='notOrdered' and CustomerId=@customerId
		UPDATE Orders SET [Status]=@status, AddressId=@addressId, PhoneNumber=@phoneNumber WHERE Id=@orderId
	END
Go
