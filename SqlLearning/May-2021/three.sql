
CREATE VIEW CoursesByOwner AS
SELECT CD.CourseName, UD.Email FROM CourseDetails AS CD
INNER JOIN UserDetails AS UD
ON CD.CourseOwnerId = UD.UserId

GO
SELECT * FROM CoursesByOwner

DROP VIEW CoursesByOwner

---- WITH ENCRYPTION 
CREATE VIEW CoursesByOwner WITH ENCRYPTION AS
SELECT CD.CourseName, UD.Email FROM CourseDetails AS CD
INNER JOIN UserDetails AS UD
ON CD.CourseOwnerId = UD.UserId


---- WITH CHECK OPTION
CREATE VIEW CoursesByOwner WITH ENCRYPTION AS
SELECT CD.CourseName, UD.Email FROM CourseDetails AS CD
INNER JOIN UserDetails AS UD
ON CD.CourseOwnerId = UD.UserId
WITH CHECK OPTION


-- Adding/Deleting rows into the Views
INSERT INTO CoursesByOwner(CourseName, Email)
VALUES('Azure', 'johnskull@outlook.com')
-- As the abouve insert statement alters multiple tables if worked, this will throw error
-- If Inseting into View inserts row in single table, this works
-- Same goes with the Deleting the rows fromt the View

-- Updating rows of the View
-- If WITH CHECK OPTION is included while creating View the Update will work only if the Update 
-- satifies the included criteria in the select_statement
-- If WITH CHECK OPTION is not included Update will work irrespective of the Update


