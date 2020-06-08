CREATE PROCEDURE [dbo].[GetUserById]
	@userId BIGINT
AS
	SELECT Users.Id, [Name], Email, NormalizedUserName, ISNULL(PhoneNumber,'unknown'), ISNULL(City,'unknown'), ISNULL(Number,'unkown'), IsValid
	FROM Users JOIN Addresses ON Addresses.Id=Users.AddressId
	WHERE Users.Id=@userId
GO
