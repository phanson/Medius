using System;

namespace Medius.Controllers.Actions
{
    /// <summary>
    /// Skeleton implementation of <see cref="IReversibleAction"/> that protects against improper use of the action.
    /// </summary>
    public abstract class AbstractAction : IReversibleAction
    {
        protected bool Applied { get; set; }

        /// <summary>
        /// When overridden in a derived class, performs the action.
        /// </summary>
        protected abstract void InternalDo();

        /// <summary>
        /// When overridden in a derived class, reverses the action.
        /// </summary>
        protected abstract void InternalUndo();

        #region IReversibleAction Implementation

        public void Do()
        {
            InternalDo();
            Applied = true;
        }

        public void Undo()
        {
            // guard against calling Undo() before calling Do()
            if (!Applied)
                throw new InvalidOperationException("Attempted to undo an operation that has not yet been performed.");
            InternalUndo();
        }

        #endregion IReversibleAction Implementation
    }
}
