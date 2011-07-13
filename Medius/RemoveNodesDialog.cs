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
    public partial class RemoveNodesDialog : Form
    {
        public string XPath
        {
            get { return xpathText.Text; }
        }

        public RemoveNodesDialog()
        {
            InitializeComponent();
        }
    }
}
