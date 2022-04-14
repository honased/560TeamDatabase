INSERT Flix.Show(IsMovie, Title, ReleaseYear, AgeRating)
SELECT IIF(N.[Type] = N'Movie', 1, 0), N.Title, N.ReleaseYear, N.Rating
FROM Flix.Netflix N;