﻿-- Build DB Structure
DROP TABLE IF EXISTS [dbo].[Address];
CREATE TABLE [dbo].[Address]
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Street VARCHAR(255) NOT NULL,
    StreetNo VARCHAR(255) NOT NULL,
    City VARCHAR(255) NOT NULL,
    PostCode VARCHAR(255) NOT NULL
)

DROP TABLE IF EXISTS [dbo].[User];
CREATE TABLE [dbo].[User]
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Login VARCHAR(255) NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Type TINYINT NOT NULL
)

DROP TABLE IF EXISTS [dbo].[Customer];
CREATE TABLE [dbo].[Customer]
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name VARCHAR(255) NOT NULL,
    Surname VARCHAR(255) NOT NULL,
    VAT_number VARCHAR(255) NOT NULL,
    UserId UNIQUEIDENTIFIER NOT NULL
)

DROP TABLE IF EXISTS [dbo].[Payment];
CREATE TABLE [dbo].[Payment]
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    DueDate DATETIME2 NOT NULL,
    Method TINYINT NOT NULL,
    Status TINYINT NOT NULL
)

DROP TABLE IF EXISTS [dbo].[Product];
CREATE TABLE [dbo].[Product]
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Name VARCHAR(255) NOT NULL,
    Price INT NOT NULL,
    Category VARCHAR(255) NOT NULL
)

DROP TABLE IF EXISTS [dbo].[Order];
CREATE TABLE [dbo].[Order]
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    DueDate DATETIME2 NOT NULL,
    DeliveryDate DATETIME2 NOT NULL,
    PaymentId UNIQUEIDENTIFIER NOT NULL,
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    AddressId UNIQUEIDENTIFIER NOT NULL
)

DROP TABLE IF EXISTS [dbo].[Order_Product];
CREATE TABLE [dbo].[Order_Product]
(
    OrderId UNIQUEIDENTIFIER NOT NULL,
    ProductId UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT PK_OrderProduct PRIMARY KEY (OrderId, ProductId)
)

-- Seed DB

INSERT INTO [dbo].[Address] 
(Street, StreetNo, City, PostCode)
VALUES 
('Studzienna', '12', 'Bialystok', '15-771'),
('Pulaskiego', '33', 'Bialystok', '15-331'),
('Wiejska', '25B', 'Warszawa', '01-001');

INSERT INTO [dbo].[User]
( Login, Password, Email, Type)
VALUES
('admin', 'admin', 'admin@example.com', 2),
('seller', 'seller', 'seller@example.com', 1),
('customer', 'customer', 'customer@example.com', 0);

DECLARE @userId uniqueidentifier;
SELECT TOP(1) @userId = u.Id
FROM [dbo].[User] u
WHERE u.Email = 'customer@example.com';

INSERT INTO [dbo].[Customer]
( Name, Surname, VAT_number, UserId )
VALUES
('Cust', 'Omer', 'RealVatNumber000', @userId);

INSERT INTO [dbo].[Payment]
( DueDate, Method, Status )
VALUES
( GetDate(), 0, 1 );

INSERT INTO [dbo].[Product]
( Name, Price, Category )
VALUES
( 'Bread', 200, 'Food');

DECLARE @paymentId uniqueidentifier;
SELECT TOP(1) @paymentId = p.Id
FROM [dbo].[Payment] p;

DECLARE @customerId uniqueidentifier;
SELECT TOP(1) @customerId = c.Id
FROM [dbo].[Customer] c;

DECLARE @addressId uniqueidentifier;
SELECT TOP(1) @addressId = a.Id
FROM [dbo].[Address] a
WHERE a.Street = 'Studzienna';

INSERT INTO [dbo].[Order]
( DueDate, DeliveryDate, PaymentId, CustomerId, AddressId )
VALUES
( GetDate(), GetDate(), @paymentId, @customerId, @addressId);

DECLARE @orderId uniqueidentifier;
SELECT TOP(1) @orderId = o.Id
FROM [dbo].[Order] o;

INSERT INTO [dbo].[Order_Product]
SELECT @orderId AS 'OrderId', p.Id AS 'ProductId'
FROM [dbo].[Product] p



