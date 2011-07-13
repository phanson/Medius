using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Medius.Controllers.Actions;
using Medius.Model;

namespace Medius
{
    public partial class ValidateHtmlDialog : Form
    {
        public List<Post> Posts { get; set; }

        public ValidateHtmlDialog()
        {
            InitializeComponent();
        }

        private void ValidateHtmlDialog_Load(object sender, EventArgs e)
        {
            var action = new ValidatePostsAction(Posts);
            action.Do();
            action.NonValidatingPosts.ForEach(item => postList.Items.Add(String.Format("{0} ({1})", item.Title, item.Author)));
        }
    }
}
