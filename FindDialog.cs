using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocalizationUE4
{
    public partial class FindDialog : Form
    {
        public FindDialog()
        {
            InitializeComponent();
        }

        private void OnFindNext(object sender, EventArgs e)
        {
            MainFrame mainFrame = Owner as MainFrame;
            if (mainFrame != null)
                mainFrame.FindNext(findText.Text, down.Checked, wholeWords.Checked, matchCase.Checked);
        }

        private void OnCancel(object sender, EventArgs e)
        {
            Hide();
        }

        private void OnClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
