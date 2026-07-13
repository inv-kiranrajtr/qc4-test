@echo off
set msbuild.exe= %1
set ouputdir = %3
set path=%path%;C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE
set msinfo.exe = %5

ECHO START
IF EXIST .\build.log DEL .\build.log
IF EXIST %~3\Quick-CROSS4_64.msi DEL %~3\Quick-CROSS4_64.msi
IF EXIST %~3\Quick-CROSS4_86.msi DEL %~3\Quick-CROSS4_86.msi

%msbuild.exe% %2 -t:REBUILD -p:Configuration=Release -fl -flp:logfile=build.log;verbosity=diagnostic
FIND "0 Error(s)" .\build.log > 0 && set "err=1" || set "err=0"

IF %err%==0 GOTO PROBLEM 
IF %err%==1 GOTO BUILDOK 

:BUILDOK
ECHO BUILD SUCCESS 

DEL "%~3\*.pdb"
cd /D %~3
echo %~3\Codesigning.bat %~3\QuickCross4.exe
@call "%~3\Codesigning.bat" "%~3\QuickCross4.exe"
@call "%~3\Codesigning.bat" "%~3\QC4ExcelAddIn.dll"
ECHO SUCCESSFULLY ADDED Digital Signature to QuickCross4.exe and QC4ExcelAddIn.dll
pause
cd ..\
ECHO BUILDING x64 MSI
devenv %2 /rebuild "Release" /project Quick-CROSS4_x64 /projectconfig "Release" 
IF EXIST %~3\Quick-CROSS4_64.msi GOTO X64OK
ELSE GOTO PROBLEM
:X64OK
ECHO BUILDING x86 MSI
devenv %2 /rebuild "Release" /project Quick-CROSS4_x86 /projectconfig "Release" 
IF EXIST %~3\Quick-CROSS4_86.msi GOTO INSTALLEROK
:INSTALLEROK
cd /D %~3
ECHO ADDING MSINFO
%5 "%~3\Quick-CROSS4_64.msi" -w 10
%5 "%~3\Quick-CROSS4_86.msi" -w 10
ECHO ADDING Digital Signature to x64 MSI
@call "%~3\Codesigning.bat" "%~3\Quick-CROSS4_64.msi"
pause
ECHO ADDING Digital Signature to x64 MSI
@call "%~3\Codesigning.bat" "%~3\Quick-CROSS4_86.msi"
pause
IF EXIST %~4\Quick-CROSS4_64.msi DEL %~4\Quick-CROSS4_64.msi
IF EXIST %~4\Quick-CROSS4_86.msi DEL %~4\Quick-CROSS4_86.msi
copy %~3\Quick-CROSS4_64.msi %~4\Quick-CROSS4_64.msi
copy %~3\Quick-CROSS4_86.msi %~4\Quick-CROSS4_86.msi
ECHO CREATING INSTALLER EXE
devenv %2 /rebuild "Release" /project QuickCROSS-Setup /projectconfig "Release" 
IF EXIST %~3\QuickCROSS-Setup.exe GOTO EXEREADY
ELSE GOTO PROBLEM
:EXEREADY
ECHO SUCCESSFULLY CREATED INSTALLER EXE... SIGNING
cd /D %~3
@call %~3\Codesigning.bat %~3\QuickCROSS-Setup.exe
ECHO SUCCESSFULLY ADDED Digital Signature to QuickCROSS-Setup.exe
GOTO BYE
:PROBLEM
ECHO ERROR OCCURRED
:BYE 
pause

