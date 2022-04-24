WITH ListedGenres AS 
	(
		SELECT TRIM(' ' FROM value) AS Genre
		FROM Flix.Netflix N
			CROSS APPLY STRING_SPLIT(N.ListedIn, ',')
		WHERE TRIM(' ' FROM value) <> ''
	)
INSERT Flix.Genre(Genre) 
SELECT DISTINCT LG.Genre
FROM ListedGenres LG;