using System;
using System.Reflection;
using System.Windows.Forms;

namespace TranslationEditor
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            nameLabel.Text = AssemblyTitle;
            copyrightLabel.Text = AssemblyCopyright;
        }

        private void OnClick(object sender, EventArgs e)
        {
            Close();
        }

        private void OnRionixClick(object sender, LinkLabelLinkClickedEventArgs e)
        {
            e.Link.Visited = true;
            System.Diagnostics.Process.Start("http://www.rionix.com/");
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        #endregion
    }
}
