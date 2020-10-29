namespace TranslationEditor
{
    partial class MainFrame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.importMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicateMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.escapingCharactersMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.hideTranslatedMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.statusbar = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.rowCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.nativeCulture = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolbar = new System.Windows.Forms.ToolStrip();
            this.openToolBtn = new System.Windows.Forms.ToolStripButton();
            this.saveToolBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolBtn = new System.Windows.Forms.ToolStripButton();
            this.pasteToolBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolBtn = new System.Windows.Forms.ToolStripButton();
            this.exportToolBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cultureCombo = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.hideTranslatedToolBtn = new System.Windows.Forms.ToolStripButton();
            this.findToolBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolBtn = new System.Windows.Forms.ToolStripButton();
            this.openDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveDlg = new System.Windows.Forms.SaveFileDialog();
            this.importDlg = new System.Windows.Forms.OpenFileDialog();
            this.namespaceEdit = new System.Windows.Forms.TextBox();
            this.keyEdit = new System.Windows.Forms.TextBox();
            this.pathEdit = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.translationEdit = new System.Windows.Forms.TextBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNamespace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTranslation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exportDlg = new System.Windows.Forms.SaveFileDialog();
            this.menu.SuspendLayout();
            this.statusbar.SuspendLayout();
            this.toolbar.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMenuBtn,
            this.saveMenuBtn,
            this.saveAsMenuBtn,
            this.toolStripMenuItem2,
            this.importMenuBtn,
            this.exportMenuBtn,
            this.toolStripMenuItem1,
            this.exitMenuBtn});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openMenuBtn
            // 
            this.openMenuBtn.Image = global::TranslationEditor.Properties.Resources.icons8_open;
            this.openMenuBtn.Name = "openMenuBtn";
            this.openMenuBtn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openMenuBtn.Size = new System.Drawing.Size(194, 22);
            this.openMenuBtn.Text = "Open...";
            this.openMenuBtn.Click += new System.EventHandler(this.OnOpen);
            // 
            // saveMenuBtn
            // 
            this.saveMenuBtn.Image = global::TranslationEditor.Properties.Resources.icons8_save;
            this.saveMenuBtn.Name = "saveMenuBtn";
            this.saveMenuBtn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenuBtn.Size = new System.Drawing.Size(194, 22);
            this.saveMenuBtn.Text = "Save";
            this.saveMenuBtn.Click += new System.EventHandler(this.OnSave);
            // 
            // saveAsMenuBtn
            // 
            this.saveAsMenuBtn.Image = global::TranslationEditor.Properties.Resources.icons8_save_as;
            this.saveAsMenuBtn.Name = "saveAsMenuBtn";
            this.saveAsMenuBtn.Size = new System.Drawing.Size(194, 22);
            this.saveAsMenuBtn.Text = "Save As...";
            this.saveAsMenuBtn.Click += new System.EventHandler(this.OnSaveAs);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(191, 6);
            // 
            // importMenuBtn
            // 
            this.importMenuBtn.Image = global::TranslationEditor.Properties.Resources.icons8_import_csv;
            this.importMenuBtn.Name = "importMenuBtn";
            this.importMenuBtn.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.importMenuBtn.Size = new System.Drawing.Size(194, 22);
            this.importMenuBtn.Text = "Import...";
            this.importMenuBtn.Click += new System.EventHandler(this.OnImport);
            // 
            // exportMenuBtn
            // 
            this.exportMenuBtn.Image = global::TranslationEditor.Properties.Resources.icons8_export_csv;
            this.exportMenuBtn.Name = "exportMenuBtn";
            this.exportMenuBtn.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.exportMenuBtn.Size = new System.Drawing.Size(194, 22);
            this.exportMenuBtn.Text = "Export...";
            this.exportMenuBtn.Click += new System.EventHandler(this.OnExport);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(191, 6);
            // 
            // exitMenuBtn
            // 
            this.exitMenuBtn.Name = "exitMenuBtn";
            this.exitMenuBtn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitMenuBtn.Size = new System.Drawing.Size(194, 22);
            this.exitMenuBtn.Text = "Exit";
            this.exitMenuBtn.Click += new System.EventHandler(this.OnExit);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyMenuBtn,
            this.pasteMenuBtn,
            this.duplicateMenuBtn,
            this.toolStripMenuItem3,
            this.escapingCharactersMenuBtn});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // copyMenuBtn
            // 
            this.copyMenuBtn.Image = global::TranslationEditor.Properties.Resources.icons8_copy;
            this.copyMenuBtn.Name = "copyMenuBtn";
            this.copyMenuBtn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyMenuBtn.Size = new System.Drawing.Size(180, 22);
            this.copyMenuBtn.Text = "Copy";
            this.copyMenuBtn.Click += new System.EventHandler(this.OnCopy);
            // 
            // pasteMenuBtn
            // 
            this.pasteMenuBtn.Image = global::TranslationEditor.Properties.Resources.icons8_paste;
            this.pasteMenuBtn.Name = "pasteMenuBtn";
            this.pasteMenuBtn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteMenuBtn.Size = new System.Drawing.Size(180, 22);
            this.pasteMenuBtn.Text = "Paste";
            this.pasteMenuBtn.Click += new System.EventHandler(this.OnPaste);
            // 
            // duplicateMenuBtn
            // 
            this.duplicateMenuBtn.Name = "duplicateMenuBtn";
            this.duplicateMenuBtn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.duplicateMenuBtn.Size = new System.Drawing.Size(180, 22);
            this.duplicateMenuBtn.Text = "Duplicate";
            this.duplicateMenuBtn.Click += new System.EventHandler(this.OnDuplicate);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // escapingCharactersMenuBtn
            // 
            this.escapingCharactersMenuBtn.Name = "escapingCharactersMenuBtn";
            this.escapingCharactersMenuBtn.Size = new System.Drawing.Size(180, 22);
            this.escapingCharactersMenuBtn.Text = "Escaping Characters";
            this.escapingCharactersMenuBtn.ToolTipText = "Removes escaping characters on copy/paste actions.";
            this.escapingCharactersMenuBtn.Click += new System.EventHandler(this.OnReplaceNewLine);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.findMenuBtn,
            this.hideTranslatedMenuBtn});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // findMenuBtn
            // 
            this.findMenuBtn.Image = global::TranslationEditor.Properties.Resources.icons8_search;
            this.findMenuBtn.Name = "findMenuBtn";
            this.findMenuBtn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findMenuBtn.Size = new System.Drawing.Size(155, 22);
            this.findMenuBtn.Text = "Find...";
            this.findMenuBtn.Click += new System.EventHandler(this.OnShowFind);
            // 
            // hideTranslatedMenuBtn
            // 
            this.hideTranslatedMenuBtn.Image = global::TranslationEditor.Properties.Resources.icons8_show;
            this.hideTranslatedMenuBtn.Name = "hideTranslatedMenuBtn";
            this.hideTranslatedMenuBtn.Size = new System.Drawing.Size(155, 22);
            this.hideTranslatedMenuBtn.Text = "Hide Translated";
            this.hideTranslatedMenuBtn.Click += new System.EventHandler(this.OnHideTranslated);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuBtn});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutMenuBtn
            // 
            this.aboutMenuBtn.Image = global::TranslationEditor.Properties.Resources.icons8_about;
            this.aboutMenuBtn.Name = "aboutMenuBtn";
            this.aboutMenuBtn.Size = new System.Drawing.Size(107, 22);
            this.aboutMenuBtn.Text = "About";
            this.aboutMenuBtn.Click += new System.EventHandler(this.OnAbout);
            // 
            // statusbar
            // 
            this.statusbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.rowCount,
            this.nativeCulture});
            this.statusbar.Location = new System.Drawing.Point(0, 428);
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(800, 22);
            this.statusbar.TabIndex = 1;
            this.statusbar.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(659, 17);
            this.status.Spring = true;
            this.status.Text = "Ready";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rowCount
            // 
            this.rowCount.Name = "rowCount";
            this.rowCount.Size = new System.Drawing.Size(35, 17);
            this.rowCount.Text = "Rows";
            // 
            // nativeCulture
            // 
            this.nativeCulture.Margin = new System.Windows.Forms.Padding(8, 3, 0, 2);
            this.nativeCulture.Name = "nativeCulture";
            this.nativeCulture.Size = new System.Drawing.Size(83, 17);
            this.nativeCulture.Text = "Native Culture";
            // 
            // toolbar
            // 
            this.toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolBtn,
            this.saveToolBtn,
            this.toolStripSeparator5,
            this.copyToolBtn,
            this.pasteToolBtn,
            this.toolStripSeparator1,
            this.importToolBtn,
            this.exportToolBtn,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.cultureCombo,
            this.toolStripSeparator4,
            this.hideTranslatedToolBtn,
            this.findToolBtn,
            this.toolStripSeparator3,
            this.aboutToolBtn});
            this.toolbar.Location = new System.Drawing.Point(0, 24);
            this.toolbar.Name = "toolbar";
            this.toolbar.Size = new System.Drawing.Size(800, 25);
            this.toolbar.TabIndex = 2;
            this.toolbar.Text = "toolStrip1";
            // 
            // openToolBtn
            // 
            this.openToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolBtn.Image = global::TranslationEditor.Properties.Resources.icons8_open;
            this.openToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolBtn.Name = "openToolBtn";
            this.openToolBtn.Size = new System.Drawing.Size(23, 22);
            this.openToolBtn.Text = "Open";
            this.openToolBtn.Click += new System.EventHandler(this.OnOpen);
            // 
            // saveToolBtn
            // 
            this.saveToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolBtn.Image = global::TranslationEditor.Properties.Resources.icons8_save;
            this.saveToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolBtn.Name = "saveToolBtn";
            this.saveToolBtn.Size = new System.Drawing.Size(23, 22);
            this.saveToolBtn.Text = "Save";
            this.saveToolBtn.Click += new System.EventHandler(this.OnSave);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // copyToolBtn
            // 
            this.copyToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyToolBtn.Image = global::TranslationEditor.Properties.Resources.icons8_copy;
            this.copyToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolBtn.Name = "copyToolBtn";
            this.copyToolBtn.Size = new System.Drawing.Size(23, 22);
            this.copyToolBtn.Text = "Copy to Clipboard";
            this.copyToolBtn.Click += new System.EventHandler(this.OnCopy);
            // 
            // pasteToolBtn
            // 
            this.pasteToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pasteToolBtn.Image = global::TranslationEditor.Properties.Resources.icons8_paste;
            this.pasteToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolBtn.Name = "pasteToolBtn";
            this.pasteToolBtn.Size = new System.Drawing.Size(23, 22);
            this.pasteToolBtn.Text = "Paste from Clipboard";
            this.pasteToolBtn.Click += new System.EventHandler(this.OnPaste);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // importToolBtn
            // 
            this.importToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.importToolBtn.Image = global::TranslationEditor.Properties.Resources.icons8_import_csv;
            this.importToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importToolBtn.Name = "importToolBtn";
            this.importToolBtn.Size = new System.Drawing.Size(23, 22);
            this.importToolBtn.Text = "Import";
            this.importToolBtn.Click += new System.EventHandler(this.OnImport);
            // 
            // exportToolBtn
            // 
            this.exportToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exportToolBtn.Image = global::TranslationEditor.Properties.Resources.icons8_export_csv;
            this.exportToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportToolBtn.Name = "exportToolBtn";
            this.exportToolBtn.Size = new System.Drawing.Size(23, 22);
            this.exportToolBtn.Text = "Export";
            this.exportToolBtn.Click += new System.EventHandler(this.OnExport);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(49, 22);
            this.toolStripLabel1.Text = "Culture:";
            // 
            // cultureCombo
            // 
            this.cultureCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cultureCombo.Name = "cultureCombo";
            this.cultureCombo.Size = new System.Drawing.Size(96, 25);
            this.cultureCombo.SelectedIndexChanged += new System.EventHandler(this.OnCultureChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // hideTranslatedToolBtn
            // 
            this.hideTranslatedToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.hideTranslatedToolBtn.Image = global::TranslationEditor.Properties.Resources.icons8_show;
            this.hideTranslatedToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.hideTranslatedToolBtn.Name = "hideTranslatedToolBtn";
            this.hideTranslatedToolBtn.Size = new System.Drawing.Size(23, 22);
            this.hideTranslatedToolBtn.Text = "Hide Translated";
            this.hideTranslatedToolBtn.ToolTipText = "Hide Translated";
            this.hideTranslatedToolBtn.Click += new System.EventHandler(this.OnHideTranslated);
            // 
            // findToolBtn
            // 
            this.findToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.findToolBtn.Image = global::TranslationEditor.Properties.Resources.icons8_search;
            this.findToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.findToolBtn.Name = "findToolBtn";
            this.findToolBtn.Size = new System.Drawing.Size(23, 22);
            this.findToolBtn.Text = "Find Text";
            this.findToolBtn.Click += new System.EventHandler(this.OnShowFind);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // aboutToolBtn
            // 
            this.aboutToolBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.aboutToolBtn.Image = global::TranslationEditor.Properties.Resources.icons8_about;
            this.aboutToolBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.aboutToolBtn.Name = "aboutToolBtn";
            this.aboutToolBtn.Size = new System.Drawing.Size(23, 22);
            this.aboutToolBtn.Text = "About";
            this.aboutToolBtn.Click += new System.EventHandler(this.OnAbout);
            // 
            // openDlg
            // 
            this.openDlg.DefaultExt = "manifest";
            this.openDlg.Filter = "Localization Manifest|*.manifest|All Files|*.*";
            // 
            // saveDlg
            // 
            this.saveDlg.DefaultExt = "manifest";
            this.saveDlg.Filter = "Localization Manifest|*.manifest|All Files|*.*";
            this.saveDlg.OverwritePrompt = false;
            // 
            // importDlg
            // 
            this.importDlg.DefaultExt = "xlsx";
            this.importDlg.Filter = "Excel Files|*.xlsx|All Files|*.*";
            this.importDlg.Title = "Import";
            // 
            // namespaceEdit
            // 
            this.namespaceEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.namespaceEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.namespaceEdit.Location = new System.Drawing.Point(3, 3);
            this.namespaceEdit.Name = "namespaceEdit";
            this.namespaceEdit.ReadOnly = true;
            this.namespaceEdit.Size = new System.Drawing.Size(394, 23);
            this.namespaceEdit.TabIndex = 0;
            this.namespaceEdit.TabStop = false;
            // 
            // keyEdit
            // 
            this.keyEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keyEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.keyEdit.Location = new System.Drawing.Point(403, 3);
            this.keyEdit.Name = "keyEdit";
            this.keyEdit.ReadOnly = true;
            this.keyEdit.Size = new System.Drawing.Size(394, 23);
            this.keyEdit.TabIndex = 1;
            this.keyEdit.TabStop = false;
            // 
            // pathEdit
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pathEdit, 2);
            this.pathEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pathEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pathEdit.Location = new System.Drawing.Point(3, 32);
            this.pathEdit.Name = "pathEdit";
            this.pathEdit.ReadOnly = true;
            this.pathEdit.Size = new System.Drawing.Size(794, 23);
            this.pathEdit.TabIndex = 2;
            this.pathEdit.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.pathEdit, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.keyEdit, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.namespaceEdit, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.translationEdit, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 246);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 182);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // translationEdit
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.translationEdit, 2);
            this.translationEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.translationEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.translationEdit.Location = new System.Drawing.Point(3, 61);
            this.translationEdit.MaxLength = 65535;
            this.translationEdit.Multiline = true;
            this.translationEdit.Name = "translationEdit";
            this.translationEdit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.translationEdit.Size = new System.Drawing.Size(794, 118);
            this.translationEdit.TabIndex = 3;
            this.translationEdit.Enter += new System.EventHandler(this.OnTranslationFocused);
            this.translationEdit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnTranslationKeyDown);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToResizeRows = false;
            this.dataGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dataGrid.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.dataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Unicode MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGrid.ColumnHeadersHeight = 24;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNumber,
            this.colNamespace,
            this.colKey,
            this.colSource,
            this.colTranslation});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Unicode MS", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.EnableHeadersVisualStyles = false;
            this.dataGrid.GridColor = System.Drawing.Color.SteelBlue;
            this.dataGrid.Location = new System.Drawing.Point(0, 49);
            this.dataGrid.MultiSelect = false;
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            this.dataGrid.RowHeadersVisible = false;
            this.dataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid.ShowEditingIcon = false;
            this.dataGrid.Size = new System.Drawing.Size(800, 197);
            this.dataGrid.TabIndex = 0;
            this.dataGrid.SelectionChanged += new System.EventHandler(this.OnSelectedIndexChanged);
            this.dataGrid.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.OnSortCompare);
            // 
            // colNumber
            // 
            this.colNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.SteelBlue;
            this.colNumber.DefaultCellStyle = dataGridViewCellStyle2;
            this.colNumber.HeaderText = "#";
            this.colNumber.Name = "colNumber";
            this.colNumber.ReadOnly = true;
            this.colNumber.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colNumber.Width = 48;
            // 
            // colNamespace
            // 
            this.colNamespace.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colNamespace.HeaderText = "Namespace";
            this.colNamespace.Name = "colNamespace";
            this.colNamespace.ReadOnly = true;
            this.colNamespace.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNamespace.Width = 128;
            // 
            // colKey
            // 
            this.colKey.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colKey.HeaderText = "Key";
            this.colKey.Name = "colKey";
            this.colKey.ReadOnly = true;
            this.colKey.Width = 192;
            // 
            // colSource
            // 
            this.colSource.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colSource.HeaderText = "Source";
            this.colSource.Name = "colSource";
            this.colSource.ReadOnly = true;
            this.colSource.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // colTranslation
            // 
            this.colTranslation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTranslation.HeaderText = "Translation";
            this.colTranslation.Name = "colTranslation";
            this.colTranslation.ReadOnly = true;
            this.colTranslation.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // exportDlg
            // 
            this.exportDlg.DefaultExt = "xlsx";
            this.exportDlg.Filter = "Excel Files|*.xlsx|All Files|*.*";
            this.exportDlg.OverwritePrompt = false;
            this.exportDlg.Title = "Export";
            // 
            // MainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.statusbar);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(320, 240);
            this.Name = "MainFrame";
            this.Text = "Translation Editor for UE4";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.statusbar.ResumeLayout(false);
            this.statusbar.PerformLayout();
            this.toolbar.ResumeLayout(false);
            this.toolbar.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMenuBtn;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuBtn;
        private System.Windows.Forms.StatusStrip statusbar;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ToolStrip toolbar;
        private System.Windows.Forms.ToolStripMenuItem openMenuBtn;
        private System.Windows.Forms.ToolStripMenuItem saveMenuBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem importMenuBtn;
        private System.Windows.Forms.ToolStripMenuItem exportMenuBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton openToolBtn;
        private System.Windows.Forms.ToolStripButton saveToolBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton importToolBtn;
        private System.Windows.Forms.ToolStripButton exportToolBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton aboutToolBtn;
        private System.Windows.Forms.OpenFileDialog openDlg;
        private System.Windows.Forms.SaveFileDialog saveDlg;
        private System.Windows.Forms.OpenFileDialog importDlg;
        private System.Windows.Forms.TextBox pathEdit;
        private System.Windows.Forms.TextBox keyEdit;
        private System.Windows.Forms.TextBox namespaceEdit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripStatusLabel rowCount;
        private System.Windows.Forms.TextBox translationEdit;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem findMenuBtn;
        private System.Windows.Forms.ToolStripComboBox cultureCombo;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.ToolStripStatusLabel nativeCulture;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton findToolBtn;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyMenuBtn;
        private System.Windows.Forms.ToolStripMenuItem pasteMenuBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem escapingCharactersMenuBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton copyToolBtn;
        private System.Windows.Forms.ToolStripButton pasteToolBtn;
        private System.Windows.Forms.ToolStripMenuItem duplicateMenuBtn;
        private System.Windows.Forms.ToolStripMenuItem saveAsMenuBtn;
        private System.Windows.Forms.ToolStripMenuItem hideTranslatedMenuBtn;
        private System.Windows.Forms.ToolStripButton hideTranslatedToolBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNamespace;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTranslation;
        private System.Windows.Forms.SaveFileDialog exportDlg;
    }
}

