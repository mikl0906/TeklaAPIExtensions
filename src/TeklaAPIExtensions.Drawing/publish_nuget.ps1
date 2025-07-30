dotnet pack

# Set the source folder where the .nupkg files are built.
$sourceFolder = "bin\Release"

# Get the latest .nupkg file by LastWriteTime
$latestPackage = Get-ChildItem -Path $sourceFolder -Filter "*.nupkg" | 
Sort-Object LastWriteTime -Descending | 
Select-Object -First 1

if ($null -ne $latestPackage) {
    Write-Host "Publishing package:" $latestPackage.Name
    dotnet nuget push $latestPackage --source "nuget.org"
}
else {
    Write-Host "No .nupkg file found in $sourceFolder"
}