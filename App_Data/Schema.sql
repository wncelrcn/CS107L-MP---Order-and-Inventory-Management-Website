-- Drop tables if they exist
DROP TABLE IF EXISTS AuthUsers;
DROP TABLE IF EXISTS AdminCredentials;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Products;
DROP TABLE IF EXISTS ShoppingCart;
DROP TABLE IF EXISTS Orders;


-- Create Users table
CREATE TABLE Users (
    Username VARCHAR(100) NOT NULL PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    ContactNumber NVARCHAR(20) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    CONSTRAINT CHK_ContactNumber CHECK (LEN(ContactNumber) = 11), -- Check contact number length
    
);


-- Create AuthUsers table
CREATE TABLE AuthUsers (
    Username VARCHAR(100) NOT NULL PRIMARY KEY,
    Password VARCHAR(100) NOT NULL,
    CONSTRAINT FK_AuthUsers_Users FOREIGN KEY (Username) REFERENCES Users(username)
);

-- Create AdminCredentials table
CREATE TABLE AdminCredentials (
    AdminUsername VARCHAR(100) NOT NULL PRIMARY KEY,
    Password VARCHAR(100) NOT NULL,
);

-- Insert admin credentials
INSERT INTO AdminCredentials (AdminUsername, Password) VALUES ('admin', 'password');


-- Create Products table
CREATE TABLE Products (
    ProductID INT NOT NULL PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Stock INT NOT NULL,
    Category VARCHAR(50) NOT NULL,
    CONSTRAINT CHK_Price CHECK (Price >= 0), -- Check price is non-negative
    CONSTRAINT CHK_Stock CHECK (Stock >= 0) -- Check stock is non-negative
);

INSERT INTO Products (ProductID, ProductName, Price, Stock, Category) VALUES
(1, 'COFFEE EXPRESSO CONCENTRATE', 750.00, 21, 'Coffee'),
(2, 'COFFEE SWEETENER', 125.00, 47, 'Coffee'),
(3, 'COFFEE CREAMER', 250.00, 35, 'Coffee'),
(4, 'COFFEE CREAMER SACK', 4000.00, 20, 'Coffee'),
(5, 'CARAMEL SYRUP', 420.00, 50, 'Coffee'),
(6, 'FRENCH VANILLA SYRUP', 420.00, 45, 'Coffee'),
(7, 'SALTED CARAMEL SYRUP', 420.00, 50, 'Coffee'),
(8, 'HAZELNUT SYRUP', 420.00, 50, 'Coffee'),
(9, 'BROWN SUGAR SYRUP', 420.00, 55, 'Coffee'),
(10, 'CHOCOLATE SYRUP', 420.00, 57, 'Coffee'),
(11, 'ALMOND SYRUP', 420.00, 25, 'Coffee'),
(12, 'TIRAMISU SYRUP', 420.00, 29, 'Coffee'),
(13, 'PEPPERMINT SYRUP', 550.00, 43, 'Coffee'),
(14, 'BLUEBERRY SYRUP', 700.00, 26, 'FruitTea'),
(15, 'GREEN APPLE SYRUP', 700.00, 28, 'FruitTea'),
(16, 'PEACH SYRUP', 700.00, 12, 'FruitTea'),
(17, 'PASSION FRUIT SYRUP', 700.00, 34, 'FruitTea'),
(18, 'KIWI SYRUP', 700.00, 23, 'FruitTea'),
(19, 'FRUITY WINTERMELON POWDER', 400.00, 60, 'Milktea'),
(20, 'JAPANESE HOKKAIDO POWDER', 400.00, 60, 'Milktea'),
(21, 'OKINAWA TIGER SUGAR POWDER', 400.00, 53, 'Milktea'),
(22, 'COOKIE DOUGH', 400.00, 32, 'Milktea'),
(23, 'SCARLET ROSE', 400.00, 13, 'Milktea'),
(24, 'DELISH CHEESECAKE', 400.00, 5, 'Milktea'),
(25, 'CREAMY AVOCADO', 400.00, 39, 'Milktea'),
(26, 'CANDY POP', 400.00, 12, 'Milktea'),
(27, 'CHERRY BLOSSOM', 400.00, 9, 'Milktea'),
(28, 'GALAXY DARK CHOCO', 400.00, 44, 'Milktea'),
(29, 'MATCHA POWDER', 450.00, 49, 'Milktea'),
(30, 'BLACK PEARL', 200.00, 32, 'Misc'),
(31, 'COFFEE JELLY', 800.00, 16, 'Misc'),
(32, 'NATA DE COCO', 800.00, 34, 'Misc'),
(33, 'TIGER SUGAR', 450.00, 21, 'Misc'),
(34, 'FRUIT JELLY', 800.00, 28, 'Misc'),
(35, 'WHIPPED CREAM', 800.00, 29, 'Misc'),
(36, 'CHOCOLATE SYRUP & TOPPINGS', 200.00, 15, 'Misc'),
(37, 'CARAMEL SYRUP & TOPPINGS', 200.00, 12, 'Misc'),
(38, '16 0Z. CUP W/ LOGO 100PCS', 450.00, 42, 'Misc'),
(39, '22OZ. CUP W/ LOGO 100PCS', 550.00, 21, 'Misc'),
(40, '8OZ CUPS WITH LOGO AND LIDS 100PCS (HOT DRINKS)', 650.00, 12, 'Misc'),
(41, '12OZ CUPS WITH LOGO AND LIDS 100PCS (HOT DRINKS)', 700.00, 16, 'Misc'),
(42, 'FOLDABLE CUP HOLDER', 950.00, 31, 'Misc'),
(43, 'STRAW 21CM 100PCS (1PACK)', 100.00, 43, 'Misc'),
(44, 'COFFEE THIN STRAW', 100.00, 21, 'Misc'),
(45, 'COFFEE FLAT LIDS 90MM', 150.00, 8, 'Misc'),
(46, 'DOME LIDS 100PCS', 150.00, 6, 'Misc'),
(47, 'TAKE OUT PLASTIC SINGLE 100PCS', 150.00, 64, 'Misc'),
(48, 'TAKE OUT PLASTIC DOUBLE 100PCS', 200.00, 53, 'Misc');


-- Create ShoppingCart table
CREATE TABLE ShoppingCart (
    ShoppingCartID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProductID INT NOT NULL,
    ProductName NVARCHAR(255) NOT NULL,
    UnitPrice DECIMAL(18, 2) NOT NULL,
    Username VARCHAR(100) NOT NULL,
    Quantity INT NOT NULL,
    TotalPrice DECIMAL(18, 2) NOT NULL,
    CONSTRAINT CHK_Quantity CHECK (Quantity > 0), -- Check quantity is positive
    CONSTRAINT CHK_TotalPrice CHECK (TotalPrice >= 0), -- Check total price is non-negative
    FOREIGN KEY (Username) REFERENCES Users(Username),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);


-- Create Orders table
CREATE TABLE Orders (
    OrderNum INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    TransactionID VARCHAR(100) NOT NULL,
    Username VARCHAR(100) NOT NULL,
    ProductName NVARCHAR(255) NOT NULL,
    Quantity INT NOT NULL,
    OrderDate DATE DEFAULT GETDATE() NOT NULL,
    TotalOrderPrice DECIMAL(18, 2) NOT NULL,
    OrderStatus VARCHAR(50) NOT NULL,
    CONSTRAINT CHK_Quantity_Order CHECK (Quantity > 0), -- Check quantity is positive
    CONSTRAINT CHK_TotalOrderPrice CHECK (TotalOrderPrice >= 0), -- Check total order price is non-negative
    CONSTRAINT FK_Orders_Users FOREIGN KEY (Username) REFERENCES Users (Username)
);
