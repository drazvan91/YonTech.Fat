#!/bin/bash 

echo "Test create-console-fat-project"
dotnet clean ../Yontech.Fat.Templates/templates/CreateConsoleFatProject/
dotnet run --project ../Yontech.Fat.Templates/templates/CreateConsoleFatProject/

echo "Test create-fat-project"
dotnet clean ../Yontech.Fat.Templates/templates/CreateFatProject/
dotnet test ../Yontech.Fat.Templates/templates/CreateFatProject/

echo "Test create-fat-project-with-samples"
dotnet clean ../Yontech.Fat.Templates/templates/CreateFatProjectWithSamples/
dotnet test ../Yontech.Fat.Templates/templates/CreateFatProjectWithSamples/