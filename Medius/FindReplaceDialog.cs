using System.Windows.Forms;

namespace Medius
{
    public partial class FindReplaceDialog : Form
    {
        /// <summary>
        /// Gets the string or regex pattern to match.
        /// </summary>
        public string Pattern
        {
            get { return findText.Text; }
        }

        /// <summary>
        /// Gets the string or regex substitution to make.
        /// </summary>
        public string Replacement
        {
            get { return replaceText.Text; }
        }

        /// <summary>
        /// Gets flag for whether to use regex.
        /// </summary>
        public bool ShouldUseRegex
        {
            get { return regexCheckBox.Checked; }
        }

        public FindReplaceDialog()
        {
            InitializeComponent();
        }
    }
}
