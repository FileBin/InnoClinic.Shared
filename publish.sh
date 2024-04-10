#!/usr/bin/env bash

. .env

dotnet pack -c Release || exit 1

read -r -d '' PROJECTS <<- EOF
Domain.Abstractions
Domain.Models
Exceptions
Misc
TestUtils
EOF

echo "$PROJECTS" | while read proj;
do
    directory="$(pwd)"
    cd "$proj"
    prev_version="$(cat '.version.prev')"
    current_version="$(cat InnoClinic.Shared.$proj.csproj | grep -oPm1 "(?<=<Version>)[^<]+")"
    if [ "$current_version" != "$prev_version" ]; then
        dotnet nuget push \
        .build/bin/Release/Filebin.InnoClinic.Shared.$proj.$current_version.nupkg \
        -k $NUGET_API_KEY \
        -s https://api.nuget.org/v3/index.json && \
        echo "$current_version" > 'version.prev'
    fi
    cd $directory
done

# dotnet nuget push Domain.Abstractions/.build/bin/Debug/Filebin.InnoClinic.Shared.Domain.Abstractions.1.0.0.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
# dotnet nuget push Domain.Models/.build/bin/Debug/Filebin.InnoClinic.Shared.Domain.Models.1.0.0.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
# dotnet nuget push Exceptions/.build/bin/Debug/Filebin.InnoClinic.Shared.Exceptions.1.0.0.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
# dotnet nuget push Misc/.build/bin/Debug/Filebin.InnoClinic.Shared.Misc.1.0.0.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
# dotnet nuget push TestUtils/.build/bin/Debug/Filebin.InnoClinic.Shared.TestUtils.1.0.0.nupkg -k $NUGET_API_KEY -s https://api.nuget.org/v3/index.json
