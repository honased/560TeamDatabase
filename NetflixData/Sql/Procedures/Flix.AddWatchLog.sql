-- Adds a show to a user's watch log based off UserID and ShowID
-- Auto generates the time they watch at, and Inserts userID and ShowId and adds that it is not deleted
CREATE OR ALTER PROCEDURE Flix.AddWatchLog
	@UserID INT,
	@ShowID INT
AS

INSERT Flix.ShowWatchLog(UserID, ShowID, IsDeleted)
VALUES(@UserID, @ShowID, 0);

GO