CREATE PROCEDURE [dbo].[GetUserByName]
	@normalizedUserName VARCHAR(50),
	@addressId INT = NULL
AS
	SELECT @addressId=Users.AddressId FROM Users WHERE NormalizedUserName=@normalizedUserName
	IF(@addressId) IS NULL
	BEGIN
		SELECT Users.Id, [Name], Email, [Rank], NormalizedUserName, PasswordHash, ISNULL(PhoneNumber,'unknown') as PhoneNumber, 'unknown' as City, 'unknown' as Street, 'unknown' as Number, IsValid
		FROM Users
		WHERE Users.NormalizedUserName=@normalizedUserName
	END
	ELSE
	BEGIN
		SELECT Users.Id, [Name], Email, [Rank], NormalizedUserName, PasswordHash, ISNULL(PhoneNumber,'unknown') as PhoneNumber, City, Street, Number, IsValid
		FROM Users JOIN Addresses ON Addresses.Id=@addressId
		WHERE Users.NormalizedUserName=@normalizedUserName
	END
GO
