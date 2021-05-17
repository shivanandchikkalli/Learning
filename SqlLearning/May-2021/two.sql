SELECT * FROM UserDetails

ALTER TABLE UserDetails
ADD CONSTRAINT PK_UserDetails_UserId PRIMARY KEY (UserId) 

SET IDENTITY_INSERT UserDetails ON
INSERT INTO UserDetails (UserId, FirstName, MiddleName, LastName, UserName , Email)
VALUES
(6, 'Mary', '', 'Cooper', '', '')
SET IDENTITY_INSERT UserDetails OFF

ALTER TABLE UserDetails
DROP CONSTRAINT PK_UserDetails_UserId


-- FOREIGN KEY

CREATE TABLE CourseDetails
(
CourseId INT PRIMARY KEY IDENTITY(1,1),
CourseName VARCHAR(50) NOT NULL,
CourseOwnerId BIGINT NOT NULL FOREIGN KEY REFERENCES UserDetails(UserId),
CreateDate DATETIME DEFAULT GETDATE(),
CreateUserId VARCHAR(15) DEFAULT 'Admin',
UpdateDate DATETIME DEFAULT GETDATE(),
UpdateUserId VARCHAR(15) DEFAULT 'Admin',
);

SELECT * FROM CourseDetails

INSERT INTO CourseDetails(CourseName, CourseOwnerId)
VALUES ('C', 1),
('C++', 3),
('C#', 6),
('SQL', 4)


SELECT CD.CourseName, UD.Email FROM CourseDetails AS CD
INNER JOIN UserDetails AS UD
ON CD.CourseOwnerId = UD.UserId

-- INNER JOIN (JOIN), LEFT JOIN, RIGHT JOIN

-- ADD CONSTRAINT => DEFAULT, FOREIGN KEY, PRIMARY KEY, UNIQUE, CHECK
