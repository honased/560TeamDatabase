
CREATE OR ALTER PROCEDURE Flix.AddWatchLog
	@UserID INT,
	@ShowID INT
AS

INSERT Flix.ShowWatchCount(UserID, ShowID, IsDeleted)
VALUES(@UserID, @ShowID, 0)


GO