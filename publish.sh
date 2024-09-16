#!/usr/bin/env bash

. .env

dotnet pack -c Release || exit 1

read -r -d '' PROJECTS <<- EOF
Domain.Abstractions
Domain.Models
Exceptions
Misc
TestUtils
Messaging.Contracts
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
        echo "$current_version" > '.version.prev'
    fi
    cd $directory
done