#!/usr/bin/env bash

. .env

dotnet restore
dotnet build -c Release
dotnet pack -c Release
dotnet nuget push Domain.Abstractions/.build/bin/Debug/Filebin.InnoClinic.Shared.Domain.Abstractions.1.0.0.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
dotnet nuget push Domain.Models/.build/bin/Debug/Filebin.InnoClinic.Shared.Domain.Models.1.0.0.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
dotnet nuget push Exceptions/.build/bin/Debug/Filebin.InnoClinic.Shared.Exceptions.1.0.0.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
dotnet nuget push Misc/.build/bin/Debug/Filebin.InnoClinic.Shared.Misc.1.0.0.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
dotnet nuget push TestUtils/.build/bin/Debug/Filebin.InnoClinic.Shared.TestUtils.1.0.0.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
