# Translation Editor for UE4

![Screenshot](ReadmeImages/Screenshot.png "Screenshot")

## Compilation time and runtime dependencies

- Microsoft Visual Studio 2019
- C# and .Net 4.0 or higher
- Newtonsoft.Json 12.0 *(via NuGet)*
- EPPlus 4.5 *(via NuGet)*

## Features
- Multiline editing *(yes, UE4 still not support this)*
- Export and Import from Excel files
- Sort by any column

## Using

How to change localizations in the program:

- **Gather** and **Compile** translations in Unreal Engine 4
- Open `*.manifest` file from `[YOUR_PROJECT]\Content\Localization\Game`
- All `*.archive` files will be loaded automaticaly
- Modify localization as you want. Press **[Ctrl]+[Enter]** to apply modifications to the selected translation.
- Save to `*.manifest` file. All `*.archive` files will be saved automaticaly.
- **Gather** and **Compile** translations in Unreal Engine 4 again.
- Enjoy :)

This program can export to Microsoft Excel as a single worksheet document. How to import and export localization with this program:

- Open `*.manifest` file.
- Press `File -> Export...` and program will create Excel file. 
- You will get a document similar to this:
    ![Excel](ReadmeImages/Excel.png "Excel Document")
- Untranslated cells will be red. This will allow the translator to find them quickly.
- You can modify the document as you want down to line: **--== !!! DO NOT TRANSLATE THE TEXT BELOW !!! == SERVICE DATA ==--**
- You can sort rows as you like, but before importing, you need to sort them by the first column.
- When you are done with translation, start the program and select `File -> Import`.
- Select `File -> Save As...` and save to your game `*.manifest` file.
- Don't forget to **Gather** and **Compile** translations in Unreal Engine 4 again.

## Discussion

- [Unreal Engine Forums](https://forums.unrealengine.com/community/community-content-tools-and-tutorials/1497851-opensource-translation-editor-for-ue4)
