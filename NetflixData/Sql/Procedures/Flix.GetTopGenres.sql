-- A procedure that returns the top(5) highest occuring genres 
-- returns the a list of genres that occures the most for a user
CREATE OR ALTER PROCEDURE Flix.GetTopGenres
@UserID INT
AS
WITH TopG(Genre, GenreCount, GenreID) AS
	(
		SELECT G.Genre, Count(*), G.GenreID
		FROM Flix.UserShowLibrary USL 
			INNER JOIN Flix.Show S ON USL.ShowID = S.ShowID
			INNER JOIN Flix.ShowGenres SG ON SG.ShowID = S.ShowID
			INNER JOIN Flix.Genre G ON G.GenreID = SG.GenreID
		WHERE USL.UserID = @UserID
		GROUP BY G.Genre, G.GenreID
	)
SELECT TOP(5) G.Genre, G.GenreID 
FROM TopG G
ORDER BY G.GenreCount DESC
GO
