﻿CREATE TABLE [dbo].[Categories]
(
	[Id] INT IDENTITY(1,1) CONSTRAINT PK_Categories PRIMARY KEY,
	[Name] VARCHAR(50) CONSTRAINT NN_Name NOT NULL
)
