CREATE OR ALTER PROCEDURE Flix.SearchShows
	@UserID INT,
	@Title NVARCHAR(128),
	@ReleaseYear INT
AS

WITH UserLibrary(ShowID) AS
	(
		SELECT USL.ShowID
		FROM Flix.[User] U
			INNER JOIN Flix.[UserShowLibrary] USL ON USL.UserID = U.UserID
		WHERE U.UserID = @UserID AND USL.IsDeleted = 0
	)
SELECT S.Title
FROM Flix.Show S
WHERE S.ShowID NOT IN
	(
		SELECT UL.ShowID
		FROM UserLibrary UL
	)
	AND CHARINDEX(IIF(@Title = N'', S.Title, @Title), S.Title, 0) > 0
	AND ISNULL(@ReleaseYear, S.ReleaseYear) = S.ReleaseYear;
GO