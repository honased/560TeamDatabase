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
	RIGHT JOIN Flix.Person P ON P.FirstName + ' ' + P.LastName = LP.[Name]
WHERE LP.ShowId IS NOT NULL;