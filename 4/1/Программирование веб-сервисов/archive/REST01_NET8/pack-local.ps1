$ErrorActionPreference = 'Stop'
Set-Location $PSScriptRoot
New-Item -ItemType Directory -Force local-nuget | Out-Null
dotnet pack .\ResultsCollection\ResultsCollection.csproj -c Release -o .\local-nuget
dotnet pack .\ResultsAuthenticate\ResultsAuthenticate.csproj -c Release -o .\local-nuget
dotnet restore .\REST01\REST01.csproj -p:UseLocalNuget=true
dotnet build .\REST01\REST01.csproj -c Release --no-restore -p:UseLocalNuget=true
Write-Host 'Local NuGet packages built. To use them: dotnet run --project REST01 -p:UseLocalNuget=true'
