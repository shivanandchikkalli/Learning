--CREATE DATABASE SqlLearning;

USE SqlLearning;

CREATE TABLE UserDetails
(
UserId BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1),
FirstName VARCHAR(25) NOT NULL,
MiddleName VARCHAR(25) NULL,
LastName VARCHAR(25) NOT NULL,
UserName VARCHAR(15) NOT NULL,
Email VARCHAR(25) NOT NULL,
CreateDate DATETIME DEFAULT GETDATE(),
CreateUserId VARCHAR(15) DEFAULT 'Admin',
UpdateDate DATETIME DEFAULT GETDATE(),
UpdateUserId VARCHAR(15) DEFAULT 'Admin',
);

--DROP TABLE UserDetails

INSERT INTO UserDetails (FirstName, MiddleName, LastName, UserName , Email)
VALUES
('Sheldon', '', 'Cooper', '', ''),
('Mike', '', 'Gibbs', '', ''),
('Gary', '', 'Con', '', ''),
('Harry', '', 'Rim', '', ''),
('John', '', 'Von', '', '')


UPDATE UserDetails
SET UserName = CONCAT(LOWER(FirstName), LOWER(LastName)),
Email = CONCAT(LOWER(FirstName), LOWER(LastName), '@outlook.com'),
MiddleName = NULL
WHERE MiddleName = ''

SELECT * FROM UserDetails
WHERE UserId BETWEEN 1 AND 6

--ALTER TABLE UserDetails
--ADD CONSTRAINT unique_UserDetails_UserId UNIQUE (UserId);






