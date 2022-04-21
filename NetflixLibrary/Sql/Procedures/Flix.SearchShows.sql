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
SELECT S.ShowID, S.Title, ISNULL(S.AgeRating, N'Unknown') AS AgeRating, S.IsMovie, S.ReleaseYear,
	(
		SELECT ISNULL(STRING_AGG(G2.Genre, ','), N'')
		FROM Flix.ShowGenres SG2
			INNER JOIN Flix.Genre G2 ON G2.GenreID = SG2.GenreID
		WHERE SG2.ShowID = S.ShowID
	) AS Genres,
	(
		SELECT ISNULL(STRING_AGG(P2.FirstName + N' ' + P2.LastName, ','), N'')
		FROM Flix.Actor A
			INNER JOIN Flix.Person P2 ON P2.PersonID = A.PersonID
		WHERE A.ShowID = S.ShowID
	) AS [Cast],
	(
		SELECT ISNULL(STRING_AGG(P3.FirstName + N' ' + P3.LastName, ','), N'')
		FROM Flix.Director D2
			INNER JOIN Flix.Person P3 ON D2.PersonID = P3.PersonID
		WHERE D2.ShowID = S.ShowID
	) AS Directors
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
GROUP BY S.ShowID, S.Title, S.AgeRating, S.IsMovie, S.ReleaseYear;
GO