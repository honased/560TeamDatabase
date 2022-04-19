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
END

SELECT @WillRegisterUser AS Result;
END
