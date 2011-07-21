using Medius.Model;

namespace Medius.Controllers.Actions
{
    /// <summary>
    /// Action to combine two posts into a single post by concatenation.
    /// </summary>
    public class CombinePostsAction : AbstractAction
    {
        protected Chapter chapter;
        protected string before;
        protected Post a, b;
        protected DeletePostAction deleteAction;

        public CombinePostsAction(Chapter chapter, Post a, Post b)
        {
            this.chapter = chapter;
            this.a = a;
            this.b = b;

            this.deleteAction = new DeletePostAction(chapter, b);
        }

        protected override void InternalDo()
        {
            deleteAction.Do();
            before = a.Content;
            a.Content += b.Content;
        }

        protected override void InternalUndo()
        {
            a.Content = before;
            deleteAction.Undo();
        }
    }
}
