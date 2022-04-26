-- This procedure will "delete" the given watchlog by
-- setting its "IsDeleted" property to 1
CREATE OR ALTER PROCEDURE Flix.RemoveWatchLog
	@WatchLogID INT
AS

UPDATE Flix.ShowWatchLog
SET
	IsDeleted = 1
WHERE
	WatchLogID = @WatchLogID;
GO