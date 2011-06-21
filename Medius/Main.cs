using System;
using System.Windows.Forms;
using Medius.Controllers;
using Medius.Model;
using Medius.Controllers.Actions;

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
            TreeNode bnode, cnode, pnode;
            bnode = new TreeNode(project.Book.Title);
            bnode.Tag = project.Book;

            // TODO: not sort in place
            project.Book.Chapters.Sort((a, b) => a.Ordering - b.Ordering);
            foreach (var chapter in project.Book.Chapters)
            {
                cnode = new TreeNode(chapter.Title);
                cnode.Tag = chapter;
                chapter.Posts.Sort((a, b) =>
                {
                    int c = (int)a.PublishDate.Subtract(b.PublishDate).TotalSeconds;
                    return c == 0 ? a.Ordering - b.Ordering : c;
                });
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

        private void addChapterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddChapterDialog d = new AddChapterDialog();
            if (d.ShowDialog() != DialogResult.OK)
                return;

            actions.Do(new AddChapterAction(project.Book, new Chapter(d.Title), outline.SelectedNode.Tag as Chapter));

            updateUI();
        }

        private void updateUI()
        {
            updateOutline();
            updateEditMenu();
        }

        private void updateEditMenu()
        {
            undoToolStripMenuItem.Enabled = actions.CanUndo;
            redoToolStripMenuItem.Enabled = actions.CanRedo;
        }

        private void updateOutline()
        {
            // TODO: optimize this to rebuild only changed elements
            clearOutline();
            populateOutline();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            updateUI();
        }
    }
}
