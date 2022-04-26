-- This procedure gets all associated watch logs with a show for a user.
-- It is ordered by the watched date asc.
CREATE OR ALTER PROCEDURE Flix.GetWatchLogs
    @UserID INT,
    @ShowID INT
AS
    SELECT SWL.WatchLogID, SWL.ShowID, SWL.WatchedOn
    FROM Flix.ShowWatchLog SWL
        WHERE SWL.ShowID = @ShowID AND SWL.UserID = @UserID AND SWL.IsDeleted = 0
	ORDER BY SWL.WatchedOn ASC;
GO