IF OBJECT_ID(N'Flix.ShowWatchLog') IS NULL
BEGIN
	CREATE TABLE Flix.ShowWatchLog
	(
		WatchLogID INT NOT NULL IDENTITY(1, 1),
		ShowID INT NOT NULL FOREIGN KEY
			REFERENCES Flix.Show(ShowID),
		UserID INT NOT NULL FOREIGN KEY
			REFERENCES Flix.[User](UserID),
		WatchedOn DATETIMEOFFSET NOT NULL DEFAULT(SYSDATETIMEOFFSET()),
		IsDeleted BIT NOT NULL,


		CONSTRAINT [PK_ShowWatchLog_WatchCount] PRIMARY KEY CLUSTERED
		(
			WatchLogID ASC
		),

		CONSTRAINT [UK_ShowWatchLog_UserIDShowIDDateWatched] UNIQUE NONCLUSTERED
		(
			ShowID,
			UserID,
			WatchedOn
		)

	);
END;