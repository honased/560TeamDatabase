------------------------------------------------------------------
SQL Scripts location

Tables:                     NetflixData/Sql/Tables/
Procedures/SQL Operations:  NetflixData/Sql/Procedures/
Data:                       NetflixData/Sql/Data/
Schemas:                    NetflixData/Sql/Schemas/
OtherObjects:               [There are no other objects in this project]

Database Creation/Drop:     Scripts/

Recreating everything
    - Run the powershell script "RebuildDatabase.ps1" from the main directory.

------------------------------------------------------------------
Projects

Overall, all projects are housed in a solution called NetflixLibrary (NetflixLibrary.sln).

There are 4 separate projects in this solution each represented by a subfolder. They are listed below

DataAccess (C#)
    - Data Access library provided by John Keller.
    - Nothing has been modified from the original source.

NetflixData (C#)
    - Contains all SQL Scripts, Data Delegates, and models

    - Data Delegates can be found in "DataDelegates" subfolder
        - Roughly, each Data Delegate in this folder maps to a SQL Stored Procedure

    - SQL Scripts can be found in "Sql" subfolder
        - Refer to Section "SQL Scripts loaction" above

    - All Models can be found in "Models" subfolder
        - These roughly map into specific tables from the Sql folder

NetflixLibrary (C# / WPF)
    - The View of the project implemented in WPF
    - All views are represented by a view definition "*.xaml" and the codebehind "*.xaml.cs"

    - The subfolder "Views" contains all views for the project
        - This includes the Login Screen, Library Page, User Stats, Etc.

    - The class "MainWindow" aggregates the Login Screen and the overall application Page
        - Handles swapping between these two views

    - The files "App.xaml[.cs]" and "AssemblyInfo.cs" can be ignored (we did not change anything)

BuildDatabase (C# / Empty Project)
    - This project is not needed and can be excluded.
    - This project does not contain any code, and it doesn't build with the rest of the solution
    - Instead, it exists as a way to run the powershell script from within Visual Studio
    - This is accomplished by right-clicking on the project, and hitting build
        - The powershell script "RebuildDatabase.ps1" is run as a build event
    - However, not everyone's powershell is on the same version, so this may not work.
    - If it doesn't work, it is best to just call the "RebuildDatabase.ps1" manually.

------------------------------------------------------------------

Final Project dependencies ( -> indicates depends on )

    BuildDatabase ->    Nothing
    DataAccess ->       Nothing
    NetflixData ->      DataAccess
    NetflixLibrary ->   NetflixData

As such, the final dependency chain is:
    NetflixLibrary -> NetflixData -> DataAccess