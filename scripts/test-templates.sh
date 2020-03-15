#!/bin/bash 

echo "Test create-console-fat-project"
dotnet clean ../Yontech.Fat.Templates/templates/CreateConsoleFatProject/
dotnet run --project ../Yontech.Fat.Templates/templates/CreateConsoleFatProject/ || exit 1

echo "Test create-fat-project"
dotnet clean ../Yontech.Fat.Templates/templates/CreateFatProject/
dotnet test ../Yontech.Fat.Templates/templates/CreateFatProject/ || exit 1

echo "Test create-fat-project-with-samples"
dotnet clean ../Yontech.Fat.Templates/templates/CreateFatProjectWithSamples/
dotnet test ../Yontech.Fat.Templates/templates/CreateFatProjectWithSamples/ || exit 1