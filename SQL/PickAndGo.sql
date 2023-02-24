DROP PROC IF EXISTS spUpdateOrderHeaderStatus;
GO

IF OBJECT_ID('Customer_DietaryType', 'U')    
	IS NOT NULL DROP TABLE Customer_DietaryType;

IF OBJECT_ID('Ingredient_DietaryType', 'U')    
	IS NOT NULL DROP TABLE Ingredient_DietaryType;

IF OBJECT_ID('Favorite', 'U')    
	IS NOT NULL DROP TABLE Favorite;

IF OBJECT_ID('DietaryType', 'U')    
	IS NOT NULL DROP TABLE DietaryType;

IF OBJECT_ID('LineIngredient', 'U')    
	IS NOT NULL DROP TABLE LineIngredient;

IF OBJECT_ID('OrderLine', 'U')    
	IS NOT NULL DROP TABLE OrderLine;

IF OBJECT_ID('OrderHeader', 'U')    
	IS NOT NULL DROP TABLE OrderHeader;

IF OBJECT_ID('Ingredient', 'U')    
	IS NOT NULL DROP TABLE Ingredient;

IF OBJECT_ID('Customer', 'U')    
	IS NOT NULL DROP TABLE Customer;

IF OBJECT_ID('Product', 'U')    
	IS NOT NULL DROP TABLE Product;

IF OBJECT_ID('Category', 'U')    
	IS NOT NULL DROP TABLE Category;

CREATE TABLE Customer (
	customerID INT PRIMARY KEY IDENTITY NOT NULL,
	lastName VARCHAR(25),
	firstName VARCHAR(25),
	emailAddress VARCHAR(40) NOT NULL,
	phoneNumber VARCHAR(15),
	adminUser CHAR(1) NOT NULL,
	dateSignedUp DATE,
	dateLastOrdered DATE);

CREATE TABLE Product (
	productID INT PRIMARY KEY NOT NULL,
	[description] VARCHAR(50) NOT NULL,
	basePrice DECIMAL(9,2),
	[image] VARCHAR(255));

CREATE TABLE Category (
	categoryID VARCHAR(12) PRIMARY KEY NOT NULL);

CREATE TABLE Ingredient (
	ingredientID INT PRIMARY KEY IDENTITY NOT NULL,
	[description] VARCHAR(50) NOT NULL,
	price DECIMAL(9,2),
	categoryID VARCHAR(12) NOT NULL,
	inStock CHAR(1),
	FOREIGN KEY(categoryID) REFERENCES Category(categoryID));

CREATE TABLE OrderHeader (
	orderID INT PRIMARY KEY IDENTITY NOT NULL,
	customerID INT NOT NULL,
	orderDate DATE,
	orderValue DECIMAL(9,2),
	pickupTime DATETIME,
	orderStatus CHAR(1) NOT NULL,
	currency VARCHAR(3),
	paymentType VARCHAR(15),
	paymentID VARCHAR(40),
	paymentDate DATE,
	FOREIGN KEY(customerID) REFERENCES Customer(customerID));

CREATE TABLE OrderLine (
	orderID INT NOT NULL,
	lineID INT IDENTITY NOT NULL,
	productID INT NOT NULL,
	quantity INT,
	lineStatus CHAR(1) NOT NULL,
	price DECIMAL(9,2),
	CONSTRAINT PK_OrderLine PRIMARY KEY (orderID,lineID),
	FOREIGN KEY(orderID) REFERENCES OrderHeader(orderID),
	FOREIGN KEY(productID) REFERENCES Product(productID));

CREATE TABLE LineIngredient (
	orderID INT NOT NULL,
	lineID INT NOT NULL,
	ingredientID INT NOT NULL,
	quantity INT,
	price DECIMAL(9,2),
	CONSTRAINT PK_LineIngredient PRIMARY KEY (orderID,lineID,ingredientID),
	FOREIGN KEY(ingredientID) REFERENCES Ingredient(ingredientID),
	FOREIGN KEY(orderID) REFERENCES OrderHeader(orderID),
	FOREIGN KEY(orderID,lineID) REFERENCES OrderLine(orderID,lineID));

CREATE TABLE Favorite (
	customerID INT NOT NULL,
	orderID INT NOT NULL,
	lineID INT NOT NULL,
	favoriteName VARCHAR(25) NOT NULL,
	CONSTRAINT PK_Favorite PRIMARY KEY (customerID,orderID,lineID),
	FOREIGN KEY(customerID) REFERENCES Customer(customerID),
	FOREIGN KEY(orderID) REFERENCES OrderHeader(orderID),
	FOREIGN KEY(orderID,lineID) REFERENCES OrderLine(orderID,lineID));

CREATE TABLE DietaryType (
	dietaryID VARCHAR(2) PRIMARY KEY NOT NULL,
	dietaryName VARCHAR(30) NOT NULL,
	dietaryImage VARCHAR(255));

CREATE TABLE Customer_DietaryType (
	customerID INT NOT NULL,
	dietaryID VARCHAR(2) NOT NULL,
	CONSTRAINT PK_Customer_DietaryType PRIMARY KEY (customerID,dietaryID),
	FOREIGN KEY(customerID) REFERENCES Customer(customerID),
	FOREIGN KEY(dietaryID) REFERENCES DietaryType(dietaryID));

CREATE TABLE Ingredient_DietaryType (
	ingredientID INT NOT NULL,
	dietaryID VARCHAR(2) NOT NULL,
	CONSTRAINT PK_Ingredient_DietaryType PRIMARY KEY (ingredientID,dietaryID),
	FOREIGN KEY(ingredientID) REFERENCES Ingredient(ingredientID),
	FOREIGN KEY(dietaryID) REFERENCES DietaryType(dietaryID));

GO

CREATE PROCEDURE spUpdateOrderHeaderStatus (@OrderId INT) AS
BEGIN
	DECLARE @openLines INT;
	SET @openLines = (SELECT COUNT(ol.lineId)
                       FROM OrderLine ol
                       WHERE ol.orderID = @OrderId AND ol.lineStatus = 'O');

	IF @openLines = 0
    BEGIN
		UPDATE OrderHeader
               SET orderStatus = 'C'
			   WHERE orderId = @OrderId;
	END
END

GO

