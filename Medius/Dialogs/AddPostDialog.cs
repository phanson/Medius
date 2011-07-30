using System.Windows.Forms;

namespace Medius
{
    public partial class AddPostDialog : Form
    {
        public string Title
        {
            get
            {
                return titleText.Text;
            }
        }

        public AddPostDialog()
        {
            InitializeComponent();
        }
    }
}
