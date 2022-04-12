IF OBJECT_ID(N'Flix.ShowReviews') IS NULL
BEGIN
	CREATE TABLE Flix.ShowReviews
	(
		ShowReviewsID INT NOT NULL IDENTITY(1, 1),
		ShowID INT NOT NULL FOREIGN KEY
			REFERENCES Flix.Show(ShowID),
		UserID INT NOT NULL FOREIGN KEY
			REFERENCES Flix.[User](UserID),
		Review INT NOT NULL,
		IsDeleted BIT NOT NULL,


		CONSTRAINT [PK_ShowReviews_ShowReviewsID] PRIMARY KEY CLUSTERED
		(
			ShowReviewsID ASC
		),

		CONSTRAINT [UK_ShowReview_UserIDShowID] UNIQUE NONCLUSTERED
		(
			ShowID,
			UserID
		)



	);
END;


IF NOT EXISTS
   (
      SELECT *
      FROM sys.check_constraints cc
      WHERE cc.parent_object_id = OBJECT_ID(N'Flix.ShowReviews')
         AND cc.[name] = N'CK_Flix_ShowReview_Review'
   )
BEGIN
   ALTER TABLE Flix.ShowReviews
   ADD CONSTRAINT [CK_Flix_ShowReview_Review] CHECK
   (
      Review <= 5 AND Review >= 1
   )
END;
