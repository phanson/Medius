using System.Collections.Generic;

namespace Medius.Controllers.Actions
{
    /// <summary>
    /// Skeleton implementation of <see cref="IReversibleAction"/> for a set of entities that protects against improper use of the action.
    /// </summary>
    public abstract class AbstractOperateOnEachAction<T> : AbstractAction
    {
        private IEnumerable<T> items;
        
        /// <summary>
        /// List of posts on which this action will operate.
        /// </summary>
        public IEnumerable<T> Items
        {
            get
            {
                return items;
            }
        }

        public AbstractOperateOnEachAction(IEnumerable<T> items)
        {
            this.items = items;
        }

        /// <summary>
        /// When overridden in a derived class, performs the action on a single item.
        /// </summary>
        protected abstract void InternalDoForEach(T item);

        /// <summary>
        /// When overridden in a derived class, reverses the action on a single item.
        /// </summary>
        protected abstract void InternalUndoForEach(T item);

        protected override void InternalDo()
        {
            foreach(var item in Items)
                InternalDoForEach(item);
        }

        protected override void InternalUndo()
        {
            foreach (var item in Items)
                InternalUndoForEach(item);
        }
    }
}
