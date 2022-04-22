IF NOT EXISTS
   (
      SELECT *
      FROM sys.schemas s
      WHERE s.[name] = N'Flix'
   )
BEGIN
   EXEC(N'CREATE SCHEMA [Flix] AUTHORIZATION [dbo]');
END;
