CREATE PROCEDURE [dbo].[UpdateImage]
	@id BIGINT = NULL,
	@photo VARBINARY(MAX) = NULL,
	@status VARCHAR(15) = NULL
AS
	SET NOCOUNT ON;
	IF(SELECT Id FROM Images WHERE Photo=@photo AND [Status]='deleted') IS NOT NULL
	BEGIN
		UPDATE Images SET [Status]=@status WHERE Photo=@photo
		UPDATE Images SET [Status]='deleted' WHERE Id=@id
		Return (SELECT Id FROM Images Where Photo=@photo)
	END
	ELSE IF(SELECT Id FROM Images WHERE Photo=@photo) IS NOT NULL
		RETURN 0
	ELSE
	BEGIN
		UPDATE Images SET [Status]='deleted' WHERE Id=@id
		INSERT INTO Images(Photo,[Status])
		VALUES(@photo,@status)
		SET @id=SCOPE_IDENTITY()
		RETURN @id
	END
