#!/bin/bash 

NUGET_TOKEN="PUT_TOKEN_HERE"

FAT_VERSION="0.3.0"
CONSOLE_RUNNER_VERSION="0.3.0"
TEST_ADAPTER_VERSION="0.3.0"
TEMPLATES_VERSION="0.3.0"


echo "Pushing Yontech.Fat:$FAT_VERSION"
dotnet build ../Yontech.Fat/
dotnet pack ../Yontech.Fat/
dotnet nuget push ../Yontech.Fat/bin/Debug/Yontech.Fat.$FAT_VERSION.nupkg -k $NUGET_TOKEN -s https://api.nuget.org/v3/index.json

echo "Pushing Yontech.Fat.ConsoleRunner:$CONSOLE_RUNNER_VERSION"
dotnet build ../Yontech.Fat.ConsoleRunner/
dotnet pack ../Yontech.Fat.ConsoleRunner/
dotnet nuget push ../Yontech.Fat.ConsoleRunner/bin/Debug/Yontech.Fat.ConsoleRunner.$CONSOLE_RUNNER_VERSION.nupkg -k $NUGET_TOKEN -s https://api.nuget.org/v3/index.json

echo "Pushing Yontech.Fat.TestAdapter:$TEST_ADAPTER_VERSION"
dotnet build ../Yontech.Fat.TestAdapter/
dotnet pack ../Yontech.Fat.TestAdapter/
dotnet nuget push ../Yontech.Fat.TestAdapter/bin/Debug/Yontech.Fat.TestAdapter.$TEST_ADAPTER_VERSION.nupkg -k $NUGET_TOKEN -s https://api.nuget.org/v3/index.json

echo "Pushing Yontech.Fat.Templates:$TEMPLATES_VERSION"
dotnet build ../Yontech.Fat.Templates/
dotnet pack ../Yontech.Fat.Templates/
dotnet nuget push ../Yontech.Fat.Templates/bin/Debug/Yontech.Fat.Templates.$TEMPLATES_VERSION.nupkg -k $NUGET_TOKEN -s https://api.nuget.org/v3/index.json