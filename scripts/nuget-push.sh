#!/bin/bash 

NUGET_TOKEN="PUT_TOKEN_HERE"

FAT_VERSION="0.9.1"
CONSOLE_RUNNER_VERSION="0.9.1"
TEST_ADAPTER_VERSION="0.9.1"
TEMPLATES_VERSION="0.8.1"


echo "Pushing Yontech.Fat:$FAT_VERSION"
dotnet build ../Yontech.Fat/ || exit 1
dotnet pack ../Yontech.Fat/ || exit 1
dotnet nuget push ../Yontech.Fat/bin/Debug/Yontech.Fat.$FAT_VERSION.nupkg -k $NUGET_TOKEN -s https://api.nuget.org/v3/index.json

echo "Pushing Yontech.Fat.ConsoleRunner:$CONSOLE_RUNNER_VERSION"
dotnet build ../Yontech.Fat.ConsoleRunner/ || exit 1
dotnet pack ../Yontech.Fat.ConsoleRunner/ || exit 1
dotnet nuget push ../Yontech.Fat.ConsoleRunner/bin/Debug/Yontech.Fat.ConsoleRunner.$CONSOLE_RUNNER_VERSION.nupkg -k $NUGET_TOKEN -s https://api.nuget.org/v3/index.json

echo "Pushing Yontech.Fat.TestAdapter:$TEST_ADAPTER_VERSION"
dotnet build ../Yontech.Fat.TestAdapter/ || exit 1
dotnet pack ../Yontech.Fat.TestAdapter/ || exit 1
dotnet nuget push ../Yontech.Fat.TestAdapter/bin/Debug/Yontech.Fat.TestAdapter.$TEST_ADAPTER_VERSION.nupkg -k $NUGET_TOKEN -s https://api.nuget.org/v3/index.json

echo "Pushing Yontech.Fat.Templates:$TEMPLATES_VERSION"
dotnet build ../Yontech.Fat.Templates/ || exit 1
dotnet pack ../Yontech.Fat.Templates/ || exit 1
dotnet nuget push ../Yontech.Fat.Templates/bin/Debug/Yontech.Fat.Templates.$TEMPLATES_VERSION.nupkg -k $NUGET_TOKEN -s https://api.nuget.org/v3/index.json