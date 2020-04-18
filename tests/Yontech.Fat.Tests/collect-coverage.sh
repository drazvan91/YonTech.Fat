dotnet test --collect:"XPlat Code Coverage"
dotnet reportgenerator "-reports:TestResults/coverage.cobertura.xml" "-targetdir:TestResults/html" -reporttypes:HTML
open TestResults/html/index.htm 