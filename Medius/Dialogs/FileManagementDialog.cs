using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Medius.Model;
using Medius.Controllers;
using Medius.Controllers.Actions;

namespace Medius
{
    public partial class FileManagementDialog : Form
    {
        private Project project;
        private IUndoRedoController actions;

        public FileManagementDialog()
        {
            InitializeComponent();
        }

        public FileManagementDialog(Project project) : this()
        {
            this.project = project;
            this.actions = new UndoRedoController();
        }

        private void FileManagementDialog_Load(object sender, EventArgs e)
        {
            updateUI();
        }

        private void updateUI()
        {
            updateToolbar();
            updateFileList();
        }

        private void updateToolbar()
        {
            undoButton.Enabled = actions.CanUndo;
            redoButton.Enabled = actions.CanRedo;

            removeFileButton.Enabled = (fileList.SelectedItems.Count > 0);
        }

        private void updateFileList()
        {
            fileList.SuspendLayout();
            fileList.BeginUpdate();

            fileList.Items.Clear();

            ListViewItem item;
            foreach (ISupportFile file in project.Files)
            {
                item = new ListViewItem(new string[] { file.Filename, file.FileType.ToString() });
                item.Tag = file;
                fileList.Items.Add(item);
            }

            fileList.EndUpdate();
            fileList.ResumeLayout();
        }

        private void undoButton_Click(object sender, EventArgs e)
        {
            actions.Undo();
            updateUI();
        }

        private void redoButton_Click(object sender, EventArgs e)
        {
            actions.Redo();
            updateUI();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            updateUI();
        }

        private void addFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.CheckFileExists = true;
            d.Multiselect = false;
            d.Filter = "All files (*.*)|*.*";
            if (d.ShowDialog() != DialogResult.OK)
                return;

            actions.Do(new AddFileAction(project, d.FileName));
            updateUI();
        }

        private void removeFileButton_Click(object sender, EventArgs e)
        {
            if (fileList.SelectedItems.Count == 0)
                return;

            var filesToRemove = new List<ISupportFile>();
            foreach (ListViewItem item in fileList.SelectedItems)
                filesToRemove.Add((ISupportFile)item.Tag);

            actions.Do(new RemoveFilesAction(project, filesToRemove));
            updateUI();
        }

        private void fileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateToolbar();
        }
    }
}
