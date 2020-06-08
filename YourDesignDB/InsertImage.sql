CREATE PROCEDURE [dbo].[InsertImage]
	@photo VARBINARY(max),
	@status VARCHAR(15),
	@text VARCHAR(100) = NULL,
	@id BIGINT = NULL OUTPUT,
	@currentStatus VARCHAR(15) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT @id=Id, @currentStatus=[Status] FROM Images WHERE Photo=@photo
	IF(@id) is null
	BEGIN
		INSERT INTO Images(Photo,[Text],[Status])
		VALUES (@photo,@text,@status)
		SET @id=SCOPE_IDENTITY()
	END
	ELSE IF(@currentStatus='deleted')
	BEGIN
		UPDATE Images SET [Status]=@status WHERE Photo=@photo
	END
	ELSE 
		SET @id=0
	RETURN @id
END
