using System;
using System.Windows.Forms;
using Medius.Model;

namespace Medius
{
    /// <summary>
    /// UI for splitting a post into exactly two new posts consisting of disjoint content.
    /// </summary>
    public partial class SplitPostDialog : Form
    {
        /// <summary>
        /// Revised title for the existing (part 1) post.
        /// </summary>
        public string ExistingPostTitle
        {
            get
            {
                return existingTitleText.Text;
            }
        }

        /// <summary>
        /// Revised content for the existing (part 1) post.
        /// </summary>
        public string ExistingPostContent
        {
            get
            {
                return splitPointText.Text.Substring(0, splitPointText.SelectionStart);
            }
        }

        /// <summary>
        /// Title for the split (part 2) post.
        /// </summary>
        public string SplitPostTitle
        {
            get
            {
                return splitTitleText.Text;
            }
        }

        /// <summary>
        /// Content for the split (part 2) post.
        /// </summary>
        public string SplitPostContent
        {
            get
            {
                return splitPointText.Text.Substring(splitPointText.SelectionStart);
            }
        }

        public SplitPostDialog()
        {
            InitializeComponent();
        }

        public SplitPostDialog(Post post) : this()
        {
            splitPointText.Text = post.Content;
            int halfLength = splitPointText.Text.Length / 2;
            splitPointText.SelectionStart = halfLength;
            splitPointText.Select(halfLength, halfLength + 1);

            existingTitleText.Text = post.Title + " (Part 1)";
            splitTitleText.Text = post.Title + " (Part 2)";
        }

        private void updatePreview(object sender, EventArgs e)
        {
            existingContentBrowser.DocumentText = Util.Helpers.ToHtml(ExistingPostContent);
            splitContentBrowser.DocumentText = Util.Helpers.ToHtml(SplitPostContent);
        }

        private void splitPointText_KeyDown(object sender, KeyEventArgs e)
        {
            updatePreview(sender, (EventArgs)e);
            
            // prevent buffer modification
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }
}
