@ECHO OFF
echo Making installers for both .NET and no .NET
call "%programfiles(x86)%\NSIS\makensis.exe" /DINCLUDE_DOTNET=0 "Blindspot Installer.nsi" >nul
call "%programfiles(x86)%\NSIS\makensis.exe" /DINCLUDE_DOTNET=1 "Blindspot Installer.nsi" >nul
echo Finished making installers
echo.

echo gzipping up no .NET installer and naming to blindspot_dev.exe.gz
call 7z a -tgzip "blindspot_dev.exe.gz" "Blindspot Installer no DotNet.exe" >nul
pause