-- Returns all the possible genres for all the shows and movies in the database
CREATE OR ALTER PROCEDURE Flix.GetAllGenres
AS

SELECT G.GenreID, G.Genre
FROM Flix.Genre G
GO