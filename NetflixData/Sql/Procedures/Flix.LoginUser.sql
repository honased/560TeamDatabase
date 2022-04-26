-- This procedure returns the userid associated with the username if it exists.
-- Otherwise, no row is returned.
CREATE OR ALTER PROCEDURE Flix.LoginUser
	@Username NVARCHAR(64)
AS

SELECT U.UserID
FROM Flix.[User] U
WHERE U.Username = @Username