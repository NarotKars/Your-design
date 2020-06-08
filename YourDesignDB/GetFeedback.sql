CREATE PROCEDURE [dbo].[GetFeedback]
AS
	SET NOCOUNT ON;
	SELECT ISNULL([Name], 'unknown') as [Name] , [Text]
	FROM Feedback LEFT JOIN Users ON Feedback.CustomerId=Users.Id
GO
