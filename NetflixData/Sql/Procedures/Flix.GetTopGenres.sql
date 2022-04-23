CREATE OR ALTER PROCEDURE Flix.GetTopGenres
@UserID INT
AS
WITH TopG(Genre, GenreCount, GenreID) AS
	(
		SELECT G.Genre, Count(*), GenreID
		FROM Flix.UserShowLibrary USL 
			INNER JOIN Flix.Show S ON USL.ShowID = S.ShowID
			INNER JOIN Flix.ShowGenres SG ON SG.ShowID = S.ShowID
			INNER JOIN Flix.Genre G ON G.GenreID = SG.GenreID
		WHERE USL.UserID = @UserID
		GROUP BY G.Genre
	)
SELECT TOP(5) G.Genre, G.GenreID 
FROM Flix.TopG G
ORDER BY G.GenreCount DESC
GO
