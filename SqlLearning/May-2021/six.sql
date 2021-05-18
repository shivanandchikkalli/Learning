USE SqlLearning;

--	MERGE Statement

IF NOT EXISTS(SELECT TOP 1 * FROM sys.tables WHERE name = 'Employee')
BEGIN
CREATE TABLE Employee
(
	EmployeeId BIGINT PRIMARY KEY IDENTITY(1,1),
	FirstName VARCHAR(25) NOT NULL,
	MiddleName VARCHAR(25) NULL,
	LastName VARCHAR(25) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	NetWorth BIGINT NOT NULL DEFAULT 0,
	CreateDate DATETIME DEFAULT GETDATE(),
	CreatedUserId VARCHAR(25) DEFAULT 'Admin',
	UpdateDateDate DATETIME DEFAULT GETDATE(),
	UpdatedUserId VARCHAR(25) DEFAULT 'Admin',
);
END

IF NOT EXISTS(SELECT TOP 1 * FROM sys.tables WHERE name = 'EmployeeTemp')
BEGIN
CREATE TABLE EmployeeTemp
(
	EmployeeTempId BIGINT PRIMARY KEY IDENTITY(1,1),
	FirstName VARCHAR(25) NOT NULL,
	MiddleName VARCHAR(25) NULL,
	LastName VARCHAR(25) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	NetWorth BIGINT NOT NULL DEFAULT 0,
	CreateDate DATETIME DEFAULT GETDATE(),
	CreatedUserId VARCHAR(25) DEFAULT 'Admin',
	UpdateDateDate DATETIME DEFAULT GETDATE(),
	UpdatedUserId VARCHAR(25) DEFAULT 'Admin',
);
END

INSERT INTO Employee (FirstName, MiddleName, LastName, Email)
VALUES ('John', '', 'Wick', ''),
('Michael', '', 'J', ''),
('Harry', '', 'Potter', ''),
('Henry', '', 'Ford', ''),
('Neils', '', 'Bohr', '')


SELECT * FROM Employee
SELECT * FROM EmployeeTemp


INSERT INTO EmployeeTemp (FirstName, MiddleName, LastName, Email, NetWorth)
VALUES ('John', '', 'Wick', '', 100000),
('Michael', '', 'J', '', 30000),
('Harry', '', 'Potter', '', 50000),
('Henry', '', 'Ford', '', 700000),
('Neils', '', 'Bohr', '', 48000),
('Bill', '', 'Gates', '', 1000000000),
('Steve', '', 'Jobs', '', 800000000)

GO
UPDATE Employee
SET Email = CONCAT(LOWER(FirstName), ISNULL(TRIM(LOWER(MiddleName)), '') , LOWER(LastName), '@outlook.com')

GO
UPDATE EmployeeTemp
SET Email = CONCAT(LOWER(FirstName), LOWER(LastName), '@outlook.com')

GO
UPDATE Employee
SET MiddleName = 'James'
WHERE EmployeeId = 3

GO
BEGIN TRAN
SELECT * FROM Employee
MERGE INTO Employee AS E
USING EmployeeTemp AS ET
ON E.EmployeeId = ET.EmployeeTempId AND E.FirstName = ET.FirstName AND E.LastName = ET.LastName
WHEN MATCHED THEN
	UPDATE
	SET E.NetWorth = E.NetWorth + ET.NetWorth
WHEN NOT MATCHED BY TARGET THEN
	INSERT (FirstName, MiddleName, LastName, Email, NetWorth)
	VALUES (ET.FirstName, ET.MiddleName, ET.LastName, ET.Email, ET.NetWorth);

SELECT * FROM Employee
ROLLBACK TRAN


--	MERGING WILL NOT ALLOW UPDATING/DELETING MULTIPLE ROWS
-- To Fix this issue the source table needs to have only one row for each matching row in the target table.
-- We can achieve this by using the GROUP BY on the Source table
-- Example below



CREATE TABLE Transactions
(
	TransactionId BIGINT PRIMARY KEY IDENTITY(1,1),
	Amount BIGINT NOT NULL,
	CreateDate DATETIME DEFAULT GETDATE(),
	CreatedUserId VARCHAR(25) DEFAULT 'Admin',
	UpdateDateDate DATETIME DEFAULT GETDATE(),
	UpdatedUserId VARCHAR(25) DEFAULT 'Admin',
);
CREATE TABLE TransactionsTemp
(
	TransactionTempId BIGINT,
	Amount BIGINT NOT NULL,
	CreateDate DATETIME DEFAULT GETDATE(),
	CreatedUserId VARCHAR(25) DEFAULT 'Admin',
	UpdateDateDate DATETIME DEFAULT GETDATE(),
	UpdatedUserId VARCHAR(25) DEFAULT 'Admin',
);

INSERT INTO Transactions(Amount)
VALUES (100),
(200),
(500),
(50),
(7000)


INSERT INTO TransactionsTemp(TransactionTempId, Amount)
VALUES (4, 1000),
(6,200),
(2, 500),
(4, 50),
(1, 7000)

SELECT * FROM Transactions
SELECT * FROM TransactionsTemp


GO
BEGIN TRAN
SELECT * FROM Transactions
MERGE INTO Transactions AS E
USING TransactionsTemp AS ET
ON E.TransactionId = ET.TransactionTempId
WHEN MATCHED THEN
	UPDATE
	SET E.Amount = E.Amount + ET.Amount
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Amount)
	VALUES (ET.Amount);

SELECT * FROM Employee
ROLLBACK TRAN

--THIS ABOVE MERGE STATEMENT THROWS BELOW ERROR
--	The MERGE statement attempted to UPDATE or DELETE the same row more than once. 
--	This happens when a target row matches more than one source row. 
--	A MERGE statement cannot UPDATE/DELETE the same row of the target table multiple times. 
--	Refine the ON clause to ensure a target row matches at most one source row, 
--	or use the GROUP BY clause to group the source rows.

-- WE SHOULD BE USING THE BELOW QUERY IN-PLACE OF SOURCE TABLE
SELECT TransactionTempId, SUM(Amount) FROM TransactionsTemp
GROUP BY TransactionTempId

-- UPDATED MERGE STATEMENT


GO
BEGIN TRAN
SELECT * FROM Transactions
MERGE INTO Transactions AS E
USING (
	SELECT TransactionTempId, SUM(Amount) AS 'Amount' FROM TransactionsTemp
	GROUP BY TransactionTempId
) AS ET
ON E.TransactionId = ET.TransactionTempId
WHEN MATCHED THEN
	UPDATE
	SET E.Amount = E.Amount + ET.Amount
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Amount)
	VALUES (ET.Amount)
WHEN NOT MATCHED BY SOURCE THEN
	UPDATE
	SET E.Amount = E.Amount + 1
OUTPUT inserted.*, deleted.*;

SELECT * FROM Transactions
ROLLBACK TRAN