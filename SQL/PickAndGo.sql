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
	[image] IMAGE);

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
	pickupTime TIME,
	orderStatus CHAR(1),
	FOREIGN KEY(customerID) REFERENCES Customer(customerID));

CREATE TABLE OrderLine (
	orderID INT NOT NULL,
	lineID INT IDENTITY NOT NULL,
	productID INT NOT NULL,
	quantity INT,
	CONSTRAINT PK_OrderLine PRIMARY KEY (orderID,lineID),
	FOREIGN KEY(orderID) REFERENCES OrderHeader(orderID),
	FOREIGN KEY(productID) REFERENCES Product(productID));

CREATE TABLE LineIngredient (
	orderID INT NOT NULL,
	lineID INT NOT NULL,
	ingredientID INT NOT NULL,
	quantity INT,
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
	dietaryImage IMAGE);

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

INSERT INTO Category VALUES('Breads');
INSERT INTO Category VALUES('Vegetables');
INSERT INTO Category VALUES('Protein');
INSERT INTO Category VALUES('Seasonings');
INSERT INTO Category VALUES('Sauces');

INSERT INTO Product VALUES(1,'Large sandwich', 3.00, '');
INSERT INTO Product VALUES(2,'Medium sandwich', 2.00, '');
INSERT INTO Product VALUES(3,'Small sandwich', 1.00, '');

INSERT INTO DietaryType VALUES('GF', 'Gluten Free', '');
INSERT INTO DietaryType VALUES('DF', 'Dairy Free', '');
INSERT INTO DietaryType VALUES('NT', 'May contain traces of peanuts', '');
INSERT INTO DietaryType VALUES('VN', 'Vegan', '');
INSERT INTO DietaryType VALUES('VG', 'Vegetarian', '');

INSERT INTO Customer (lastName, firstName, emailAddress, phoneNumber, adminUser, dateSignedUp, dateLastOrdered)
                      VALUES('Admin', 'Admin', 'admin@admin.com', '', 'Y', '', '');
