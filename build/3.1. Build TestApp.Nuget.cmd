@echo off

set projectName=TestApp.Nuget
set srcFolder=..\src
set publishFolder=..\publish

for %%f in (
  net472
  net8.0
) do (
  setlocal EnableDelayedExpansion
  set outputFolder=%publishFolder%\%projectName%-%%f
  
  dotnet publish %srcFolder%\%projectName%\%projectName%.csproj --configuration Release --framework %%f --output !outputFolder!
  
  if %ERRORLEVEL% EQU 0 (
    echo:
    echo *************
    echo Published "%projectName%" should be found in "!outputFolder!" folder.
    echo *************
    echo:
  )
)

pause