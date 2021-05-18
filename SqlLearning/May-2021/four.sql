USE SqlLearning;

SELECT * FROM [dbo].[CourseDetails]

GO
DROP TRIGGER TR_DmlCourseUpdate
GO
CREATE TRIGGER TR_DmlCourseUpdate
    ON [dbo].[CourseDetails]
    AFTER DELETE, INSERT, UPDATE
    AS
    BEGIN
	SET NOCOUNT ON
	--PRINT 'Trigger Called...'
	SELECT * FROM inserted
	SELECT * FROM deleted
    END

BEGIN TRAN
INSERT INTO [dbo].[CourseDetails] (CourseName, CourseOwnerId)
VALUES ('HTML5/CSS3', 5)
ROLLBACK TRAN

