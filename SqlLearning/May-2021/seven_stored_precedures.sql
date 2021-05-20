USE SqlLearning;

GO
IF EXISTS(SELECT TOP 1 * FROM SYS.PROCEDURES WHERE name = 'getUserDetails')
DROP PROC getUserDetails
GO
CREATE PROCEDURE getUserDetails(@UserIdFrom BIGINT, @UserIdTo BIGINT) AS
BEGIN
	DECLARE @UserIdTemp BIGINT = @UserIdFrom
	WHILE (@UserIdTemp <= @UserIdTo)
	BEGIN
		SELECT UserId, FirstName, LastName, Email
		FROM UserDetails
		WHERE UserId = @UserIdTemp
		SET @UserIdTemp += 1
	END
END

GO
getUserDetails 1, 5



---------------------------------------------------------------------------------------------




GO
IF EXISTS(SELECT TOP 1 * FROM SYS.PROCEDURES WHERE name = 'GetCourseDetails')
DROP PROC GetCourseDetails
GO
CREATE PROCEDURE GetCourseDetails(@CourseOwnerId INT, @CourseCount INT OUTPUT) AS
BEGIN
	SELECT * FROM CourseDetails
	WHERE CourseOwnerId = @CourseOwnerId
	SET @CourseCount = @@ROWCOUNT
END

GO
DECLARE @CourseCount INT = 0
EXECUTE GetCourseDetails 6, @CourseCount OUTPUT

SELECT @CourseCount





---------------------------------------------------------------------------------------------


GO
IF EXISTS(SELECT TOP 1 * FROM SYS.PROCEDURES WHERE name = 'errorHandling')
DROP PROC errorHandling
GO
CREATE PROCEDURE errorHandling(@Number INT) AS
BEGIN
	SET NOCOUNT ON
	BEGIN TRY
		SELECT @Number/0
	END TRY
	BEGIN CATCH
		--SELECT ERROR_MESSAGE() , ERROR_LINE(), ERROR_NUMBER(), ERROR_PROCEDURE(), ERROR_SEVERITY(), ERROR_STATE()
		--THROW 54321, 'Custom Error', 1
		--RAISERROR('my error raised', 16, 1)
	END CATCH
END


EXECUTE errorHandling 10
