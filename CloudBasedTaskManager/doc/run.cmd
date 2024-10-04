@echo off
echo Starting Blazor WebAssembly App...
start /B dotnet run --project BlazorWASM\BlazorWASM.csproj

echo Starting Web API...
start /B dotnet run --project WebAPI\WebAPI.csproj

echo Starting Razor Class Library (if applicable, otherwise skip this)
:: If your Razor Class Library is used as a project reference, it runs when you run the Blazor project
:: If not, you can use it as follows, if it's a separate project:
:: start /B dotnet run --project path\to\YourRazorClassLibraryProject\YourRazorClassLibraryProject.csproj

echo All projects should now be running.
pause
