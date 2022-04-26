-- A procedure that returns the top(5) most watched shows for a user
-- Returns a list of shows that occurs most in the show watch log
CREATE OR ALTER PROCEDURE Flix.GetMyMostWatchedShows
	@UserID INT
AS

WITH TopWatched(ShowID, CountOf) AS
(
	SELECT TOP(5)
		S.ShowID, COUNT(*) AS COUNTOF
	FROM Flix.ShowWatchLog S
	WHERE S.UserID = @UserID AND S.IsDeleted = 0
	GROUP BY S.ShowID
	ORDER BY COUNTOF DESC
)

SELECT
	S.ShowID AS ShowID,
	S.Title,
	S.ReleaseYear
	FROM TopWatched TW
		INNER JOIN Flix.Show S ON TW.ShowID = S.ShowID;