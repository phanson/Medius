using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Medius
{
    public partial class Main : Form
    {
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
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // select import format
            // TODO

            // run import
            // TODO
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // display about box
            new AboutBox().ShowDialog(this);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            // confirm close if not saved
            // TODO
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // select export format
            // TODO

            // run export
            // TODO
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // select file if not already selected
            // TODO

            // save to file
            // TODO
        }

        private void referenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // open help file
            // TODO
        }
    }
}
