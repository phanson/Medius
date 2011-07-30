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
        protected string beforeContent, afterContent;
        protected string beforeTitle, afterTitle;

        public EditPostAction(Post post, string newContent, string newTitle = null) : base()
        {
            this.post = post;
            this.afterContent = newContent;
            this.afterTitle = newTitle;
        }

        protected override void InternalDo()
        {
            if (afterTitle != null)
            {
                beforeTitle = post.Title;
                post.Title = afterTitle;
            }

            beforeContent = post.Content;
            post.Content = afterContent;
        }

        protected override void InternalUndo()
        {
            if (afterTitle != null)
                post.Title = beforeTitle;

            post.Content = beforeContent;
        }
    }
}
