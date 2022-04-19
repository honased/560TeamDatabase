Param(
   [string] $Server = "(localdb)\MSSQLLocalDb",
   [string] $Database = "DB560"
)

# Following code adapted from http://cosmonautdreams.com/2013/09/03/Getting-Powershell-to-run-in-64-bit.html
#############################################################################
#If Powershell is running the 32-bit version on a 64-bit machine, we
#need to force powershell to run in 64-bit mode .
#############################################################################
if ($env:PROCESSOR_ARCHITEW6432 -eq "AMD64") {
    write-warning "Swapping to 64 bit powershell process..."
    if ($myInvocation.Line) {
        &"$env:WINDIR\sysnative\windowspowershell\v1.0\powershell.exe" -NonInteractive -NoProfile $myInvocation.Line
    }else{
        &"$env:WINDIR\sysnative\windowspowershell\v1.0\powershell.exe" -NonInteractive -NoProfile -file "$($myInvocation.InvocationName)" $args
    }
exit $lastexitcode
}

#############################################################################
#End
#############################################################################

# Check if the SqlServer Module is installed. If not, install it
Write-Host "Checking for SqlServer Module..."
if (Get-Module -ListAvailable -Name SqlServer) {
   Write-Host "SqlServer Module Exists!"
}
else {
   Write-Host "SqlServer Module does not exists. Installing module..."
   Install-PackageProvider NuGet -Scope CurrentUser -Force;
   Set-PSRepository PSGallery -InstallationPolicy Trusted
   Install-Module SQLServer -Repository PSGallery -Scope CurrentUser
   Write-Host "Installed SqlServer Module!"
}

$CurrentDrive = (Get-Location).Drive.Name + ":"

Write-Host ""
Write-Host "Rebuilding database $Database on $Server..."

# Drop and Recreate the Database
& $PSScriptRoot\Scripts\DropDatabase.ps1 -Database $Database
& $PSScriptRoot\Scripts\CreateDatabase.ps1 -Database $Database

Write-Host "Creating schema..."
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Schemas\Flix.sql"

Write-Host "Creating tables..."
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Tables\User.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Tables\Show.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Tables\Person.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Tables\Genre.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Tables\Director.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Tables\ShowGenres.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Tables\Actor.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Tables\ShowReviews.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Tables\UserShowLibrary.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Tables\ShowWatchCount.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Tables\Netflix.sql"
#Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "PersonData\Sql\Tables\Person.AddressType.sql"
#Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "PersonData\Sql\Tables\Person.PersonAddress.sql"

Write-Host "Stored procedures..."
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Procedures\Flix.SearchShows.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\Procedures\Flix.LoginRegister.sql"
#Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "PersonData\Sql\Procedures\Person.RetrievePersons.sql"
#Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "PersonData\Sql\Procedures\Person.FetchPerson.sql"
#Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "PersonData\Sql\Procedures\Person.GetPerson.sql"
#Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "PersonData\Sql\Procedures\Person.SavePersonAddress.sql"
#Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "PersonData\Sql\Procedures\Person.RetrieveAddressesForPerson.sql"

Write-Host "Inserting data..."
$MyLoc = $PSScriptRoot
$Variables = "FilePath='$MyLoc\Data\netflix_titles.csv'", "FormatPath='$MyLoc\Data\Netflix.fmt'"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -Variable $Variables -InputFile $PSScriptRoot\"NetflixLibrary\Sql\InsertData\InsertSpreadsheetData.sql"
#bcp DB560.Flix.Netflix in "Data\netflix_titles.csv" -S "(localdb)\MSSQLLocalDb" -T -f Data\Netflix.fmt
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\InsertData\InsertShows.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\InsertData\InsertGenres.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\InsertData\InsertShowGenres.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\InsertData\InsertPeople.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\InsertData\InsertActors.sql"
Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile $PSScriptRoot\"NetflixLibrary\Sql\InsertData\InsertDirectors.sql"
#Invoke-SqlCmd -ServerInstance $Server -Database $Database -InputFile "PersonData\Sql\Data\Person.AddressType.sql"

Write-Host "Rebuild completed."
Write-Host ""

Set-Location $CurrentDrive
