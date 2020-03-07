#!/bin/bash 

dotnet-format -f ../Yontech.Fat
dotnet-format -f ../Yontech.Fat.ConsoleRunner
dotnet-format -f ../Yontech.Fat.TestAdapter
dotnet-format -f ../Yontech.Fat.Templates

dotnet-format -f ../Yontech.Fat.Templates/templates/CreateConsoleFatProject
dotnet-format -f ../Yontech.Fat.Templates/templates/CreateFatProject
dotnet-format -f ../Yontech.Fat.Templates/templates/CreateFatProjectWithSamples