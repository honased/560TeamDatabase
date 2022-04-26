--This procedure adds a show to a user's library.
--If the show is already in the library it does nothing unless it was deleted in which it adds it back in.
CREATE OR ALTER PROCEDURE Flix.AddShowToLibrary
	@UserID INT,
	@ShowID INT
AS

WITH NewShowCTE(UserID, ShowID) AS
	(
		SELECT @UserID, @ShowID
	)
MERGE Flix.UserShowLibrary USL
USING NewShowCTE NSC ON NSC.UserID = USL.UserID
	AND NSC.ShowID = USL.ShowID
WHEN MATCHED THEN
	UPDATE
		SET IsDeleted = 0
WHEN NOT MATCHED THEN
	INSERT(UserID, ShowID, IsDeleted)
	VALUES(NSC.UserID, NSC.ShowID, 0);
GO