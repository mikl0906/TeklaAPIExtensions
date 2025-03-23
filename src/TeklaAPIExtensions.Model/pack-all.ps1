$projectFile = "TeklaAPIExtensions.Model.csproj"
[xml]$project = Get-Content $projectFile
$baseVersion = $project.Project.PropertyGroup.Version

$tsVersions = @("2022", "2023", "2024", "2025")

foreach ($tsVersion in $tsVersions) {
    $packageVersion = "$baseVersion-ts$tsVersion"
    dotnet pack --configuration Release -p:TeklaVersion=$tsVersion -p:PackageVersion=$packageVersion
}
