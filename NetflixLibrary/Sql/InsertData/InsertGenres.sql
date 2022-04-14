WITH ListedGenres AS 
	(
		SELECT value AS Genre
		FROM Flix.Netflix N
			CROSS APPLY STRING_SPLIT(N.ListedIn, ',')
	)
INSERT Flix.Genre(Genre) 
SELECT DISTINCT LG.Genre
FROM ListedGenres LG;