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
        bool modified;

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            disableUI();
            
            // temporary -- remove edit tab until it is implemented
            tabControl.TabPages.Remove(editTab);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // confirm close if not saved
            e.Cancel = (modified && (MessageBox.Show(this, "There are unsaved changes. Are you sure you want to close?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No));
        }

        private void import_Click(object sender, EventArgs e)
        {
            // select import format
            // TODO

            // run import
            // TODO
        }

        private void export_Click(object sender, EventArgs e)
        {
            // select export format
            // TODO

            // run export
            // TODO
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
            updateOutline();
            updateEditMenu();
        }

        private void updateEditMenu()
        {
            undoToolStripMenuItem.Enabled = undoButton.Enabled = actions.CanUndo;
            redoToolStripMenuItem.Enabled = redoButton.Enabled = actions.CanRedo;
        }

        private void updateOutline()
        {
            // TODO: optimize this to rebuild only changed elements
            clearOutline();
            populateOutline();
        }

        #endregion Helper functions

        private void outline_AfterSelect(object sender, TreeViewEventArgs e)
        {
            switch (e.Node.Level)
            {
                case 0: // book
                    Book b = e.Node.Tag as Book;  // could get this from project as well...
                    browseWindow.DocumentText = string.Empty;
                    propertyGrid.SelectedObject = b;
                    break;
                case 1: // chapter
                    Chapter c = e.Node.Tag as Chapter;
                    browseWindow.DocumentText = string.Empty;
                    propertyGrid.SelectedObject = c;
                    break;
                case 2: // post
                    Post p = e.Node.Tag as Post;
                    browseWindow.DocumentText = "<!DOCTYPE html><html><head><title>" + p.Title + "</title></head><body>" + p.Content + "</body></html>";
                    propertyGrid.SelectedObject = p;
                    break;
                default:
                    throw new Exception();
            }
        }

        private void addChapter_Click(object sender, EventArgs e)
        {
            AddChapterDialog d = new AddChapterDialog();
            d.Posts = project.Book.GetAllPosts();
            if (d.ShowDialog() != DialogResult.OK)
                return;

            actions.Do(new AddChapterAction(project.Book, new Chapter(d.Title, d.SelectedPosts), outline.SelectedNode.Tag as Chapter));

            updateUI();
        }

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
    }
}
