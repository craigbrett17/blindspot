;NSIS script for installing Blindspot
;Comes with .NET installing and extraction of files

;What version are we on?
!define VERSION "1.1"

;Use Modern looking installer
!include "MUI2.nsh"

;Settings
Name "Blindspot"
OutFile "Blindspot Installer.exe"
!define APPDATADIR "$LOCALAPPDATA\Blindspot"

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

;Language Selection Dialog Settings

;Show all languages, despite user's codepage
!define MUI_LANGDLL_ALLLANGUAGES

;Remember the installer language
!define MUI_LANGDLL_REGISTRY_ROOT "HKCU"
!define MUI_LANGDLL_REGISTRY_KEY "Software\Blindspot"
!define MUI_LANGDLL_REGISTRY_VALUENAME "Installer Language"

;Pages

!define MUI_WELCOMEPAGE_TEXT \
"$(welcome_top_1) ${VERSION}. $(welcome_top_2) $\r$\n$\r$\n $(welcome_dotnet)."
!insertmacro MUI_PAGE_WELCOME
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
"$(finishpage_text_reboot)"
!define MUI_FINISHPAGE_REBOOTLATER_DEFAULT
!define MUI_FINISHPAGE_RUN $INSTDIR\Blindspot.exe
!define MUI_FINISHPAGE_SHOWREADME http://blindspot.codeplex.com/wikipage?title=Getting%20Started&referringTitle=Documentation
!define MUI_FINISHPAGE_SHOWREADME_TEXT "$(show_readme)"
!define MUI_FINISHPAGE_SHOWREADME_NOTCHECKED
!insertmacro MUI_PAGE_FINISH

!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

;Supported languages
!insertmacro MUI_LANGUAGE "English"
!insertmacro MUI_LANGUAGE "French"
!insertmacro MUI_LANGUAGE "German"
!insertmacro MUI_LANGUAGE "Spanish"
!insertmacro MUI_LANGUAGE "Swedish"

; Language strings

;English
LangString welcome_top_1 ${LANG_ENGLISH} "Welcome to the installation wizard for Blindspot version"
LangString welcome_top_2 ${LANG_ENGLISH} "Please click next to proceed with the setup"
LangString welcome_dotnet ${LANG_ENGLISH} "Note: Blindspot requires installation of the .NET Framework 4, which will be installed as part of this setup if you do not currently have this installed"
LangString finishpage_text_reboot ${LANG_ENGLISH} "Since .NET Framework has been installed, a system reboot is needed to launch the application"
LangString show_readme ${LANG_ENGLISH} "View Getting Started guide"
LangString install_dotnet_question ${LANG_ENGLISH} "The .NET framework version 4 is not currently installed. Do you want to install it? This requires 600MB free space on your system"

;French
LangString welcome_top_1 ${LANG_FRENCH} "Bienvenue dans l'assistant d'installation pour Blindspot version"
LangString welcome_top_2 ${LANG_FRENCH} "S'il vous plaît cliquer sur suivant pour procéder à la configuration"
LangString welcome_dotnet ${LANG_FRENCH} "Note: Blindspot nécessite l'installation du NET Framework 4, qui sera installé dans le cadre de cette configuration si vous n'avez pas actuellement cette installation"
LangString finishpage_text_reboot ${LANG_FRENCH} "Parce que .NET Framework a été installé, un redémarrage du système est nécessaire pour lancer l'application"
LangString show_readme ${LANG_FRENCH} "Voir le Guide de démarrage"
LangString install_dotnet_question ${LANG_FRENCH} ".NET Framework version 4 n'est pas installé. Voulez-vous installer? Cela nécessite 600 Mo d'espace libre sur votre système"

;German
LangString welcome_top_1 ${LANG_GERMAN} "Willkommen auf der Installations-Assistent für Blindspot version"
LangString welcome_top_2 ${LANG_GERMAN} "Bitte klicken sie auf weiter, um das setup gehen"
LangString welcome_dotnet ${LANG_GERMAN} "Hinweis: Blindspot erfordert die installation des .NET Framework 4, die als teil von diesem setup installiert werden, wenn sie derzeit nicht über diese installiert"
LangString finishpage_text_reboot ${LANG_GERMAN} "Weil .NET Framework installiert wurde, ist ein neustart des systems erforderlich, um die anwendung zu starten"
LangString show_readme ${LANG_GERMAN} "Sehen sie im Handbuch Erste Schritte"
LangString install_dotnet_question ${LANG_GERMAN} "Die .NET Framework Version 4 noch nicht installiert ist. Möchten Sie es installieren? Dies erfordert 600 MB freien speicherplatz auf ihrem system"

;Spanish
LangString welcome_top_1 ${LANG_SPANISH} "Bienvenido al asistente de instalación para la Blindspot versión"
LangString welcome_top_2 ${LANG_SPANISH} "Haga clic en Siguiente para continuar con la instalación"
LangString welcome_dotnet ${LANG_SPANISH} "Nota: Blindspot requiere la instalación del .NET Framework 4, que se instala como parte de esta configuración si usted no tiene actualmente esta instalado"
LangString finishpage_text_reboot ${LANG_SPANISH} "Debido .NET Framework está instalado, es necesario un reinicio del sistema para iniciar la aplicación"
LangString show_readme ${LANG_SPANISH} "Ver Guía de introducción"
LangString install_dotnet_question ${LANG_SPANISH} "El .NET Framework versión 4 no está instalado. ¿Desea instalarlo? Esto requiere 600 MB de espacio libre en el sistema"

;Swedish
LangString welcome_top_1 ${LANG_SWEDISH} "Välkommen till installationsguiden för Blindspot version"
LangString welcome_top_2 ${LANG_SWEDISH} "Klicka på Nästa för att fortsätta med installationen"
LangString welcome_dotnet ${LANG_SWEDISH} "Obs: Blindspot kräver installation av .NET Framework 4, som kommer att installeras som en del av denna inställning om du för närvarande inte har det installerat"
LangString finishpage_text_reboot ${LANG_SWEDISH} "Eftersom .NET Framework har installerats, är en omstart krävs för att starta programmet"
LangString show_readme ${LANG_SWEDISH} "Se Komma igång"
LangString install_dotnet_question ${LANG_SWEDISH} "Den .NET Framework version 4 är för närvarande inte installerat. Vill du installera det? Detta kräver 600 MB ledigt utrymme på ditt system"

;Reserve Files

; Needed so that it reserves this file in the datablock -- makes the installer start quicker basically
!insertmacro MUI_RESERVEFILE_LANGDLL

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

; Language assemblies
SetOutPath "$INSTDIR\de"
File "de\*.*"
SetOutPath "$INSTDIR\es"
File "es\*.*"
SetOutPath "$INSTDIR\fr"
File "fr\*.*"
SetOutPath "$INSTDIR\sv"
File "sv\*.*"

; Setting up local app data folders
; Don't overwrite existing hotkeys text file
SetOutPath "${APPDATADIR}\Settings"
; If we need to override keyboard layouts for new features, comment out this line
; IfFileExists "$OUTDIR\hotkeys.txt" +2 0
File /oname=hotkeys.txt "Keyboard Layouts\Standard.txt"
; Copy keyboard layouts
SetOutPath "${APPDATADIR}\Keyboard Layouts"
File "Keyboard Layouts\*.txt"

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

;Installer Functions

; make sure we show the language select dialog
Function .onInit
!insertmacro MUI_LANGDLL_DISPLAY
FunctionEnd

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
MessageBox MB_YESNO "$(install_dotnet_question)" IDNO dotNet40Found
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
Delete "$INSTDIR\de\*.*"
Delete "$INSTDIR\es\*.*"
Delete "$INSTDIR\fr\*.*"
Delete "$INSTDIR\sv\*.*"
; Not sure if deleting sub folders is necessary first but here for completeness
RMDir "$INSTDIR\Lib"
RMDir "$INSTDIR\Settings"
RMDir "$INSTDIR\de"
RMDir "$INSTDIR\es"
RMDir "$INSTDIR\fr"
RMDir "$INSTDIR\sv"
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

;Uninstaller Functions

; Get the language that was used for the installer for the uninstaller
Function un.onInit
!insertmacro MUI_UNGETLANGUAGE
FunctionEnd