$buildNumber = $env:BUILD_NUMBER

$version = Get-Content '.\version.txt'
$version = "$version.$buildNumber"
Write-Host "Current version: $version"

&dotnet build --configuration Release --no-restore /p:Version=$version

exit 0
