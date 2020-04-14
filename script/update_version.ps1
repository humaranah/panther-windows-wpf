function Set-NewDevelopVersion {
    param (
        $version
    )

    $versionArray = $version.Split(".");
    $versionArray[1] = ($versionArray -as [int]) + 1;
    return $versionArray -join ".";
}

function Set-HotfixVersionNumber {
    param (
        $version
    )

    $versionArray = $version.Split(".");
    $versionArray[2] = ($versionArray -as [int]) + 1;
    return $versionArray -join '.';
}


Write-Host "Updating Version Number...";

$env = $args[0];
$currentVersion = Get-Content '.\version.txt';
Write-Host "Current version: $currentVersion";

switch ($env) {
    'release' {
        Write-Host "Setting new version for release.";
        $newVersion = Set-MinorVersionNumber $currentVersion;
        $newVersion | Set-Content '.\version.txt';
        break
    }
    'hotfix' {
        Write-Host "Setting new version for hotfix.";
        $newVersion = Set-HotfixVersionNumber $currentVersion;
        $newVersion | Set-Content '.\version.txt';
        break
    }
    Default {
        Write-Error "No environment specified!";
        exit 1;
    }
}

Write-Host "New version number: $newVersion";
exit 0
