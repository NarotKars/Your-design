CREATE PROCEDURE [dbo].[GetProductDetails]
	@imageId bigint 
AS
	SET NOCOUNT ON;
	SELECT Users.[Name], BuyingPrice, SellingPrice
	From Products join Users on Products.CompanyId=Users.Id
	where Products.ImageId=@imageId
GO
