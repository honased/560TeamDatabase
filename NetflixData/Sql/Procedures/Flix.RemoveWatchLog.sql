CREATE OR ALTER PROCEDURE Flix.RemoveWatchLog
	@WatchLogID INT
AS

UPDATE Flix.ShowWatchLog 
SET
	IsDeleted = 1
WHERE
	WatchLogID = @WatchLogID;
GO