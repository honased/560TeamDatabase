CREATE OR ALTER PROCEDURE Flix.GetMyMostWatchedShows
	@UserID INT
AS

WITH TopWatched(ShowID, CountOf) AS
(
	SELECT TOP(5)
		S.ShowID, COUNT(*) AS COUNTOF
	FROM Flix.ShowWatchCount S
	WHERE S.UserID = @UserID
	GROUP BY S.ShowID
	ORDER BY COUNTOF DESC
)

SELECT
	S.ShowID AS ShowID,
	S.Title, 
	S.ReleaseYear
	FROM TopWatched TW
		INNER JOIN Flix.Show S ON TW.ShowID = S.ShowID;
	