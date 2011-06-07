namespace Medius.Controllers.Actions
{
    /// <summary>
    /// Generalized template for an action to be taken on a model element. Implementors should derive from <see cref="AbstractAction"/>.
    /// </summary>
    /// <remarks>
    /// Implementors should define a constructor that takes a reference to the model element on which to operate.
    /// </remarks>
    public interface IReversibleAction
    {
        /// <summary>
        /// Perform the action.
        /// </summary>
        void Do();

        /// <summary>
        /// Reverse the effects of performing the operation.
        /// </summary>
        void Undo();
    }
}
