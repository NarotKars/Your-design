CREATE PROCEDURE [dbo].[GetImages]
	@categoryId INT = NULL,
	@status1 VARCHAR(15) =NULL,
	@status2 VARCHAR(15) = NULL
AS
	SET NOCOUNT ON;
	IF( @categoryId ) IS NULL
	SELECT Id, Photo, [Status], ISNULL([Text], 'Your text here') as Text
	FROM Images WHERE [Status] = @status1 OR [Status] = @status2
	ELSE
	SELECT Images.Id, Photo, [Status], ISNULL([Text], 'Your text here') as Text, CategoryId 
	FROM Images JOIN Products on Products.ImageId = Images.Id
	WHERE CategoryId=@categoryId
GO
