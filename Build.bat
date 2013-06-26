@echo off
cls
".nuget\nuget.exe" "install" "FAKE" "-OutputDirectory" "build" "-ExcludeVersion"
"build\FAKE\tools\Fake.exe" build.fsx
pause