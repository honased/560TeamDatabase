IF OBJECT_ID(N'Flix.ShowWatchCount') IS NULL
BEGIN
	CREATE TABLE Flix.ShowWatchCount
	(
		WatchCount INT NOT NULL,
		ShowID INT NOT NULL FOREIGN KEY
			REFERENCES Flix.Show(ShowID),
		UserID INT NOT NULL FOREIGN KEY
			REFERENCES Flix.[User](UserID),
		DateWatched DATETIMEOFFSET NOT NULL DEFAULT(SYSDATETIMEOFFSET()),
		IsDeleted BIT NOT NULL,


		CONSTRAINT [PK_ShowWatchCount_WatchCount] PRIMARY KEY CLUSTERED
		(
			WatchCount ASC
		),

		CONSTRAINT [UK_ShowWatchCount_UserIDShowIDDateWatched] UNIQUE NONCLUSTERED
		(
			ShowID,
			UserID,
			DateWatched
		)

	);
END;