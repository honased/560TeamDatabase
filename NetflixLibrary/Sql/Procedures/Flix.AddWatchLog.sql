
CREATE OR ALTER PROCEDURE Flix.AddWatchLog
	@UserID INT,
	@ShowID INT
AS

WITH UserShowLibrary(ShowID) AS
	(
		SELECT USL.ShowID
		FROM Flix.[User] U
			INNER JOIN Flix.[ShowWatchCount] SWC ON SWC.UserID = U.UserID
		WHERE U.UserID = @UserID AND SWC.IsDeleted = 0
	)
INSERT Flix.ShowWatchCount(UserID, ShowID, DateWatched, IsDeleted)
VALUES(@UserID, @ShowID, SYSDATETIMEOFFSET(), 0)


GO