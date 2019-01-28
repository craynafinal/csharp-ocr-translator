# csharp_ocr_translator
## Condition
- It is using windows desktop application project because it has to read windows desktop screen.
- Solution platform should not be "Any CPU" (used x64)
- Install UWP and Windows 10 SDK related components from Visual Studio Installer
- To use UWP libraries in windows desktop application project, manually added the following references:
 - C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Runtime.WindowsRuntime.dll
 - C:\Windows\Microsoft.NET\Framework\v4.0.30319\System.Runtime.WindowsRuntime.UI.Xaml.dll
 - C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.17763.0\Windows.winmd
