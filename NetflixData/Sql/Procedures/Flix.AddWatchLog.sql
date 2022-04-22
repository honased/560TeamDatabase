
CREATE OR ALTER PROCEDURE Flix.AddWatchLog
	@UserID INT,
	@ShowID INT
AS

INSERT Flix.ShowWatchLog(UserID, ShowID, IsDeleted)
VALUES(@UserID, @ShowID, 0);

GO