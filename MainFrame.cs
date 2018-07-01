using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace LocalizationUE4
{
    public partial class MainFrame : Form
    {
        //
        // DATA
        //

        private const string serviceData = "--== !!! DO NOT TRANSLATE THE TEXT BELOW !!! == SERVICE DATA ==--";
        public FindDialog findDlg = null;
        public InternalFormat data = null;

        //
        // Constructors and destructor
        //

        public MainFrame()
        {
            InitializeComponent();

            findDlg = new FindDialog();

            Application.Idle += new EventHandler(OnIdle);
        }

        //
        // File actions (Open, Save, Import and Export)
        //

        private void OnOpen(object sender, EventArgs e)
        {
            if (openDlg.ShowDialog(this) == DialogResult.OK)
            {
                data = new InternalFormat();

                string FileName = openDlg.FileName;
                string DirName = Path.GetDirectoryName(FileName);
                string Title = Path.GetFileNameWithoutExtension(FileName);
                string FileText = "";
                byte[] FileData = null;

                try
                {
                    FileText = File.ReadAllText(FileName);
                    data.LoadFromManifest(FileName, FileText);

                    string metaname = Path.ChangeExtension(FileName, "locmeta");
                    FileData = File.ReadAllBytes(metaname);
                    data.LoadFromLocMeta(FileData);

                    var dirs = Directory.GetDirectories(DirName);
                    foreach (var subdir in dirs)
                    {
                        string culture = subdir.Replace(DirName + Path.DirectorySeparatorChar, "");
                        string name = Path.Combine(subdir, Title + ".archive");
                        FileText = File.ReadAllText(name);
                        data.LoadFromArchive(culture, FileText);
                    }
                }
                catch (Exception ex)
                {
                    data = null;
                    MessageBox.Show(this, ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                UpdateAll();
            }
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (data == null)
                return;
            if (saveDlg.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveDlg.FileName;
                string DirName = Path.GetDirectoryName(FileName);
                string Title = Path.GetFileNameWithoutExtension(FileName);
                string FileText = "";

                try
                {
                    FileText = data.SaveToManifest();
                    File.WriteAllText(FileName, FileText, Encoding.Unicode);

                    foreach (var culture in data.Cultures)
                    {
                        string dname = Path.Combine(DirName, culture);
                        string fname = Path.Combine(dname, Title + ".archive");
                        FileText = data.SaveToArchive(culture);
                        Directory.CreateDirectory(dname);
                        File.WriteAllText(fname, FileText, Encoding.Unicode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnImport(object sender, EventArgs e)
        {
            if (importDlg.ShowDialog(this) == DialogResult.OK)
            {
                // open excel document
                Excel.Application App = new Excel.Application();
                Excel.Workbooks Workbooks = App.Workbooks;
                Excel.Workbook Workbook = Workbooks.Open(importDlg.FileName, 
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing);
                Excel._Worksheet Worksheet = App.ActiveSheet;
                Excel.Range Range = Worksheet.UsedRange;

                /*
                // read document data
                var Cells = Range.Value2;
                int index = 2;
                for (; Cells[index, 1] != serviceData; index++)
                {
                    PortableObject po = new PortableObject();
                    po.msgctxt = Cells[index, 1];
                    po.msgid = Cells[index, 2];
                    po.msgstr = Cells[index, 3];
                    portableObjects.Add(po);
                }

                index++; // skip "-- DO NOT TRANSLATE! -- SERVICE DATA --"
                titleBlock = Cells[index++, 1];

                foreach (var po in portableObjects)
                {
                    po.SourceLocation = Cells[index, 2];
                    po.Restore();
                    index++;
                }
                */

                // close excel and clear all headres
                Marshal.ReleaseComObject(Range); Range = null;
                Marshal.ReleaseComObject(Worksheet); Worksheet = null;
                Workbook.Close(false, Type.Missing, Type.Missing);
                Marshal.ReleaseComObject(Workbook); Workbook = null;
                Workbooks.Close();
                Marshal.ReleaseComObject(Workbooks); Workbooks = null;
                App.Quit();
                Marshal.ReleaseComObject(App); App = null;

                // update info
                // UpdateLocaleList();
                status.Text = "Import succeded.";
                // count.Text = string.Format("Lines: {0}", portableObjects.Count);
            }
        }

        private void OnExport(object sender, EventArgs e)
        {
            if (data == null)
                return;

            var App = new Excel.Application();
            // Make the object visible.
            App.Visible = true;
            // App.ScreenUpdating = false;
            status.Text = "Exporting... Please wait.";

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
                        Worksheet.Cells[index, "A"] = MakeSafeString(rec.Source);
                        Worksheet.Cells[index, "B"] = ns.Name;
                        Worksheet.Cells[index, "C"] = key.Key;
                        Worksheet.Cells[index, "D"] = key.Path;
                        index++;
                    }

            // App.ScreenUpdating = true;
            status.Text = "Successful exporting.";

            Marshal.ReleaseComObject(Worksheet); Worksheet = null;
            Marshal.ReleaseComObject(Workbooks); Workbooks = null;
            Marshal.ReleaseComObject(App); App = null;
        }

        //
        // Other menu actions
        //

        private void OnShowFind(object sender, EventArgs e)
        {
            if (findDlg.Visible)
                findDlg.Hide();
            else
                findDlg.Show(this);
        }

        private void OnAbout(object sender, EventArgs e)
        {
            AboutDialog dlg = new AboutDialog();          dlg.ShowDialog(this);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Close();
        }

        //
        // Form events
        //

        private void OnCultureChanged(object sender, EventArgs e)
        {
            UpdateLocaleListTranslation();
            OnSelectedIndexChanged(sender, e);
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGrid.SelectedRows[0];
                InternalKey key = (InternalKey)row.Tag;
                if (key != null)
                {
                    namespaceEdit.Text = row.Cells[1].Value.ToString();
                    keyEdit.Text = key.Key;
                    pathEdit.Text = key.Path;
                    translationEdit.Text = row.Cells[4].Value.ToString();
                }
            }
            else
            {
                namespaceEdit.Text = "";
                keyEdit.Text = "";
                pathEdit.Text = "";
                translationEdit.Text = "";
            }
        }

        private void OnTranslationKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && e.Control == true)
            {
                if (dataGrid.SelectedRows.Count > 0)
                {
                    string culture = cultureCombo.SelectedItem.ToString();
                    var row = dataGrid.SelectedRows[0];
                    InternalKey key = (InternalKey)row.Tag;
                    if (key != null)
                    {
                        row.Cells[4].Value = translationEdit.Text;
                        key.SetTranslationForCulture(culture, translationEdit.Text);
                    }
                }
                e.SuppressKeyPress = true;
            }
        }

        public void OnIdle(object sender, EventArgs e)
        {
            saveMenuBtn.Enabled = (data != null);
            saveToolBtn.Enabled = (data != null);
            exportMenuBtn.Enabled = (data != null);
            exportToolBtn.Enabled = (data != null);
            findMenuBtn.Checked = findDlg.Visible;
            findToolBtn.Checked = findDlg.Visible;
        }

        //
        // Update methods
        //

        private void UpdateAll()
        {
            UpdateCultureCombo();
            UpdateLocaleListWithoutTranslation();
            UpdateLocaleListTranslation();
            OnSelectedIndexChanged(this, null);
        }

        private void UpdateCultureCombo()
        {
            if (data == null)
            {
                cultureCombo.Items.Clear();
                nativeCulture.Text = "Native Culture";
            }
            else
            {
                cultureCombo.BeginUpdate();
                cultureCombo.Items.Clear();
                foreach (string s in data.Cultures)
                    cultureCombo.Items.Add(s);
                cultureCombo.SelectedIndex = 0;
                cultureCombo.EndUpdate();
                nativeCulture.Text = "Native Culture: [" + data.NativeCulture + "]";
            }
        }

        private void UpdateLocaleListWithoutTranslation()
        {
            dataGrid.SuspendLayout();
            dataGrid.Rows.Clear();
            if (data != null)
            {
                int index = 0;
                foreach (var ns in data.Subnamespaces)
                {
                    foreach (var rec in ns.Children)
                    {
                        foreach(var key in rec.Keys)
                        {
                            dataGrid.Rows.Add(new string[]
                            {
                                (index + 1).ToString(),
                                ns.Name, key.Key,
                                "", ""
                            });
                            dataGrid.Rows[index].Tag = key;
                            index++;
                        }
                    }
                }
                rowCount.Text = "Rows: " + dataGrid.RowCount.ToString();
            }
            else
            {
                rowCount.Text = "Rows";
            }
            dataGrid.ResumeLayout();
        }

        private void UpdateLocaleListTranslation()
        {
            dataGrid.SuspendLayout();
            if (data != null)
            {
                string culture = cultureCombo.SelectedItem.ToString();
                bool native = (culture == data.NativeCulture);

                dataGrid.Columns[3].HeaderText = native ? "Source" : "Native Culture";

                foreach (DataGridViewRow item in dataGrid.Rows)
                {
                    InternalKey key = (InternalKey)item.Tag;
                    item.Cells[3].Value = native ? key.parent.Source : key.GetTranslationForCulture(data.NativeCulture);
                    item.Cells[4].Value = key.GetTranslationForCulture(culture);
                }
            }
            dataGrid.ResumeLayout();
        }

        //
        // Find methods
        //

        public int LoopIndex(int index, int count, bool down)
        {
            if (count < 1)
                return 0;
            index += down ? 1 : -1;
            if (index < 0)
                return count - 1;
            if (index < count)
                return index;
            return 0;
        }

        public void FindNext(string text, bool directionDown, bool wholeWords, bool matchCase)
        {
            int count = dataGrid.Rows.Count;

            if (count < 1 || text.Trim() == "")
                return;

            int findIndex = 0, stopIndex = 0;
            if (dataGrid.SelectedRows.Count > 0)
            {
                stopIndex = findIndex = dataGrid.SelectedRows[0].Index;
                findIndex = LoopIndex(findIndex, count, directionDown);
            }

            string pattern = wholeWords ? string.Format("\\b{0}\\b", text) : text;

            int resultIndex = -1;
            while (findIndex != stopIndex)
            {
                if (IsMatch(findIndex, pattern, matchCase))
                {
                    resultIndex = findIndex;
                    break;
                }
                else
                    findIndex = LoopIndex(findIndex, count, directionDown);
            }

            if (resultIndex >= 0 && resultIndex < count)
            {
                var row = dataGrid.Rows[resultIndex];
                row.Selected = true;
                dataGrid.CurrentCell = row.Cells[0];
            }
            else
                System.Media.SystemSounds.Beep.Play();
        }

        //
        // Utility methods
        //

        private string MakeSafeString(string source)
        {
            return source.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"");
        }

        private bool IsMatch(int index, string pattern, bool matchCase)
        {
            RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
            if (index >= 0 && index < dataGrid.Rows.Count)
            {
                var row = dataGrid.Rows[index];
                string source = row.Cells[3].Value.ToString();
                string translation = row.Cells[4].Value.ToString();
                bool source_match = Regex.IsMatch(source, pattern, options);
                bool translation_match = Regex.IsMatch(translation, pattern, options);
                return source_match || translation_match;
            }
            return false;
        }
    }
}
