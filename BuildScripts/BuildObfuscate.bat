REM Obfuscate
del InstallDevGrep.exe
del ..\Obfuscated\*.* /F /S /Q
del DevGrepInstall.msi /F /S /Q
VersionIncrement.exe -sa ..\DevGrep\AssemblyInfo.cs
VersionIncrement.exe -sa ..\UpdateCheck\Properties\AssemblyInfo.cs
VersionIncrement.exe -sa ..\BLS.JSON\Properties\AssemblyInfo.cs
VersionIncrement.exe -sa ..\BLS.Search.Web\Properties\AssemblyInfo.cs
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe ..\DevGrep.sln /t:Clean,Build
"C:\Program Files\Red Gate\SmartAssembly 6\SmartAssembly.com" /build "DevGrep.saproj"
"C:\Program Files\Red Gate\SmartAssembly 6\SmartAssembly.com" /build "UpdateCheck.saproj"
"C:\Program Files\Red Gate\SmartAssembly 6\SmartAssembly.com" /build "BLS.JSON.saproj"
"C:\Program Files\Red Gate\SmartAssembly 6\SmartAssembly.com" /build "BLS.Search.Web.saproj"
copy ..\DevGrep\bin\Debug\BLS.TextEditor.dll ..\Obfuscated
copy ..\DevGrep\Images\app.ico ..\Obfuscated
copy license.rtf ..\Obfuscated
copy ..\DevGrep\bin\Debug\DevGrep.exe.config ..\Obfuscated
copy ..\DevGrep\bin\Debug\DGTe.dll ..\Obfuscated
copy ..\DevGrep.Resources\bin\Debug\DevGrepResources.dll ..\Obfuscated
copy ..\DevGrep\bin\Debug\Patterson.Windows.forms.dll ..\Obfuscated
copy ..\UpdateCheck\bin\Debug\Ionic.Zip.dll ..\Obfuscated
copy ..\DevGrep\bin\Debug\SmartSearch.dll ..\Obfuscated
copy ..\DevGrep\bin\Debug\RichTextBoxEx.dll ..\Obfuscated
mkdir ..\Obfuscated\Templates
mkdir ..\Obfuscated\Themes
copy ..\DevGrep\bin\Debug\Templates\Results.html ..\Obfuscated\Templates
copy ..\DevGrep\bin\Debug\Templates\ResultsCompiled.html ..\Obfuscated\Templates
copy ..\DevGrep\bin\Debug\Themes\SilverBlack.xml ..\Obfuscated\Themes
copy ..\DevGrep\bin\Debug\Themes\WinDefault.xml ..\Obfuscated\Themes
signtool.exe sign /f ./Certs/BrianPattersonOpenSourceDev.pfx /p 10162026Abc!!!! /d "DevGrep" /du "http://www.borderlinesoftware.com/products/products-windows/product-devgrep" /t "http://timestamp.verisign.com/scripts/timestamp.dll" ..\Obfuscated\DevGrep.exe
signtool.exe sign /f ./Certs/BrianPattersonOpenSourceDev.pfx /p 10162026Abc!!!! /d "UpdateCheck" /du "http://www.borderlinesoftware.com/products/products-windows/product-devgrep" /t "http://timestamp.verisign.com/scripts/timestamp.dll" ..\Obfuscated\UpdateCheck.exe
echo Updating the NSIS Version string.
VersionIncrement.exe -sn BuildInstaller.nsi ..\Obfuscated\DevGrep.exe
"C:\Program Files (x86)\NSIS\makensis.exe" BuildInstaller.nsi
signtool.exe sign /f ./Certs/BrianPattersonOpenSourceDev.pfx /p 10162026Abc!!!! /d "DevGrep Installer" /du "http://www.borderlinesoftware.com/products/products-windows/product-devgrep" /t "http://timestamp.verisign.com/scripts/timestamp.dll" "InstallDevGrep.exe"
echo Build WiX Installer
rem %SystemRoot%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe ..\DevGrepInstall\DevGrepInstall.sln /t:Clean,Build
rem copy ..\DevGrepInstall\DevGrepInstall\Bin\Debug\DevGrepInstall.msi .\