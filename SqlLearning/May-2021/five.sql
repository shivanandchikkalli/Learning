--	UNION, UNION ALL, EXCEPT, INTERSECT


--	The below query retrieves 5 different names:

--	SELECT DISTINCT EmployeeFirstName from [dbo].[tblEmployee] where [EmployeeFirstName] like 'Y%' 


--	The below query retrieves 3 different names:

--	SELECT DISTINCT EmployeeFirstName from [dbo].[tblEmployee] where [EmployeeFirstName] like 'YA%' 



-- 1. UNION
--	SELECT DISTINCT EmployeeFirstName from [dbo].[tblEmployee] where [EmployeeFirstName] like 'Y%'
--	UNION
--	SELECT DISTINCT EmployeeFirstName from [dbo].[tblEmployee] where [EmployeeFirstName] like 'YA%'

-- This query gives 5 records , 
-- First query gives 5 records which out of which 3 records are same as the results of Query 2, so Query 2 records are
-- duplicate , UNION removes the duplicate records, hence => 5 records





-- 2. UNION ALL
--	SELECT DISTINCT EmployeeFirstName from [dbo].[tblEmployee] where [EmployeeFirstName] like 'Y%'
--	UNION ALL
--	SELECT DISTINCT EmployeeFirstName from [dbo].[tblEmployee] where [EmployeeFirstName] like 'YA%'

-- This query gives 8 records , 
-- First query gives 5 records and Query 2 gives 3 records UNION ALL combines all records and doesn't takes duplication
-- into consideration, hence => 8 records




-- 3. EXCEPT
--	SELECT DISTINCT EmployeeFirstName from [dbo].[tblEmployee] where [EmployeeFirstName] like 'Y%'
--	EXCEPT
--	SELECT DISTINCT EmployeeFirstName from [dbo].[tblEmployee] where [EmployeeFirstName] like 'YA%'

-- 5 records - 3 records which are present in results of first query
-- hence => 2 records



-- 4. INTERSECT
--	SELECT DISTINCT EmployeeFirstName from [dbo].[tblEmployee] where [EmployeeFirstName] like 'Y%'
--	INTERSECT
--	SELECT DISTINCT EmployeeFirstName from [dbo].[tblEmployee] where [EmployeeFirstName] like 'YA%'

-- Returns only records which are present in both the results
-- hence => 3 records