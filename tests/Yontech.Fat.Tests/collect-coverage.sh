# dotnet test --collect:"XPlat Code Coverage" --logger:"console;verbosity=normal"
dotnet reportgenerator "-reports:TestResults/coverage.cobertura.xml" "-targetdir:TestResults/html" -reporttypes:HTML
open TestResults/html/index.htm 