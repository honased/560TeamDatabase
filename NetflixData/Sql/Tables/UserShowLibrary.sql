IF OBJECT_ID(N'Flix.UserShowLibrary') IS NULL
BEGIN
	CREATE TABLE Flix.UserShowLibrary
	(
		ShowLibraryID INT NOT NULL IDENTITY(1, 1),
		ShowID INT NOT NULL FOREIGN KEY
			REFERENCES Flix.Show(ShowID),
		UserID INT NOT NULL FOREIGN KEY
			REFERENCES Flix.[User](UserID),
		IsDeleted BIT NOT NULL,


		CONSTRAINT [PK_UserShowLibrary_ShowLibraryID] PRIMARY KEY CLUSTERED
		(
			ShowLibraryID ASC
		),

		CONSTRAINT [UK_UserShowLibrary_UserIDShowID] UNIQUE NONCLUSTERED
		(
			ShowID,
			UserID
		)

	);
END;