using System.Windows.Forms;
using System.Collections.Generic;
using Medius.Model;

namespace Medius
{
    public partial class AddChapterDialog : Form
    {
        /// <summary>
        /// The new chapter's title.
        /// </summary>
        public string Title
        {
            get { return titleTextBox.Text; }
            set { titleTextBox.Text = (value == null) ? string.Empty : value; }
        }

        public List<Post> Posts
        {
            get { return null; }
            set { }
        }

        public AddChapterDialog()
        {
            InitializeComponent();
        }
    }
}
