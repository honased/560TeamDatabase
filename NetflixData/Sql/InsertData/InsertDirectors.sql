WITH ListedPeople AS
(
	SELECT TRIM(' ' FROM value) AS [Name], N.RowId AS ShowId
	FROM Flix.Netflix N
		CROSS APPLY STRING_SPLIT(N.Director, ',')
	WHERE value <> N''
)
INSERT Flix.Director(ShowId, PersonId)
SELECT LP.ShowId, P.PersonID
FROM ListedPeople LP
	INNER JOIN Flix.Person P ON P.FirstName = SUBSTRING(LP.[Name], 0, LEN(LP.[Name]) - CHARINDEX(' ',REVERSE(LP.[Name])) + 2)
	AND P.LastName = SUBSTRING(LP.[Name], LEN(LP.[Name]) - CHARINDEX(' ',REVERSE(LP.[Name])) + 2, LEN(LP.[Name]));