CREATE OR ALTER PROCEDURE Flix.GetMyFavoriteShows
	@UserID INT
AS

SELECT TOP(5)
		SR.ShowID AS ShowID,
		S.Title,
		S.ReleaseYear
	FROM Flix.ShowReviews SR
		INNER JOIN Flix.Show S ON SR.ShowID = S.ShowID
	WHERE SR.UserID = @UserID
	ORDER BY SR.Review DESC;

