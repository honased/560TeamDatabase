CREATE PROCEDURE Flix.GetWatchLogs
    @UserID INT,
    @ShowID INT
AS
    SELECT SWC.
    FROM Flix.ShowWatchCount SWC
        WHERE SWC.ShowID = @ShowID AND SWC.UserID = @UserID;
GO