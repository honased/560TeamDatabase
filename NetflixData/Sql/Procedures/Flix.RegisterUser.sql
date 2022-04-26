-- This procedure will insert a user and return the userid if the username
-- does not exist. Otherwise, no row is returned.
CREATE OR ALTER PROCEDURE Flix.RegisterUser
    @Username NVARCHAR(64)
AS
BEGIN
DECLARE @WillRegisterUser BIT =
    (
        SELECT IIF(NOT EXISTS
            (
                SELECT * FROM Flix.[User] U WHERE U.Username = @Username
            ), 1, 0)
    );

IF(@WillRegisterUser = 1)
BEGIN
INSERT Flix.[User](Username)
VALUES(@Username);

SELECT CAST(SCOPE_IDENTITY() AS int) AS UserID;
END

END
