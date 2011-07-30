using System;
using System.Windows.Forms;
using Medius.Controllers;
using Medius.Model;
using Medius.Controllers.Actions;
using System.Drawing;

namespace Medius
{
    public partial class Main : Form
    {
        Project project;
        IProjectPersistenceController projectLoader = new ProjectPersistenceController();

        IUndoRedoController actions = new UndoRedoController();

        string activeFilename;
        bool modified, updatingUI;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            disableUI();

            // remove edit tab until it is needed.
            // unfortunately the tabcontrol cannot hide and show tabs, so we must juggle it
            tabControl.TabPages.Remove(editTab);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // confirm close if not saved
            e.Cancel = (modified && !confirmContinueOverwrite());
        }

        /// <summary>
        /// Ask the user whether to wipe out unsaved changes.
        /// </summary>
        /// <returns><c>true</c> iff changes should be overwritten.</returns>
        private bool confirmContinueOverwrite()
        {
            return (MessageBox.Show(this, "There are unsaved changes. Are you sure you want to continue?", "Confirm overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes);
        }

        #region Simple menu items

        private void open_Click(object sender, EventArgs e)
        {
            disableUI();
            if (!load())
            {
                enableUI();
                return;
            }

            clearUI();
            populateUI();
            enableUI();
            updateMenus();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void save_Click(object sender, EventArgs e)
        {
            // save to existing file if possible
            save(false);
        }

        private void saveAs_Click(object sender, EventArgs e)
        {
            // force selection of new file
            save(true);
        }

        private void help_Click(object sender, EventArgs e)
        {
            // open help file
            // TODO
        }

        private void about_Click(object sender, EventArgs e)
        {
            // display about box
            new AboutBox().ShowDialog(this);
        }

        private void undo_Click(object sender, EventArgs e)
        {
            actions.Undo();
            updateUI();
        }

        private void redo_Click(object sender, EventArgs e)
        {
            actions.Redo();
            updateUI();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            updateUI();
        }

        #endregion Simple menu items

        #region Helper functions

        /// <summary>
        /// Populates the UI with information about the current project
        /// </summary>
        private void populateUI()
        {
            populateOutline();
        }

        private void populateOutline()
        {
            // sanity check
            if ((project == null) || (project.Book == null)) return;

            TreeNode bnode, cnode, pnode;
            bnode = new TreeNode(project.Book.Title);
            bnode.Tag = project.Book;

            // TODO: not sort in place
            project.Book.Chapters.Sort(Util.Helpers.ChapterSort);
            foreach (var chapter in project.Book.Chapters)
            {
                cnode = new TreeNode(chapter.Title);
                cnode.Tag = chapter;
                chapter.Posts.Sort(Util.Helpers.PostSort);
                foreach (var post in chapter.Posts)
                {
                    pnode = new TreeNode(post.Title);
                    pnode.Tag = post;
                    cnode.Nodes.Add(pnode);
                }
                bnode.Nodes.Add(cnode);
            }

            outline.Nodes.Add(bnode);

            // expand all nodes
            bnode.Expand();
            foreach (TreeNode node in bnode.Nodes)
                node.Expand();
        }

        /// <summary>
        /// Removes all data from the UI
        /// </summary>
        private void clearUI()
        {
            clearOutline();
            browseWindow.DocumentText = string.Empty;
        }

        private void clearOutline()
        {
            outline.Nodes.Clear();
        }

        /// <summary>
        /// Loads a model state from a file.
        /// </summary>
        /// <returns><c>true</c> if project was loaded successfully.</returns>
        private bool load()
        {
            // select file
            OpenFileDialog d = new OpenFileDialog();
            d.AddExtension = true;
            d.CheckFileExists = true;
            d.CheckPathExists = true;
            d.DefaultExt = ".medius";
            d.Filter = "Medius projects (*.medius)|*.medius";
            d.Multiselect = false;
            d.SupportMultiDottedExtensions = true;
            d.Title = "Open an existing project";
            d.ValidateNames = true;

            if (d.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return false;

            // load into memory
            project = projectLoader.Load(d.FileName);

            // fix up ordering constraints, etc
            normalize(project.Book);

            // bookkeeping
            activeFilename = d.FileName;
            modified = false;

            return true;
        }

        /// <summary>
        /// Save the current model state to a file.
        /// </summary>
        /// <param name="forceSelection">Flag to force display of file selection dialog.</param>
        private void save(bool forceSelection)
        {
            // attempt to use stored filename
            if (forceSelection || (activeFilename == null))
            {
                // select file
                SaveFileDialog d = new SaveFileDialog();
                d.AddExtension = true;
                d.CheckPathExists = true;
                d.DefaultExt = ".medius";
                d.Filter = "Medius projects (*.medius)|*.medius";
                d.OverwritePrompt = false;
                d.RestoreDirectory = true;
                d.Title = "Save project";
                d.ValidateNames = true;

                if (d.ShowDialog() == DialogResult.Cancel)
                    return;

                activeFilename = d.FileName;
            }

            // save to file
            projectLoader.Save(project, activeFilename);
            modified = false;
            updateMenus();
        }

        /// <summary>
        /// Ensures that the book is in a state such that our assumptions about ordering, etc. hold true.
        /// </summary>
        /// <param name="book">The book.</param>
        private static void normalize(Book book)
        {
            book.Chapters.Sort(Util.Helpers.ChapterSort);
            for (int i = 0; i < book.Chapters.Count; i++)
            {
                Chapter c = book.Chapters[i];
                c.Ordering = i;
                c.Posts.Sort(Util.Helpers.PostSort);
                for (int j = 0; j < c.Posts.Count; j++)
                {
                    c.Posts[j].Ordering = j;
                }
            }
        }

        private void disableUI()
        {
            container.Enabled = false;
            outline.Enabled = false;
        }

        private void enableUI()
        {
            outline.Enabled = true;
            container.Enabled = true;
        }

        private void getDragDropInfo(DragEventArgs e, out TreeNode source, out TreeNode target)
        {
            Point point = outline.PointToClient(new Point(e.X, e.Y));
            source = (TreeNode)e.Data.GetData(typeof(TreeNode));
            target = outline.GetNodeAt(point);
        }

        private void updateUI()
        {
            updatingUI = true;

            updateOutline();
            updateTabs(outline.SelectedNode);
            updateMenus();

            updatingUI = false;
        }

        private void updateMenus()
        {
            updatingUI = true;

            saveToolStripMenuItem.Enabled = saveButton.Enabled = modified;

            undoToolStripMenuItem.Enabled = undoButton.Enabled = actions.CanUndo;
            redoToolStripMenuItem.Enabled = redoButton.Enabled = actions.CanRedo;

            updatingUI = false;
        }

        /// <summary>
        /// Brings the outline control up to date with the model.
        /// </summary>
        private void updateOutline()
        {
            updatingUI = true;

            // save scroll location and selected node
            outline.BeginUpdate();
            outline.SuspendLayout();
            Point scrollLocation = outline.GetScrollPosition();
            TreeNode selectedNode = outline.SelectedNode;

            // TODO: optimize this to clear and rebuild only changed elements
            clearOutline();
            populateOutline();

            // restore scroll location and selected node (if possible)
            outline.SelectedNode = FindEquivalentNode(outline.Nodes, selectedNode);
            outline.SetScrollPosition(scrollLocation);
            outline.ResumeLayout();
            outline.EndUpdate();

            updatingUI = false;
        }

        /// <summary>
        /// Searches the given tree and returns the first node with the same data as the given node.
        /// </summary>
        /// <param name="tree">Tree to search.</param>
        /// <param name="searchNode">Target node.</param>
        /// <returns>Equivalent node, or <c>null</c> if no match is found.</returns>
        private static TreeNode FindEquivalentNode(TreeNodeCollection tree, TreeNode searchNode)
        {
            if ((tree == null) || (searchNode == null))
                return null;

            // naive tree-search because we know the data is still the same...
            // and if it isn't the same, we don't care, because we're going to lose our place anyway.
            foreach (TreeNode node in tree)
            {
                if (string.Equals(node.Text, searchNode.Text) && (node.Tag == searchNode.Tag))
                {
                    return node;
                }
                else if (node.Nodes.Count > 0)
                {
                    TreeNode x = FindEquivalentNode(node.Nodes, searchNode);
                    if (x != null)
                        return x;
                }
            }
            return null;
        }

        #endregion Helper functions

        private void addfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.CheckFileExists = true;
            d.Multiselect = false;
            d.Filter = "All files (*.*)|*.*";
            if (d.ShowDialog() != DialogResult.OK)
                return;

            new AddFileAction(project, d.FileName).Do();
        }

        private void outline_AfterSelect(object sender, TreeViewEventArgs e)
        {
            updateTabs(e.Node);
        }

        private void updateTabs(TreeNode node)
        {
            updatingUI = true;

            browseWindow.DocumentText = string.Empty;
            postEditBox.Text = string.Empty;

            combineWithNextToolStripMenuItem.Enabled = false;
            deleteToolStripMenuItem.Enabled = false;

            if (node == null)
                return;

            switch (node.Level)
            {
                case 0: // book
                    Book b = node.Tag as Book;  // could get this from project as well...
                    tabControl.TabPages.Remove(editTab);
                    propertyGrid.SelectedObject = b;
                    break;
                case 1: // chapter
                    Chapter c = node.Tag as Chapter;
                    tabControl.TabPages.Remove(editTab);
                    propertyGrid.SelectedObject = c;
                    break;
                case 2: // post
                    Post p = node.Tag as Post;
                    browseWindow.DocumentText = Util.Helpers.ToHtml(p);
                    if (!tabControl.TabPages.Contains(editTab))
                        tabControl.TabPages.Add(editTab);
                    postEditBox.Text = p.Content;
                    propertyGrid.SelectedObject = p;
                    combineWithNextToolStripMenuItem.Enabled = true;
                    deleteToolStripMenuItem.Enabled = true;
                    break;
                default:
                    throw new Exception();
            }

            updatingUI = false;
        }

        #region Outline context menu

        private void addChapter_Click(object sender, EventArgs e)
        {
            AddChapterDialog d = new AddChapterDialog();
            d.Posts = project.Book.GetAllPosts();
            if (d.ShowDialog() != DialogResult.OK)
                return;

            actions.Do(new AddChapterAction(project.Book, new Chapter(d.Title, d.SelectedPosts), outline.SelectedNode.Tag as Chapter));

            updateUI();
        }

        private void addPostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPostDialog d = new AddPostDialog();
            if (d.ShowDialog() != DialogResult.OK)
                return;

            // find containing chapter
            Chapter chapter;
            if ((outline.SelectedNode == null) || (outline.SelectedNode.Level == 0))
                chapter = project.Book.Chapters[0];
            else if (outline.SelectedNode.Level == 1)
                chapter = outline.SelectedNode.Tag as Chapter;
            else
                chapter = outline.SelectedNode.Parent.Tag as Chapter;

            actions.Do(new AddPostAction(chapter, new Post(d.Title)));

            updateUI();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // restrict to posts
            TreeNode node = outline.SelectedNode;
            if ((node == null) || (node.Level != 2))
                return;  // TODO display an error or something

            Chapter chapter = node.Parent.Tag as Chapter;
            Post post = node.Tag as Post;

            actions.Do(new DeletePostAction(chapter, post));
            updateUI();
        }

        private void combineWithNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // restrict to posts with a sibling following
            TreeNode node = outline.SelectedNode;
            if ((node == null) || (node.Level != 2) || (node.NextNode == null))
                return;  // TODO display an error or something

            Chapter chapter = node.Parent.Tag as Chapter;
            Post a = node.Tag as Post;
            Post b = node.NextNode.Tag as Post;

            actions.Do(new CombinePostsAction(chapter, a, b));
            updateUI();
        }

        private void splitPostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node = outline.SelectedNode;
            if ((node == null) || (node.Level != 2))
                return;  // TODO display an error or something

            Chapter chapter = node.Parent.Tag as Chapter;
            Post post = node.Tag as Post;

            SplitPostDialog d = new SplitPostDialog(post);
            if (d.ShowDialog() != DialogResult.OK)
                return;

            actions.Do(new SplitPostAction(chapter, post, d.ExistingPostTitle, d.ExistingPostContent, d.SplitPostTitle, d.SplitPostContent));
            updateUI();
        }

        #endregion Outline context menu

        #region Drag and Drop methods

        private void outline_ItemDrag(object sender, ItemDragEventArgs e)
        {
            // disallow root node
            if ((outline.SelectedNode == null) || (outline.SelectedNode == outline.Nodes[0])) return;
            // start drag and drop operation
            outline.DoDragDrop(outline.SelectedNode, DragDropEffects.Move);
        }

        private void outline_DragOver(object sender, DragEventArgs e)
        {
            TreeNode source, target;
            getDragDropInfo(e, out source, out target);

            if ((source.Level >= target.Level) && (source.Level - target.Level < 2))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void outline_DragDrop(object sender, DragEventArgs e)
        {
            TreeNode source, target;
            getDragDropInfo(e, out source, out target);

            switch (source.Level)
            {
                case 0:
                    return;
                case 1:
                    dragDrop_Chapter(source, target);
                    break;
                case 2:
                    dragDrop_Post(source, target);
                    break;
            }
        }

        private void dragDrop_Chapter(TreeNode source, TreeNode target)
        {
            IReversibleAction action;
            switch (target.Level)
            {
                case 0:
                    action = new MoveChapterAction(source.Tag as Chapter, target.Tag as Book);
                    break;
                case 1:
                    action = new MoveChapterAction(source.Tag as Chapter, target.Parent.Tag as Book, target.Tag as Chapter);
                    break;
                default:
                    // invalid
                    return;
            }
            actions.Do(action);
            updateUI();
        }

        private void dragDrop_Post(TreeNode source, TreeNode target)
        {
            IReversibleAction action;
            switch (target.Level)
            {
                case 1:
                    action = new MovePostAction(source.Parent.Tag as Chapter, source.Tag as Post, target.Tag as Chapter);
                    break;
                case 2:
                    action = new MovePostAction(source.Parent.Tag as Chapter, source.Tag as Post, target.Parent.Tag as Chapter, target.Tag as Post);
                    break;
                default:
                    // invalid
                    return;
            }
            actions.Do(action);
            updateUI();
        }

        #endregion Drag and Drop methods

        private void wordPressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.AddExtension = true;
            d.DefaultExt = ".xml";
            d.Filter = "WordPress eXtended RSS (*.xml)|*.xml";
            d.Multiselect = false;
            d.Title = "Open WordPress export file";
            if (d.ShowDialog() != DialogResult.OK)
                return;

            if (modified && !confirmContinueOverwrite())
                return;

            disableUI();

            IImportController importer = new WordpressImportController(new XmlPersistenceController());
            actions.Clear();
            project = importer.Import(d.FileName);

            // fix any lingering order problems
            normalize(project.Book);

            // bookkeeping
            activeFilename = null;  // force "save as"
            modified = false;

            updateUI();
            enableUI();
        }

        #region Export menu

        private void htmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "HTML Files (*.htm,*.html)|*.htm;*.html";
            d.DefaultExt = "htm";
            d.OverwritePrompt = true;
            if (d.ShowDialog() == DialogResult.OK)
            {
                disableUI();
                new HtmlExportController().Export(project, d.FileName);
                enableUI();
            }
            // TODO: display status message
        }

        private void kindleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "Zipped HTML (*.zip)|*.zip";
            d.DefaultExt = "zip";
            d.OverwritePrompt = true;
            if (d.ShowDialog() == DialogResult.OK)
            {
                disableUI();
                new KindleExportController().Export(project, d.FileName);
                enableUI();
            }
            // TODO: display status message
        }

        #endregion Export menu

        #region Cleanup menu

        private void removeNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveNodesDialog d = new RemoveNodesDialog();
            if (d.ShowDialog(this) != DialogResult.OK)
                return;

            actions.Do(new RemoveNodesAction(project.Book.GetAllPosts(), d.XPath));
            updateUI();
        }

        private void findReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindReplaceDialog d = new FindReplaceDialog();
            if (d.ShowDialog(this) != DialogResult.OK)
                return;

            actions.Do(new FindReplaceAction(project.Book.GetAllPosts(), d.Pattern, d.Replacement, d.ShouldUseRegex));
            updateUI();
        }

        private void validateHtmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ValidateHtmlDialog() { Posts = project.Book.GetAllPosts() }.ShowDialog();
        }

        #endregion Cleanup menu

        #region Edit tab

        private void postEditingSaveTimer_Tick(object sender, EventArgs e)
        {
            postEditingSaveTimer.Stop();
            if (outline.SelectedNode == null)
                return;

            Post p = outline.SelectedNode.Tag as Post;
            if (p != null)
            {
                actions.Do(new EditPostAction(p, postEditBox.Text));
                updateMenus();
                browseWindow.DocumentText = Util.Helpers.ToHtml(p);
                editTab.Text = "Edit";
            }
        }

        private void postEditBox_TextChanged(object sender, EventArgs e)
        {
            if (updatingUI)
                return;

            // mark dirty
            editTab.Text = "Edit*";

            // restart timer
            postEditingSaveTimer.Stop();
            postEditingSaveTimer.Start();
        }

        #endregion Edit tab
    }
}
