/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

--EXECUTE InsertCompany @name='Parker'
--EXECUTE InsertCompany @name='Custom Ink'
--EXECUTE InsertCompany @name='Moleskine'

--EXECUTE InsertCategory @name='pens'
--EXECUTE InsertCategory @name='t-shirts'

--EXECUTE InsertAddress @city='Yerevan', @street='Argishti' , @number='11/3'
--EXECUTE InsertAddress @city='Yerevan', @street='Isahakyan' , @number='3'

--EXECUTE InsertUser @email='narotKars@yourdesign.com', @password='123456', @phoneNum='+37499999999', @rank='customer'
--EXECUTE InsertUser @email='greta@yourdesign.com', @password='123456', @phoneNum='+37488888888', @rank='customer'

--EXECUTE InsertCustomer @name='Narot Kars Karapetian', @addressId=1, @id=1
--EXECUTE InsertCustomer @name='Greta Arakelyan', @addressId=2, @id=2

--EXECUTE InsertOrder @orderDate='2020-03-22 20:10:05', @status='delivered', @amount=2000, @addressId=1, @customerId=1
--EXECUTE InsertOrder @orderDate='2020-03-22 18:10:05', @status='delivered', @amount=3000, @addressId=1, @customerId=1
--EXECUTE InsertOrder @orderDate='2020-03-23 18:10:05', @status='rejected', @amount=4000, @addressId=1, @customerId=1
--EXECUTE InsertOrder @orderDate='2020-03-24 14:10:05', @status='delivered', @amount=400, @addressId=1, @customerId=1
--EXECUTE InsertOrder @orderDate='2020-03-24 15:10:05', @status='delivered', @amount=400, @addressId=1, @customerId=1
--EXECUTE InsertOrder @orderDate='2020-03-24 12:09:10', @status='delivered', @amount=5000, @addressId=2, @customerId=2
--EXECUTE InsertOrder @orderDate='2020-03-25 11:11:11', @status='delivered', @amount=3000, @addressId=2, @customerId=2
--EXECUTE InsertOrder @orderDate='2020-03-25 14:25:23', @status='new', @amount=4000, @addressId=1, @customerId=1
--EXECUTE InsertOrder @orderDate='2020-03-27 11:25:23', @status='new', @amount=3500, @addressId=1, @customerId=1
