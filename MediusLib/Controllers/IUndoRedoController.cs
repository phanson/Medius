using Medius.Controllers.Actions;

namespace Medius.Controllers
{
    /// <summary>
    /// Describes a container to help manage undo/redo functionality.
    /// </summary>
    public interface IUndoRedoController
    {
        /// <summary>
        /// Execute the given action.
        /// </summary>
        /// <param name="action">The reversible action.</param>
        void Do(IReversibleAction action);

        /// <summary>
        /// Flag to indicate whether there is an action available to undo.
        /// </summary>
        bool CanUndo { get; }
        
        /// <summary>
        /// Reverse the effects of the most recent action.
        /// </summary>
        /// <returns>The action, or <c>null</c> if there is nothing to undo.</returns>
        IReversibleAction Undo();

        /// <summary>
        /// Flag to indicate whether there is an action available to undo.
        /// </summary>
        bool CanRedo { get; }

        /// <summary>
        /// Reapply the effects of the action returned by the last call to <see cref="Undo"/>.
        /// </summary>
        /// <returns>The action, or <c>null</c> if there is nothing to undo.</returns>
        IReversibleAction Redo();
    }
}
