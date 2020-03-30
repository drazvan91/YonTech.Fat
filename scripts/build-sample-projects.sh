#!/bin/bash 

echo "Test Afla"
dotnet clean ../tests/SampleProjects/Alfa/
dotnet run --project ../tests/SampleProjects/Alfa/ || exit 1

echo "Test Beta"
dotnet clean ../tests/SampleProjects/Beta/
dotnet test ../tests/SampleProjects/Beta/ || exit 1

echo "Test Delta"
dotnet clean ../tests/SampleProjects/Delta/
dotnet test ../tests/SampleProjects/Delta/ || exit 1

echo "Test Gama"
dotnet clean ../tests/SampleProjects/Gama/
dotnet test ../tests/SampleProjects/Gama/ || exit 1