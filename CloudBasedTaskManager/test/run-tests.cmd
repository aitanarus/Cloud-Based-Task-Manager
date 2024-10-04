@echo off
REM Create test results directory if it doesn't exist
if not exist "test_results" (
    mkdir "test_results"
)

REM Run tests for each project
echo Running End-to-End Tests...
dotnet test "E2ETests\E2ETests.csproj" --collect:"XPlat Code Coverage" --results-directory test_results

echo Running Integration Tests...
dotnet test "IntegrationTests\IntegrationTests.csproj" --collect:"XPlat Code Coverage" --results-directory test_results

echo Running Unit Tests...
dotnet test "UnitTests\UnitTests.csproj" --collect:"XPlat Code Coverage" --results-directory test_results

REM Generate a combined report (if applicable)
echo Generating report...
reportgenerator -reports:test_results\*.xml -targetdir:test_results\report -reporttypes:Html

echo Tests completed. Check the test_results directory for coverage reports.
pause
