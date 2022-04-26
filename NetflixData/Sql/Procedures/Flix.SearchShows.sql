-- This procedure performs a variety of filters to search through the show database.
-- It allows for a title, director, release year, and genre to each be either specified
-- or ignored. The SearchLibrary bit also allows for a toggle between searching for shows
-- either in our outside of a User's library.
CREATE OR ALTER PROCEDURE Flix.SearchShows
	@UserID INT,
	@Title NVARCHAR(128),
	@Director NVARCHAR(64),
	@ReleaseYear INT,
	@GenreID INT,
	@SearchLibrary BIT
AS

WITH UserLibrary(ShowID) AS
	(
		SELECT USL.ShowID
		FROM Flix.[User] U
			INNER JOIN Flix.[UserShowLibrary] USL ON USL.UserID = U.UserID
		WHERE U.UserID = @UserID AND USL.IsDeleted = 0
	)
SELECT S.ShowID, S.Title, S.ReleaseYear
FROM Flix.Show S
	LEFT JOIN Flix.Director D ON D.ShowID = S.ShowID
	LEFT JOIN Flix.Person P ON P.PersonID = D.PersonID
	LEFT JOIN Flix.ShowGenres SG ON SG.ShowID = S.ShowID
	LEFT JOIN Flix.Genre G ON G.GenreID = SG.GenreID
WHERE ((@SearchLibrary = 1 AND S.ShowID IN
	(
		SELECT UL.ShowID
		FROM UserLibrary UL
	)) OR (@SearchLibrary = 0 AND S.ShowID NOT IN
	(
		SELECT UL.ShowID
		FROM UserLibrary UL
	)))
	AND CHARINDEX(IIF(@Title = N'', S.Title, @Title), S.Title, 0) > 0
	AND ((D.DirectorID IS NULL AND @Director = N'') OR CHARINDEX(IIF(@Director = N'', P.FirstName + N' ' + P.LastName, @Director), P.FirstName + N' ' + P.LastName, 0) > 0)
	AND ISNULL(@ReleaseYear, S.ReleaseYear) = S.ReleaseYear
	AND ISNULL(@GenreID, SG.GenreID) = SG.GenreID
GROUP BY S.ShowID, S.Title, S.ReleaseYear
ORDER BY S.Title ASC, S.ReleaseYear ASC;
GO