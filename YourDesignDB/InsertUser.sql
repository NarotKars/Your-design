CREATE PROCEDURE [dbo].[InsertUser]
	@email VARCHAR(50),
	@passwordHash VARCHAR(100),
	@name VARCHAR(50),
	@normalizedUserName VARCHAR(50),
	@rank VARCHAR(10),
	@id BIGINT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Users(Email,[PasswordHash],[NormalizedUserName],[Rank],[Name],IsValid)
	VALUES(@email,@passwordHash,@normalizedUserName,@rank,@name,'notDeleted')
	SET @id=SCOPE_IDENTITY()

	--IF(@city) IS NOT NULL
	--BEGIN
		--SELECT @addressId=Id FROM Addresses WHERE City=@city AND Street=@street AND Number=@number
		--IF (@addressId) IS NULL
		--BEGIN
			--INSERT INTO Addresses(City,Street,Number)
			--VALUES (@city,@street,@number)
			--SET @addressId=SCOPE_IDENTITY()
		--END
	--END
	RETURN @id
END
