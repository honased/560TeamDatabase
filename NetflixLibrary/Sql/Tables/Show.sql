IF OBJECT_ID(N'Flix.Show') IS NULL
BEGIN
	CREATE TABLE Flix.Show
	(
		ShowID INT NOT NULL IDENTITY(1, 1),
		IsMovie BIT NOT NULL,
		Title NVARCHAR(128) NOT NULL,
		ReleaseYear INT NOT NULL,
		AgeRating NVARCHAR(8) NOT NULL,

		CONSTRAINT [PK_Show_ShowID] PRIMARY KEY CLUSTERED
		(
			ShowID ASC
		),

		CONSTRAINT [UK_Show_TitleReleaseYear] UNIQUE NONCLUSTERED
		(
			Title,
			ReleaseYear
		)

	);
END;
