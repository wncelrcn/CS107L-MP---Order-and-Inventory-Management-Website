DROP TABLE IF EXISTS AuthUsers;
DROP TABLE IF EXISTS Users;

CREATE TABLE Users(
	username VARCHAR(100) NOT NULL PRIMARY KEY,
	FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    ContactNumber NVARCHAR(20),
    Address NVARCHAR(255),
);

CREATE TABLE AuthUsers(
	username VARCHAR(100) NOT NULL,
	password VARCHAR(100) NOT NULL,
	FOREIGN KEY (username) REFERENCES Users(username)
);
