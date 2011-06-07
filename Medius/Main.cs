using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Medius.Controllers;

namespace Medius
{
    public partial class Main : Form
    {
        IUndoRedoController actions;

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
            // TODO

            // load into memory
            // TODO

            // bookkeeping
            // TODO activeFilename = <opened filename>
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
            e.Cancel = !(modified && (MessageBox.Show(this, "There are unsaved changes. Are you sure you want to close?", "Confirm exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No));
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
                // TODO
            }

            // save to file
            // TODO
        }

        #endregion Helper functions
    }
}
