@echo off
cls
"..\.nuget\nuget.exe" "install" "FAKE" "-OutputDirectory" "..\build" "-ExcludeVersion"
pause