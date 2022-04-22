WITH ListedPeople AS
	(
		SELECT TRIM(' ' FROM value) AS [Name]
		FROM Flix.Netflix N
			CROSS APPLY STRING_SPLIT(N.Director, ',')
		WHERE value <> N''

		UNION

		SELECT TRIM(' ' FROM value) AS [Name]
		FROM Flix.Netflix N
			CROSS APPLY STRING_SPLIT(N.[Cast], ',')
		WHERE value <> N''
	)
INSERT Flix.Person(FirstName, LastName)
SELECT DISTINCT SUBSTRING(LP.[Name], 0, LEN(LP.[Name]) - CHARINDEX(' ',REVERSE(LP.[Name])) + 2), 
	SUBSTRING(LP.[Name], LEN(LP.[Name]) - CHARINDEX(' ',REVERSE(LP.[Name])) + 2, LEN(LP.[Name]))
FROM ListedPeople LP
WHERE LP.[Name] <> N'';