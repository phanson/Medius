using System.Windows.Forms;
using System.Collections.Generic;

namespace Medius
{
    public partial class RenameAuthorDialog : Form
    {
        public RenameAuthorDialog()
        {
            InitializeComponent();
        }

        public RenameAuthorDialog(List<string> authors) : this()
        {
            oldNameComboBox.Items.AddRange(authors.ToArray());
            if (oldNameComboBox.Items.Count > 0)
                oldNameComboBox.SelectedIndex = 0;
        }

        public string OldName
        {
            get { return oldNameComboBox.SelectedItem.ToString(); }
        }

        public string NewName
        {
            get { return newNameText.Text; }
        }
    }
}
