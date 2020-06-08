CREATE PROCEDURE [dbo].[InsertProduct]
	@id BIGINT = NULL OUTPUT,
	@categoryId INT,
	@imageId BIGINT,
	@companyId INT,
	@buyingPrice MONEY,
	@sellingPrice MONEY
AS
BEGIN
	SET NOCOUNT ON;
	IF NOT EXISTS(SELECT ImageId FROM Products WHERE ImageId=@imageId)
	BEGIN
		INSERT INTO Products(CategoryId,ImageId,CompanyId,BuyingPrice,SellingPrice)
		VALUES(@categoryId,@imageId,@companyId,@buyingPrice,@sellingPrice)
		SET @id=SCOPE_IDENTITY()
	END
	ELSE
	BEGIN
		UPDATE Products SET BuyingPrice=@buyingPrice, SellingPrice=@sellingPrice, CompanyId=@companyId WHERE ImageId=@imageId
		SET @id=(SELECT Id FROM Products WHERE ImageId=@imageId)
	END
	RETURN @id
END
