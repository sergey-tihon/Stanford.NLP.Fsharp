@echo off
cls
"..\.nuget\nuget.exe" "install" "FAKE" "-Pre" "-OutputDirectory" "..\build" "-ExcludeVersion"
pause