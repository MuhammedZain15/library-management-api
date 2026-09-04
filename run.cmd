@echo off
setlocal
cd /d "%~dp0"

where dotnet >nul 2>nul
if errorlevel 1 (
  echo .NET 10 SDK is not installed or is not available in PATH.
  echo Download it from https://dotnet.microsoft.com/download/dotnet/10.0
  pause
  exit /b 1
)

echo Starting Library Management API...
echo Swagger will open at http://localhost:5181/swagger/index.html
dotnet restore "LibraryApp\LibraryApp.csproj"
if errorlevel 1 goto :failed
start "" /b powershell.exe -NoProfile -WindowStyle Hidden -Command "Start-Sleep -Seconds 3; Start-Process 'http://localhost:5181/swagger/index.html'"
dotnet run --project "LibraryApp\LibraryApp.csproj" --launch-profile http --no-restore
exit /b %errorlevel%

:failed
echo.
echo The project could not be restored. Check the error above.
pause
exit /b 1
