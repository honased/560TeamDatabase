With ListedGenres AS 
	(
		SELECT value AS Genre, N.RowId AS ShowId
		FROM Flix.Netflix N
			CROSS APPLY STRING_SPLIT(N.ListedIn, ',')
	)
INSERT Flix.ShowGenres(ShowId, GenreId)
SELECT LG.ShowId, G.GenreID
FROM ListedGenres LG
	RIGHT JOIN Flix.Genre G ON G.Genre = LG.Genre;