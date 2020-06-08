CREATE PROCEDURE [dbo].[InsertFeedback]
	@text VARCHAR(MAX),
	@customerId BIGINT = NULL,
	@id BIGINT = NULL OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Feedback([Text], CustomerId)
	VALUES (@text, @customerId)
	SET @id=SCOPE_IDENTITY()
END

