# LocalizationUE4

Translation Editor for Unreal Engine 4.

**Compilation time and runtime dependencies:**
- Microsoft Visual Studio 2017
- C# and .Net 4.0 or higher
- Microsoft Office (Excel) with Primary Interop Assemblies 
- Newtonsoft.Json ver. 11 or higher

**Using:**
- Open *.manifest file from [YOUR_PROJECT]\Content\Localization\Game.
- All *.archive files will be loaded automaticaly.
- Modify localization as you want. **[Ctrl]+[Enter]** - apply modifications to string.
- Save to *.manifest file. All *.archive files will be saved automaticaly.
- Support export to Microsoft Excel as a single worksheet document.
- Do not modify rows order and their identifiers.
- Import all translations from *.xslx file.

**Features:**
- Multiline editing
- Search by translation text
- Sort by any column

**Known issues:**
- Looks like Foxit Reader software conflicts with Microsoft.Office.Interop
