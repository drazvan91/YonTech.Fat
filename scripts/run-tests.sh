#!/bin/bash 

echo "Run unit tests"
dotnet clean ../tests/Yontech.Fat.Tests/
dotnet test ../tests/Yontech.Fat.Tests/ || exit 1

echo "Run RealWorldAngular tests"
dotnet clean ../tests/RealWorldProjects/RealWorld.Angular.Tests/
dotnet run --project ../tests/RealWorldProjects/RealWorld.Angular.Tests/ || exit 1
