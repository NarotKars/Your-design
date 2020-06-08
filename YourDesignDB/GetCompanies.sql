CREATE PROCEDURE [dbo].[GetCompanies]
AS
	SET NOCOUNT ON;
	SELECT Users.Id, [Name]
	FROM Users 
	WHERE [Rank]='company' and IsValid='notDeleted'
GO
