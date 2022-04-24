With ListedGenres AS 
	(
		SELECT TRIM(' ' FROM value) AS Genre,N.RowId AS ShowId
		FROM Flix.Netflix N
			CROSS APPLY STRING_SPLIT(N.ListedIn, ',')
		WHERE TRIM(' ' FROM value) <> ''
	)
INSERT Flix.ShowGenres(ShowId, GenreId)
SELECT LG.ShowId, G.GenreID
FROM ListedGenres LG
	RIGHT JOIN Flix.Genre G ON G.Genre = LG.Genre;