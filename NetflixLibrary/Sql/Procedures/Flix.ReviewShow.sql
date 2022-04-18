CREATE OR ALTER PROCEDURE Flix.ReviewShow
	@UserID INT,
	@ShowID INT,
	@Review INT
AS

WITH ReviewCTE(UserID, ShowID) AS
	(
	SELECT @UserID, @ShowID
	)
MERGE Flix.ShowReviews SR
USING ReviewCTE ON SR.UserID = ReviewCTE.UserID
	AND SR.ShowID = ReviewCTE.ShowID
WHEN MATCHED THEN
	UPDATE
		SET
		Review = @Review,
		IsDeleted = 0
WHEN NOT MATCHED THEN
	INSERT(UserID, ShowID, Review, IsDeleted)
	VALUES(ReviewCTE.UserID, ReviewCTE.ShowID, @Review, 0);
GO