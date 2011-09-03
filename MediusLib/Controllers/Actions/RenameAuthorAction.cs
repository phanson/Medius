using System;
using System.Collections.Generic;
using Medius.Model;

namespace Medius.Controllers.Actions
{
    /// <summary>
    /// Changes the name of a given 
    /// </summary>
    public class RenameAuthorAction : AbstractAction
    {
        private List<Post> items, changedPosts = new List<Post>();
        private string oldName, newName;

        public RenameAuthorAction(IEnumerable<Post> items, string oldName, string newName) : base()
        {
            this.items = new List<Post>(items);

            this.oldName = oldName;
            this.newName = newName;
        }

        protected override void InternalDo()
        {
            changedPosts.Clear();

            try
            {
                foreach (var item in items)
                {
                    if (string.Equals(item.Author, oldName))
                    {
                        item.Author = newName;
                        changedPosts.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                // rollback
                InternalUndo();
                throw;
            }
        }

        protected override void InternalUndo()
        {
            foreach (var item in changedPosts)
            {
                item.Author = oldName;
            }
            changedPosts.Clear();
        }
    }
}
