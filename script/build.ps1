Write-Host 'Getting current version file'
$version = Get-Content '.\version.txt';
Write-Host "Current version: $version"

&dotnet build --configuration Release --no-restore /p:Version=$version;

exit 0
