CREATE PROCEDURE [dbo].[InsertCustomerDesign]
	@photo VARBINARY(max),
	@buyingPrice DECIMAL,
	@sellingPrice DECIMAL,
	@categoryId INT,
	@companyId BIGINT,
	@imageId BIGINT = NULL,
	@productId BIGINT = NULL OUTPUT
AS
	INSERT INTO Images(Photo,[Status])
	VALUES(@photo,'customer')
	SET @imageId=SCOPE_IDENTITY()

	INSERT INTO Products(CategoryId, ImageId, SellingPrice, BuyingPrice, CompanyId)
	VALUES(@categoryId, @imageId, @sellingPrice, @buyingPrice, @companyId)
	SET @productId=SCOPE_IDENTITY()
GO
