IF OBJECT_ID(N'Flix.Actor') IS NULL
BEGIN
	CREATE TABLE Flix.Actor
	(
		ActorID INT NOT NULL IDENTITY(1, 1),
		ShowID INT NOT NULL FOREIGN KEY
			REFERENCES Flix.Show(ShowID),
		PersonID INT NOT NULL FOREIGN KEY
			REFERENCES Flix.Person(PersonID),


		CONSTRAINT [PK_Actor_ActorID] PRIMARY KEY CLUSTERED
		(
			ActorID ASC
		),

		CONSTRAINT [UK_Actor_PersonIDShowID] UNIQUE NONCLUSTERED
		(
			ShowID,
			PersonID
		)

	);
END;