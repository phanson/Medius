using System;
using System.Collections.Generic;
using Medius.Controllers.Actions;

namespace Medius.Controllers
{
    public class UndoRedoController : Medius.Controllers.IUndoRedoController
    {
        protected Stack<IReversibleAction> done = new Stack<IReversibleAction>();
        protected Stack<IReversibleAction> undone = new Stack<IReversibleAction>();

        public void Do(IReversibleAction action)
        {
            action.Do();
            done.Push(action);
            undone.Clear();
        }

        public bool CanUndo
        {
            get { return (done.Count > 0); }
        }

        public IReversibleAction Undo()
        {
            // sanity check
            if (!CanUndo)
                throw new InvalidOperationException("Nothing to undo.");

            IReversibleAction action = done.Pop();
            action.Undo();
            undone.Push(action);

            return action;
        }

        public bool CanRedo
        {
            get { return (undone.Count > 0); }
        }

        public IReversibleAction Redo()
        {
            // sanity check
            if (!CanRedo)
                throw new InvalidOperationException("Nothing to undo.");

            IReversibleAction action = undone.Pop();
            action.Do();
            done.Push(action);

            return action;
        }

        public void Clear()
        {
            done.Clear();
            undone.Clear();
        }
    }
}
