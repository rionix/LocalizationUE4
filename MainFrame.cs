using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;
using System.Media;
using System.Reflection;

namespace TranslationEditor
{
    public partial class MainFrame : Form
    {
        //
        // DATA
        //

        public InternalFormat document = null;

        private string fileName = "";
        private string manifestDir = "";
        private string exportDir = "";

        private FindDialog findDlg = null;

        //
        // Constructors and destructor
        //

        public MainFrame()
        {
            InitializeComponent();
            Icon = Properties.Resources.icon_main;

            findDlg = new FindDialog();

            Application.Idle += new EventHandler(OnIdle);

            // Speedup DataGreedView

            if (!SystemInformation.TerminalServerSession)
            {
                Type dgvType = dataGrid.GetType();
                PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi.SetValue(dataGrid, true, null);
            }
        }

        //
        // File actions (Open, Save, Import and Export)
        //

        private void OnOpen(object sender, EventArgs e)
        {
            if (manifestDir != "")
                openDlg.InitialDirectory = manifestDir;

            if (openDlg.ShowDialog(this) == DialogResult.OK)
            {
                manifestDir = Path.GetDirectoryName(openDlg.FileName);
                status.Text = "Loading... Please wait.";
                document = new InternalFormat();

                fileName = openDlg.FileName;
                string DirName = Path.GetDirectoryName(fileName);
                string Title = Path.GetFileNameWithoutExtension(fileName);

                try
                {
                    string FileText = "";
                    byte[] FileData = null;

                    FileText = File.ReadAllText(fileName);
                    JsonSerializer.LoadFromManifest(document, fileName, FileText);

                    string metaname = Path.ChangeExtension(fileName, "locmeta");
                    if (File.Exists(metaname))
                    {
                        FileData = File.ReadAllBytes(metaname);
                        JsonSerializer.LoadFromLocMeta(document, FileData);
                    }
                    else
                        throw new FileNotFoundException("Can't find file: '" + Path.GetFileName(metaname) +
                            "'. Please compile translations in Unreal Engine before using this utility.", metaname);

                    var dirs = Directory.GetDirectories(DirName);
                    foreach (var subdir in dirs)
                    {
                        string culture = subdir.Replace(DirName + Path.DirectorySeparatorChar, "");
                        string name = Path.Combine(subdir, Title + ".archive");
                        FileText = File.ReadAllText(name);
                        JsonSerializer.LoadFromArchive(document, culture, FileText);
                    }
                }
                catch (Exception ex)
                {
                    document = null;
                    MessageBox.Show(this, ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                UpdateAll();
                status.Text = "All files loaded.";
            }
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (document == null)
                return;
            if (fileName == "")
                return;

            status.Text = "Saving... Please wait.";

            string DirName = Path.GetDirectoryName(fileName);
            string Title = Path.GetFileNameWithoutExtension(fileName);

            try
            {
                string FileText = JsonSerializer.SaveToManifest(document);
                File.WriteAllText(fileName, FileText, Encoding.Unicode);

                foreach (var culture in document.Cultures)
                {
                    string dname = Path.Combine(DirName, culture);
                    string fname = Path.Combine(dname, Title + ".archive");
                    FileText = JsonSerializer.SaveToArchive(document, culture);
                    Directory.CreateDirectory(dname);
                    File.WriteAllText(fname, FileText, Encoding.Unicode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            status.Text = "All files saved.";
        }

        private void OnSaveAs(object sender, EventArgs e)
        {
            if (document == null)
                return;

            if (manifestDir != "")
                saveDlg.InitialDirectory = manifestDir;

            if (saveDlg.ShowDialog(this) == DialogResult.OK)
            {
                manifestDir = Path.GetDirectoryName(saveDlg.FileName);
                fileName = saveDlg.FileName;
                OnSave(sender, e);
            }
        }

        private void OnImport(object sender, EventArgs e)
        {
            if (exportDir != "")
                importDlg.InitialDirectory = exportDir;

            if (importDlg.ShowDialog(this) == DialogResult.OK)
            {
                exportDir = Path.GetDirectoryName(importDlg.FileName);
                status.Text = "Importing... Please wait.";
                try
                {
                    document = ExcelSerializer.Import(importDlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                UpdateAll();
                status.Text = "Import finished.";
            }
        }

        private void OnExport(object sender, EventArgs e)
        {
            if (document == null)
                return;

            if (exportDir != "")
                exportDlg.InitialDirectory = exportDir;

            if (exportDlg.ShowDialog(this) == DialogResult.OK)
            {
                exportDir = Path.GetDirectoryName(exportDlg.FileName);
                status.Text = "Exporting... Please wait.";
                try
                {
                    ExcelSerializer.Export(document, exportDlg.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                status.Text = "Export finished.";
            }
        }

        //
        // Edit actions
        //

        private void OnCopy(object sender, EventArgs e)
        {
            if (translationEdit.Focused)
                translationEdit.Copy();
            else
            {
                if (dataGrid.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dataGrid.SelectedRows[0];
                    string text = row.Cells[4].Value.ToString();
                    if (escapingCharactersMenuBtn.Checked)
                        text = text.Replace("\r", "\\r").Replace("\n", "\\n").Replace("\"", "\\\"");
                    if (text.Length > 0)
                        Clipboard.SetText(text);
                    else
                        Clipboard.Clear();
                }
                else
                    SystemSounds.Beep.Play();
            }
        }

        private void OnPaste(object sender, EventArgs e)
        {
            if (translationEdit.Focused)
                translationEdit.Paste();
            else
            {
                if (dataGrid.SelectedRows.Count > 0 && Clipboard.ContainsText())
                {
                    DataGridViewRow row = dataGrid.SelectedRows[0];
                    string text = Clipboard.GetText();
                    if (escapingCharactersMenuBtn.Checked)
                        text = text.Replace("\\r", "\r").Replace("\\n", "\n").Replace("\\\"", "\"");
                    SetRowTranslation(row, text);
                    translationEdit.Text = text;
                }
                else
                    SystemSounds.Beep.Play();
            }
        }

        private void OnDuplicate(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGrid.SelectedRows[0];
                string text = row.Cells[3].Value.ToString();
                SetRowTranslation(row, text);
                translationEdit.Text = text;
            }
        }

        private void OnReplaceNewLine(object sender, EventArgs e)
        {
            escapingCharactersMenuBtn.Checked = !escapingCharactersMenuBtn.Checked;
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
            AboutDialog dlg = new AboutDialog();
            dlg.ShowDialog(this);
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
            UpdateVisibility();
        }

        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGrid.SelectedRows[0];
                InternalRecord record = (InternalRecord)row.Tag;
                if (record != null)
                {
                    namespaceEdit.Text = (row.Cells[1].Value != null) ? row.Cells[1].Value.ToString() : "";
                    keyEdit.Text = record.Key;
                    pathEdit.Text = record.Path;
                    translationEdit.Text = (row.Cells[4].Value != null) ? row.Cells[4].Value.ToString() : "";
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
                    SetRowTranslation(dataGrid.SelectedRows[0], translationEdit.Text);
                e.SuppressKeyPress = true;
            }
        }

        private void OnTranslationFocused(object sender, EventArgs e)
        {
            status.Text = "Press 'Ctrl' + 'Enter' to store changes.";
        }

        public void OnIdle(object sender, EventArgs e)
        {
            saveMenuBtn.Enabled = (document != null) && (fileName != "");
            saveToolBtn.Enabled = (document != null) && (fileName != "");
            saveAsMenuBtn.Enabled = (document != null);
            exportMenuBtn.Enabled = (document != null);
            exportToolBtn.Enabled = (document != null);
            findMenuBtn.Checked = findDlg.Visible;
            findToolBtn.Checked = findDlg.Visible;
        }

        //
        // Update methods
        //

        private void UpdateAll()
        {
            dataGrid.Rows.Clear();
            UpdateCultureCombo();
            UpdateLocaleListWithoutTranslation();
            UpdateLocaleListTranslation();
            UpdateVisibility();
            OnSelectedIndexChanged(this, null);
        }

        private void UpdateCultureCombo()
        {
            if (document == null)
            {
                cultureCombo.Items.Clear();
                nativeCulture.Text = "Native Culture";
            }
            else
            {
                cultureCombo.BeginUpdate();
                cultureCombo.Items.Clear();
                foreach (string s in document.Cultures)
                    cultureCombo.Items.Add(s);
                if (cultureCombo.Items.Count > 0)
                    cultureCombo.SelectedIndex = 0;
                cultureCombo.EndUpdate();
                nativeCulture.Text = "Native Culture: [" + document.NativeCulture + "]";
            }
        }

        private void UpdateLocaleListWithoutTranslation()
        {
            dataGrid.SuspendLayout();
            dataGrid.Rows.Clear();
            if (document != null)
            {
                int index = 0;
                foreach (var ns in document.Namespaces)
                {
                    foreach (var rec in ns.Children)
                    {
                        dataGrid.Rows.Add(new string[]
                        {
                            (index + 1).ToString(),
                            ns.Name, rec.Key,
                            rec.Source, ""
                        });
                        dataGrid.Rows[index].Tag = rec;

                        // Compare native culture and source text (must be the same)
                        if (rec.Source != rec[document.NativeCulture])
                            dataGrid.Rows[index].DefaultCellStyle.BackColor = Color.FromArgb(255, 163, 141);

                        index++;
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
            if (document != null)
            {
                string culture = cultureCombo.Text;
                foreach (DataGridViewRow item in dataGrid.Rows)
                {
                    InternalRecord record = (InternalRecord)item.Tag;
                    item.Cells[4].Value = record[culture];
                }
            }
            dataGrid.ResumeLayout();
        }

        private void UpdateVisibility()
        {
            dataGrid.SuspendLayout();
            foreach (DataGridViewRow item in dataGrid.Rows)
            {
                if (hideTranslatedMenuBtn.Checked)
                    item.Visible = (item.Cells[4].Value.ToString() == "");
                else
                    item.Visible = true;
            }
            dataGrid.ResumeLayout();
        }

        private void OnSortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 0)
            {
                int cell1AsInt = 0, cell2AsInt = 0;
                int.TryParse(e.CellValue1.ToString(), out cell1AsInt);
                int.TryParse(e.CellValue2.ToString(), out cell2AsInt);
                e.SortResult = cell1AsInt - cell2AsInt;
                e.Handled = true;
            }
            else if (e.Column.Index == 2)
            {
                var grid = sender as DataGridView;
                string namespace1 = grid.Rows[e.RowIndex1].Cells[1].Value.ToString();
                string namespace2 = grid.Rows[e.RowIndex2].Cells[1].Value.ToString();
                e.SortResult = string.Compare(namespace1, namespace2);
                if (e.SortResult == 0)
                    e.SortResult = string.Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
                e.Handled = true;
            }
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

        private bool IsMatch(int index, string pattern, bool matchCase)
        {
            RegexOptions options = matchCase ? RegexOptions.None : RegexOptions.IgnoreCase;
            if (index >= 0 && index < dataGrid.Rows.Count)
            {
                var row = dataGrid.Rows[index];
                if (!row.Visible)
                    return false;
                string source = row.Cells[3].Value.ToString();
                string translation = row.Cells[4].Value.ToString();
                bool source_match = Regex.IsMatch(source, pattern, options);
                bool translation_match = Regex.IsMatch(translation, pattern, options);
                return source_match || translation_match;
            }
            return false;
        }

        public void FindNext(string text, bool directionDown, bool wholeWords, bool matchCase)
        {
            int count = dataGrid.Rows.Count;

            if (count < 1 || text.Trim() == "")
                return;

            int findIndex = 0;
            int stopIndex = dataGrid.RowCount - 1;
            if (dataGrid.SelectedRows.Count > 0)
            {
                stopIndex = findIndex = dataGrid.SelectedRows[0].Index;
                findIndex = LoopIndex(findIndex, count, directionDown);
            }

            string pattern = Regex.Escape(text);
            if (wholeWords)
                pattern = @"\b" + pattern + @"\b";

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
                SystemSounds.Beep.Play();
        }

        //
        // Utilites
        //

        public void SetRowTranslation(DataGridViewRow row, string text)
        {
            InternalRecord record = (InternalRecord)row.Tag;
            if (record != null)
            {
                string culture = cultureCombo.SelectedItem.ToString();
                row.Cells[4].Value = text;
                record[culture] = text;
            }
        }

        private void OnHideTranslated(object sender, EventArgs e)
        {
            hideTranslatedMenuBtn.Checked = !hideTranslatedMenuBtn.Checked;
            hideTranslatedToolBtn.Checked = hideTranslatedMenuBtn.Checked;
            UpdateVisibility();
        }
    }
}
