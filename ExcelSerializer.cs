using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace LocalizationUE4
{
    public static class ExcelSerializer
    {
        private const string serviceData = "--== !!! DO NOT TRANSLATE THE TEXT BELOW !!! == SERVICE DATA ==--";

        private static string MakeName(string Namespace, string Key)
        {
            return Namespace + ',' + Key;
        }

        private static string GetKey(string ExcelName)
        {
            string[] result = ExcelName.Split(',');
            if (result.Length != 2)
                throw new FormatException("Invalid ExcelName: " + ExcelName + "!");
            return result[1];
        }

        public static InternalFormat Import(string FileName)
        {
            InternalFormat data = null;

            // open excel document
            Excel.Application App = new Excel.Application();
            Excel.Workbooks Workbooks = App.Workbooks;
            Excel.Workbook Workbook = Workbooks.Open(FileName);
            Excel._Worksheet Worksheet = App.ActiveSheet;
            Excel.Range Range = Worksheet.UsedRange;

            // action: close excel and clear all headres
            Action CloseExcel = () =>
            {
                Marshal.ReleaseComObject(Range); Range = null;
                Marshal.ReleaseComObject(Worksheet); Worksheet = null;
                Workbook.Close(false, Type.Missing, Type.Missing);
                Marshal.ReleaseComObject(Workbook); Workbook = null;
                Workbooks.Close();
                Marshal.ReleaseComObject(Workbooks); Workbooks = null;
                App.Quit();
                Marshal.ReleaseComObject(App); App = null;
            };

            // read document data

            var Cells = Range.Value2;
            int rowCount = Cells.GetLength(0);
            int columnCount = Cells.GetLength(1);
            data = new InternalFormat();

            // read native and other cultures
            data.Cultures = new List<string>();
            for (int col = 3; col <= columnCount; col++)
            {
                if (Cells[1, col] != null)
                {
                    // third column is NativeCulture
                    if (col == 3)
                        data.NativeCulture = Cells[1, col];
                    data.Cultures.Add(Cells[1, col]);
                }
            }

            int index = 2;
            int cultureCount = data.Cultures.Count;
            List<InternalRecord> records = new List<InternalRecord>(rowCount / 2);

            // read all translation keys
            for (; Cells[index, 1].ToString() != serviceData; index++)
            {
                InternalRecord record = new InternalRecord();
                record.Key = GetKey(Cells[index, 2]);
                record.Translations = new List<InternalText>(cultureCount);
                for (int culture = 0; culture < cultureCount; culture++)
                {
                    InternalText translation = new InternalText();
                    translation.Culture = data.Cultures[culture];
                    translation.Text = Cells[index, culture + 3];
                    if (translation.Text == null)
                        translation.Text = "";
                    else // replace \n to \r\n
                        translation.Text = Regex.Replace(translation.Text, "(?<!\r)\n", "\r\n");
                    record.Translations.Add(translation);
                }
                records.Add(record);
            }

            int indexOfServiceData = index;
            data.Namespaces = new List<InternalNamespace>();
            InternalNamespace lastNS = null;

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
                    data.Namespaces.Add(lastNS);
                }

                InternalRecord record = records[index - indexOfServiceData - 1];
                if (record.Key != key)
                {
                    CloseExcel();
                    throw new FormatException("Unexpected key: " + key + "!");
                }

                record.Source = source;
                record.Path = path;
                lastNS.Children.Add(record);
            }

            CloseExcel();
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
            Worksheet.Columns[1].ColumnWidth = 10;
            Worksheet.Cells[1, "A"] = "#";
            Worksheet.Columns[2].ColumnWidth = 40;
            Worksheet.Cells[1, "B"] = "ID";
            Worksheet.Columns[3].ColumnWidth = 100;
            Worksheet.Cells[1, "C"] = data.NativeCulture;
            for (int i = 0, j = 4; i < data.Cultures.Count; i++)
            {
                if (data.Cultures[i] == data.NativeCulture)
                    continue;
                Worksheet.Columns[j].ColumnWidth = 100;
                Worksheet.Cells[1, j] = data.Cultures[i];
                j++;
            }

            int index = 2;
            foreach (var ns in data.Namespaces)
                foreach (var rec in ns.Children)
                {
                    Worksheet.Cells[index, "A"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    Worksheet.Cells[index, "A"] = (index - 1).ToString();
                    Worksheet.Cells[index, "B"] = MakeName(ns.Name, rec.Key);
                    Worksheet.Cells[index, "C"].Interior.Color = ColorTranslator.ToOle(Color.FromArgb(255, 229, 212));
                    Worksheet.Cells[index, "C"] = rec[data.NativeCulture];
                    for (int i = 0, j = 4; i < data.Cultures.Count; i++)
                    {
                        if (data.Cultures[i] == data.NativeCulture)
                            continue;
                        string translation = rec[data.Cultures[i]];
                        if (string.IsNullOrWhiteSpace(translation))
                            Worksheet.Cells[index, j].Interior.Color =
                                ColorTranslator.ToOle(Color.FromArgb(255, 199, 206));
                        else
                            Worksheet.Cells[index, j].Interior.Color = (j % 2 == 0) ?
                                ColorTranslator.ToOle(Color.FromArgb(200, 239, 212)) :
                                ColorTranslator.ToOle(Color.FromArgb(200, 235, 250));
                        Worksheet.Cells[index, j] = translation;
                        j++;
                    }
                    index++;
                }

            Worksheet.Cells[index, "A"].Font.Color = ColorTranslator.ToOle(Color.Red);
            Worksheet.Cells[index, "A"].Font.Bold = true;
            Worksheet.Cells[index, "A"] = serviceData;
            index++;

            foreach (var ns in data.Namespaces)
                foreach (var rec in ns.Children)
                {
                    Worksheet.Rows[index].Font.Color = ColorTranslator.ToOle(Color.LightGray);
                    Worksheet.Cells[index, "A"] = rec.Source;
                    Worksheet.Cells[index, "B"] = ns.Name;
                    Worksheet.Cells[index, "C"] = rec.Key;
                    Worksheet.Cells[index, "D"] = rec.Path;
                    index++;
                }

            // App.ScreenUpdating = true;

            Marshal.ReleaseComObject(Worksheet); Worksheet = null;
            Marshal.ReleaseComObject(Workbooks); Workbooks = null;
            Marshal.ReleaseComObject(App); App = null;
        }
    }
}
