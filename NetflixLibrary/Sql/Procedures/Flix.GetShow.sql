CREATE OR ALTER PROCEDURE Flix.GetShow
	@UserID INT,
	@ShowID INT
AS
WITH Reviews(UserID, Review) AS
	(
		SELECT SR.UserID, SR.Review
		FROM Flix.ShowReviews SR
		WHERE SR.IsDeleted = 0 AND SR.ShowID = @ShowID
	)
SELECT S.ShowID, S.Title, S.IsMovie, ISNULL(S.AgeRating, N'Unknown') AS AgeRating, S.ReleaseYear,
	(
		SELECT ISNULL(STRING_AGG(G.Genre, ','), N'')
		FROM Flix.Genre G
			INNER JOIN Flix.ShowGenres SG ON SG.GenreID = G.GenreID
		WHERE SG.ShowID = S.ShowID
	) AS Genres,
	(
		SELECT ISNULL(STRING_AGG(P.FirstName + N' ' + P.LastName, ','), N'')
		FROM Flix.Actor A
			INNER JOIN Flix.Person P ON P.PersonID = A.PersonID
		WHERE A.ShowID = S.ShowID
	) AS [Cast],
	(
		SELECT ISNULL(STRING_AGG(P.FirstName + N' ' + P.LastName, ','), N'')
		FROM Flix.Director D
			INNER JOIN Flix.Person P ON P.PersonID = D.PersonID
		WHERE D.ShowID = S.ShowID
	) AS Directors,
	(
		SELECT R.Review
		FROM Reviews R
		WHERE R.UserID = @UserID
	) AS MyReview,
	(
		SELECT CAST(SUM(R.Review) / COUNT(*) AS float)
		FROM Reviews R
	) AS AverageReview
FROM Flix.Show S
	WHERE S.ShowID = @ShowID;
GO