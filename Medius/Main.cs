using System;
using System.Windows.Forms;
using Medius.Controllers;
using Medius.Model;

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
            // TODO
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
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
                return;

            // load into memory
            project = projectLoader.Load(d.FileName);

            // bookkeeping
            activeFilename = d.FileName;
            modified = false;
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // select import format
            // TODO

            // run import
            // TODO
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // confirm close if not saved
            e.Cancel = (modified && (MessageBox.Show(this, "There are unsaved changes. Are you sure you want to close?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No));
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // select export format
            // TODO

            // run export
            // TODO
        }

        #region Simple menu items

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // save to existing file if possible
            save(false);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // force selection of new file
            save(true);
        }

        private void referenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open help file
            // TODO
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // display about box
            new AboutBox().ShowDialog(this);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actions.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            actions.Redo();
        }

        #endregion Simple menu items

        #region Helper functions

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

        #endregion Helper functions
    }
}
