;NSIS script for installing Blindspot
;Comes with .NET installing and extraction of files

;What version are we on?
!define VERSION "0.8"

;Use Modern looking installer
!include "MUI2.nsh"

;Settings
Name "Blindspot"
OutFile "Blindspot Installer.exe"

;Install to program files directory
InstallDir "$PROGRAMFILES\Blindspot"

; Get installation folder from registry if available
; Will override InstallDir if a previous install was installed elsewhere
InstallDirRegKey HKCU "Software\Blindspot" ""

; Request application privileges for Windows Vista and later
; Since we may well be installing .NET framework, we need admin privilages
RequestExecutionLevel admin

; Warn before exiting installer
!define MUI_ABORTWARNING

;Pages

!define MUI_WELCOMEPAGE_TEXT \
"Welcome to the installation wizard for Blindspot version ${VERSION}. Please click next to proceed with the setup. $\r$\n$\r$\n Note: Blindspot requires installation of the .NET Framework 4, which will be installed as part of this setup if you do not currently have this installed."
!insertmacro MUI_PAGE_WELCOME
!define MUI_LICENSEPAGE_TEXT_TOP \
"I promise this is only a short license agreement! Please read it and accept to continue. "
!define MUI_LICENSEPAGE_TEXT_BOTTOM \
"If you agree to this license, click the agree button"
!insertmacro MUI_PAGE_LICENSE "license.txt"
!insertmacro MUI_PAGE_DIRECTORY
Var StartMenuFolder
!define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU"
!define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\Blindspot"
!define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"
!define MUI_STARTMENUPAGE_DEFAULTFOLDER Blindspot
!insertmacro MUI_PAGE_STARTMENU "Application" $StartMenuFolder
!insertmacro MUI_PAGE_INSTFILES
!define MUI_FINISHPAGE_TEXT_REBOOT \
"Since .NET Framework has been installed, a system reboot is needed to launch the application"
!define MUI_FINISHPAGE_REBOOTLATER_DEFAULT
!define MUI_FINISHPAGE_RUN $INSTDIR\Blindspot.exe
!define MUI_FINISHPAGE_SHOWREADME http://blindspot.codeplex.com/wikipage?title=Getting%20Started&referringTitle=Documentation
!define MUI_FINISHPAGE_SHOWREADME_TEXT "View getting started guide"
!define MUI_FINISHPAGE_SHOWREADME_NOTCHECKED
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

!insertmacro MUI_LANGUAGE "English"

;Installer Sections

Section ""

SetOutPath "$INSTDIR"

File "Blindspot.exe"
File "Blindspot.exe.config"
File "*.txt"
File "*.dll"

; Getting the files in \Lib
SetOutPath "$INSTDIR\Lib"
File "Lib\*.*"

; Getting the hotkeys file
SetOutPath "$INSTDIR\Settings"
File "Settings\hotkeys.txt"

SetOutPath "$INSTDIR"

;Store installation folder
WriteRegStr HKCU "Software\Blindspot" "" $INSTDIR

;Create uninstaller
WriteUninstaller "$INSTDIR\Uninstall Blindspot.exe"

!insertmacro MUI_STARTMENU_WRITE_BEGIN Application
; Create start menu shortcuts
CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Blindspot.lnk" "$INSTDIR\Blindspot.exe"
; Shortcut for the getting started guide
CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Getting started guide.lnk" "http://blindspot.codeplex.com/wikipage?title=Getting%20Started&referringTitle=Documentation"
CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk" "$INSTDIR\Uninstall Blindspot.exe"
!insertmacro MUI_STARTMENU_WRITE_END
SectionEnd

Section "MS .NET Framework"
ClearErrors
ReadRegDWORD $0 HKLM "Software\Microsoft\Net Framework Setup\NDP\v4\Full" "Install"
IfErrors dotNet40FullNotFound
IntCmp $0 1 dotNet40Found
;if no .NET full version, maybe there's a client version
dotNet40FullNotFound:
ClearErrors
ReadRegDWORD $0 HKLM "Software\Microsoft\Net Framework Setup\NDP\v4\Client" "Install"
IfErrors dotNet40NotFound
IntCmp $0 1 dotNet40Found
dotNet40NotFound: 
MessageBox MB_YESNO "The .NET framework version 4 is not currently installed. Do you want to install it? This requires 600MB free space on your system." IDNO dotNet40Found
Banner::show /set 76 "Installing .NET Framework 4.0" "Please wait"  
SetOutPath $TEMP
File /nonfatal "tools\dotNetFx40_Client_x86_x64.exe"
ExecWait "$TEMP\dotNetFx40_Client_x86_x64.exe /passive /showfinalerror /norestart"
Delete /REBOOTOK "$TEMP\dotNetFx40_Client_x86_x64.exe"
Banner::destroy
; .NET requires a restart
SetRebootFlag true
dotNet40Found:
SectionEnd

Section "Uninstall"
;Delete all files in the install directory
Delete "$INSTDIR\*.*"
Delete "$INSTDIR\Lib\*.*"
Delete "$INSTDIR\Settings\*.*"
; Not sure if deleting sub folders is necessary first but here for completeness
RMDir "$INSTDIR\Lib"
RMDir "$INSTDIR\Settings"
RMDir "$INSTDIR"

;Delete start menu stuff
!insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder
Delete "$SMPROGRAMS\$StartMenuFolder\*.lnk"
RMDir "$SMPROGRAMS\$StartMenuFolder"

;Delete the registry entry
DeleteRegKey /ifempty HKCU "Software\Blindspot"

; We're leaving the .NET framework installed if it was for now
; people might not want .NET framework mysteriously leaving their machines
SectionEnd