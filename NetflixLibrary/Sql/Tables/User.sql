﻿IF OBJECT_ID(N'Flix.User') IS NULL
BEGIN
	CREATE TABLE Flix.[User]
	(
		UserID INT NOT NULL IDENTITY(1, 1),
		Username NVARCHAR(64) NOT NULL,

		CONSTRAINT [PK_User_UserID] PRIMARY KEY CLUSTERED
		(
			UserID ASC
		),

		CONSTRAINT [UK_User_Username] UNIQUE NONCLUSTERED
		(
			Username
		)

	);
END;