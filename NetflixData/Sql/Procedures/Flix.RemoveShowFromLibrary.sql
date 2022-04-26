--This procedure removes a show from a user's library by setting its IsDeleted column to one.
CREATE OR ALTER PROCEDURE Flix.RemoveShowFromLibrary
	@UserID INT,
	@ShowID INT
AS

UPDATE Flix.UserShowLibrary 
SET
	IsDeleted = 1
WHERE
	UserID = @UserID AND
	ShowID = @ShowID;
GO