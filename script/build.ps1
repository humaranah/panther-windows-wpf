$version = Get-Content '.\version.txt';
&dotnet build --configuration Release --no-restore /p:Version=$version;

exit 0
