using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace LocalizationUE4
{
    public static class ExcelConvert
    {
        private const string serviceData = "--== !!! DO NOT TRANSLATE THE TEXT BELOW !!! == SERVICE DATA ==--";

        public static InternalFormat Import(string FileName)
        {
            InternalFormat data = null;

            // open excel document
            Excel.Application App = new Excel.Application();
            Excel.Workbooks Workbooks = App.Workbooks;
            Excel.Workbook Workbook = Workbooks.Open(FileName,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);
            Excel._Worksheet Worksheet = App.ActiveSheet;
            Excel.Range Range = Worksheet.UsedRange;

            // read document data

            var Cells = Range.Value2;
            int rowCount = Cells.GetLength(0);
            int columnCount = Cells.GetLength(1);
            data = new InternalFormat();

            // read native and other cultures
            data.Cultures = new List<string>();
            for (int col = 2; col < Range.Columns.Count; col++)
            {
                if (Cells[1, col] != null)
                {
                    // second column is NativeCulture
                    if (col == 2)
                        data.NativeCulture = Cells[1, col];
                    data.Cultures.Add(Cells[1, col]);
                }
            }

            int index = 2;
            int cultureCount = data.Cultures.Count;
            List<InternalKey> keys = new List<InternalKey>(rowCount / 2);

            // read all translation keys
            for (; Cells[index, 1] != serviceData; index++)
            {
                InternalKey key = new InternalKey();
                key.Key = Cells[index, 1];
                key.Translations = new List<InternalText>(cultureCount);
                for (int culture = 0; culture < cultureCount; culture++)
                {
                    InternalText translation = new InternalText();
                    translation.Culture = data.Cultures[culture];
                    translation.Text = Cells[index, culture + 2];
                    key.Translations.Add(translation);
                }
                keys.Add(key);
            }

            int indexOfServiceData = index;
            data.Subnamespaces = new List<InternalNamespace>(rowCount / 2);
            InternalNamespace lastNS = null;
            InternalRecord lastRec = null;

            index++;
            for (; index < rowCount + 1; index++)
            {
                string source = Cells[index, 1];
                string ns = Cells[index, 2];
                string key = Cells[index, 3];
                string path = Cells[index, 4];

                if (lastNS == null || lastNS.Name != ns)
                {
                    lastNS = new InternalNamespace();
                    lastNS.Name = ns;
                    lastNS.Children = new List<InternalRecord>();
                    data.Subnamespaces.Add(lastNS);
                    lastRec = null;
                }

                if (lastRec == null || lastRec.Source != source)
                {
                    lastRec = new InternalRecord();
                    lastRec.Source = source;
                    lastRec.Keys = new List<InternalKey>();
                    lastNS.Children.Add(lastRec);
                }

                InternalKey ikey = keys[index - indexOfServiceData - 1];
                if (ikey.Key != key)
                    throw new FormatException("Unexpected key: " + key + "!");

                ikey.Path = path;
                ikey.parent = lastRec;
                lastRec.Keys.Add(ikey);
            }

            // close excel and clear all headres
            Marshal.ReleaseComObject(Range); Range = null;
            Marshal.ReleaseComObject(Worksheet); Worksheet = null;
            Workbook.Close(false, Type.Missing, Type.Missing);
            Marshal.ReleaseComObject(Workbook); Workbook = null;
            Workbooks.Close();
            Marshal.ReleaseComObject(Workbooks); Workbooks = null;
            App.Quit();
            Marshal.ReleaseComObject(App); App = null;

            return data;
        }

        public static void Export(InternalFormat data)
        {
            var App = new Excel.Application();
            // Make the object visible.
            App.Visible = true;
            // App.ScreenUpdating = false;

            // Create a new, empty workbook and add it to the collection returned 
            // by property Workbooks. The new workbook becomes the active workbook.
            // Add has an optional parameter for specifying a praticular template. 
            // Because no argument is sent in this example, Add creates a new workbook. 
            var Workbooks = App.Workbooks;
            Workbooks.Add();

            // This example uses a single workSheet. 
            Excel._Worksheet Worksheet = App.ActiveSheet;

            // Caption
            Worksheet.Rows[1].Interior.Color = ColorTranslator.ToOle(Color.Orange);
            Worksheet.Rows[1].Font.Bold = true;
            Worksheet.Rows[1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            // Establish column headings in cells A1, B1 and other.
            Worksheet.Columns[1].ColumnWidth = 40;
            Worksheet.Cells[1, "A"] = "ID";
            Worksheet.Columns[2].ColumnWidth = 100;
            Worksheet.Cells[1, "B"] = data.NativeCulture;
            for (int i = 0, j = 3; i < data.Cultures.Count; i++)
            {
                if (data.Cultures[i] == data.NativeCulture)
                    continue;
                Worksheet.Columns[j].ColumnWidth = 100;
                Worksheet.Cells[1, j] = data.Cultures[i];
                j++;
            }

            int index = 2;
            foreach (var ns in data.Subnamespaces)
                foreach (var rec in ns.Children)
                    foreach (var key in rec.Keys)
                    {
                        Worksheet.Cells[index, "A"] = key.Key;
                        Worksheet.Cells[index, "B"].Interior.Color = ColorTranslator.ToOle(Color.FromArgb(255, 229, 212));
                        Worksheet.Cells[index, "B"] = key.GetTranslationForCulture(data.NativeCulture);
                        for (int i = 0, j = 3; i < data.Cultures.Count; i++)
                        {
                            if (data.Cultures[i] == data.NativeCulture)
                                continue;
                            Worksheet.Cells[index, j].Interior.Color = (j % 2 == 0) ?
                                ColorTranslator.ToOle(Color.FromArgb(200, 239, 212)) :
                                ColorTranslator.ToOle(Color.FromArgb(200, 235, 250));
                            Worksheet.Cells[index, j] = key.GetTranslationForCulture(data.Cultures[i]);
                            j++;
                        }
                        index++;
                    }

            Worksheet.Cells[index, "A"].Font.Color = ColorTranslator.ToOle(Color.Red);
            Worksheet.Cells[index, "A"].Font.Bold = true;
            Worksheet.Cells[index, "A"] = serviceData;
            index++;

            foreach (var ns in data.Subnamespaces)
                foreach (var rec in ns.Children)
                    foreach (var key in rec.Keys)
                    {
                        Worksheet.Rows[index].Font.Color = ColorTranslator.ToOle(Color.LightGray);
                        Worksheet.Cells[index, "A"] = rec.Source;
                        Worksheet.Cells[index, "B"] = ns.Name;
                        Worksheet.Cells[index, "C"] = key.Key;
                        Worksheet.Cells[index, "D"] = key.Path;
                        index++;
                    }

            // App.ScreenUpdating = true;

            Marshal.ReleaseComObject(Worksheet); Worksheet = null;
            Marshal.ReleaseComObject(Workbooks); Workbooks = null;
            Marshal.ReleaseComObject(App); App = null;
        }
    }
}
