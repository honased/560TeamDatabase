CREATE OR ALTER PROCEDURE Flix.GetSimilarShows
	@UserID INT,
	@ShowID INT
AS

WITH ShowGenresCTE AS
(
	SELECT SG.GenreID
	FROM Flix.ShowGenres SG
	WHERE SG.ShowID = 65
),
GenreMatchCTE AS
(
	SELECT USL.ShowID, USL.UserID
	FROM Flix.UserShowLibrary USL
		INNER JOIN Flix.ShowGenres SG ON SG.ShowID = USL.ShowID
	WHERE SG.ShowID <> @ShowID AND USL.UserID <> @UserID AND
		SG.GenreID IN (SELECT SGC.GenreID FROM ShowGenresCTE SGC)
	GROUP BY USL.UserID, USL.ShowID
	HAVING COUNT(*) = (SELECT COUNT(*) FROM ShowGenresCTE SGC)
)
SELECT TOP(10) GMC.ShowID, S.Title, S.ReleaseYear
FROM GenreMatchCTE GMC
	INNER JOIN Flix.Show S ON GMC.ShowID = S.ShowID
	LEFT JOIN Flix.ShowReviews SR ON SR.ShowID = GMC.ShowID AND SR.UserID = GMC.UserID
	LEFT JOIN Flix.ShowWatchCount SWC ON SWC.ShowID = GMC.ShowID AND SWC.UserID = GMC.UserID
GROUP BY GMC.ShowID, S.Title, S.AgeRating, S.IsMovie, S.ReleaseYear, S.ShowID
ORDER BY COUNT(*) DESC, 
	AVG(IIF(SWC.IsDeleted = 1, 0, ISNULL(SWC.WatchCount, 0))) DESC,
	AVG(IIF(SR.IsDeleted = 1, 0, ISNULL(SR.Review, 0))) DESC;
GO