using Medius.Model;

namespace Medius.Controllers.Actions
{
    /// <summary>
    /// Wrapper for general changes to post content.
    /// </summary>
    public class EditPostAction : AbstractAction
    {
        // it's very likely that we should be storing diffs instead.
        // for now I am optimizing for programmer time and not run time.
        protected Post post;
        protected string before, after;

        public EditPostAction(Post post, string newContent)
        {
            this.post = post;
            this.after = newContent;
        }

        protected override void InternalDo()
        {
            before = post.Content;
            post.Content = after;
        }

        protected override void InternalUndo()
        {
            post.Content = before;
        }
    }
}
