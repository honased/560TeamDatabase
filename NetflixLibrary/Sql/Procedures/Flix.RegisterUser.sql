
CREATE OR ALTER PROCEDURE Flix.RegisterUser
	@Username NVARCHAR(64)
AS
BEGIN 
WITH UserLibrary(AlreadyExists) AS
	(
		SELECT CASE WHEN EXISTS (
			SELECT *
			FROM Flix.[User] U
			WHERE U.Username = @Username
		)
		THEN CAST(1 AS BIT)
		ELSE CAST(0 AS BIT) END
	)


END
GO
