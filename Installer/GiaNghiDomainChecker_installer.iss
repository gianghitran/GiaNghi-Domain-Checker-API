[Setup]
AppName=GiaNghi Domain Checker
AppVersion=1.0
DefaultDirName={pf}\GiaNghiDomainCheckerApp
DefaultGroupName=GiaNghiDomainCheckerApp
OutputDir=C:\Users\ASUS\Documents\Nam2_Ki2\ltmcb\LTMCB_LyThuyet\btap\API
OutputBaseFilename=GiaNghiDomainCheckerSetUp
Compression=lzma
SolidCompression=yes

[Files]
Source: "C:\Users\ASUS\Documents\Nam2_Ki2\ltmcb\LTMCB_LyThuyet\btap\API\23521005_APIExtracting\23521005_APIExtracting\bin\Debug\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\GiaNghi Domain Checker"; Filename: "{app}\23521005_APIExtracting.exe"
Name: "{group}\Uninstall GiaNghi Domain Checker"; Filename: "{uninstallexe}"

[Run]
Filename: "{app}\23521005_APIExtracting.exe"; Description: "Chạy GiaNghi Domain Checker"; Flags: nowait postinstall skipifsilent
