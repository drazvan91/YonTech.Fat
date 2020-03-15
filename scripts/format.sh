#!/bin/bash 

dotnet-format -f ../Yontech.Fat
dotnet-format -f ../Yontech.Fat.ConsoleRunner
dotnet-format -f ../Yontech.Fat.TestAdapter
dotnet-format -f ../Yontech.Fat.Templates

dotnet-format -f ../Yontech.Fat.Templates/templates/CreateConsoleFatProject
dotnet-format -f ../Yontech.Fat.Templates/templates/CreateFatProject
dotnet-format -f ../Yontech.Fat.Templates/templates/CreateFatProjectWithSamples

dotnet-format -f ../tests/Yontech.Fat.Tests

dotnet-format -f ../tests/SampleProjects/Alfa
dotnet-format -f ../tests/SampleProjects/Beta
dotnet-format -f ../tests/SampleProjects/Gama
dotnet-format -f ../tests/SampleProjects/Delta
