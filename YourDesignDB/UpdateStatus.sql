CREATE PROCEDURE [dbo].[UpdateStatus]
	@id BIGINT,
	@status VARCHAR(15) = NULL
AS
	SET NOCOUNT ON;
	UPDATE Images SET [Status]=@status WHERE Id=@id
RETURN @id
