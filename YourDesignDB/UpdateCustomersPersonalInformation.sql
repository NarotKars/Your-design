CREATE PROCEDURE [dbo].[UpdateCustomersPersonalInformation]
	@id BIGINT,
	@email VARCHAR(50),
	@phoneNum VARCHAR(13),
	@city VARCHAR(20)=NULL,
	@street VARCHAR(30)=NULL,
	@number VARCHAR(10)=NULL,
	@addressId INT = NULL
AS
	UPDATE Users SET PhoneNumber=@phoneNum, Email=@email WHERE Id=@id
	
	--SELECT @addressId=Id FROM Addresses WHERE City=@city AND Street=@street AND Number=@number
	--IF(@addressId) IS NULL
	--BEGIN
		--INSERT INTO Addresses(City, Street, Number)
		--VALUES(@city,@street,@number)
		--SET @addressId=SCOPE_IDENTITY()
	--END

GO
