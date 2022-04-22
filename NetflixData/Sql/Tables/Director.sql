IF OBJECT_ID(N'Flix.Director') IS NULL
BEGIN
	CREATE TABLE Flix.Director
	(
		DirectorID INT NOT NULL IDENTITY(1, 1),
		ShowID INT NOT NULL FOREIGN KEY
			REFERENCES Flix.Show(ShowID),
		PersonID INT NOT NULL FOREIGN KEY
			REFERENCES Flix.Person(PersonID),


		CONSTRAINT [PK_Director_DirectorID] PRIMARY KEY CLUSTERED
		(
			DirectorID ASC
		),

		CONSTRAINT [UK_Director_ShowPerson] UNIQUE NONCLUSTERED
		(
			ShowID,
			PersonID
		)

	);
END;