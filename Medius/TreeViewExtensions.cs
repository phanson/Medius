using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Medius
{
    /// <summary>
    /// Some extensions to provide scrollbar position set/get functionality for TreeView controls.
    /// </summary>
    /// <remarks>
    /// Adapted from http://stackoverflow.com/questions/332788/maintain-scroll-position-of-treeview
    /// </remarks>
    public static class TreeViewExtensions
    {
        [DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetScrollPos(int hWnd, int nBar);

        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        private const int SB_HORZ = 0x0;
        private const int SB_VERT = 0x1;

        public static Point GetScrollPosition(this TreeView treeView)
        {
            return new Point(
                GetScrollPos((int)treeView.Handle, SB_HORZ),
                GetScrollPos((int)treeView.Handle, SB_VERT)
            );
        }

        public static void SetScrollPosition(this TreeView treeView, Point scrollPosition)
        {
            SetScrollPos((IntPtr)treeView.Handle, SB_HORZ, scrollPosition.X, true);
            SetScrollPos((IntPtr)treeView.Handle, SB_VERT, scrollPosition.Y, true);
        }

    }
}
