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

        private List<Post> posts = new List<Post>();
        /// <summary>
        /// Sets the list of possible posts to be included in the new chapter.
        /// </summary>
        public List<Post> Posts {
            set { posts = value; }
        }

        /// <summary>
        /// Gets the list of selected posts to be included in the new chapter.
        /// </summary>
        public List<Post> SelectedPosts
        {
            get
            {
                List<Post> selected = new List<Post>();
                foreach (TreeNode node in postList.Nodes)
                    if (node.Checked)
                        selected.Add(node.Tag as Post);
                return selected;
            }
        }

        public AddChapterDialog()
        {
            InitializeComponent();
        }

        private void AddChapterDialog_Load(object sender, System.EventArgs e)
        {
            posts.Sort(Util.Helpers.PostSort);
            foreach (Post p in posts)
            {
                TreeNode n = new TreeNode(p.Title);
                n.Tag = p;
                postList.Nodes.Add(n);
            }
        }
    }
}
