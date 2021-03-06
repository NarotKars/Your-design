﻿CREATE TABLE [dbo].[ProductsInOrders]
(
	[Id] BIGINT IDENTITY(1,1) CONSTRAINT PK_OrdersInProducts PRIMARY KEY CONSTRAINT NN_Id NOT NULL,
	ProductId BIGINT CONSTRAINT FK_ProductsInOrdersProducts FOREIGN KEY REFERENCES Products([Id]) CONSTRAINT NN_ProductId NOT NULL,
	OrderId BIGINT CONSTRAINT FK_ProductsInOrdersOrders FOREIGN KEY REFERENCES Orders([Id]) CONSTRAINT NN_OrderId NOT NULL,
	[Status] VARCHAR(30)
)
