using Medius.Model;

namespace Medius.Controllers.Actions
{
    /// <summary>
    /// Action that removes a post from its containing chapter.
    /// </summary>
    public class DeletePostAction : AbstractAction
    {
        protected Chapter chapter;
        protected Post post;

        public DeletePostAction(Chapter chapter, Post post)
        {
            this.chapter = chapter;
            this.post = post;
        }

        protected override void InternalDo()
        {
            chapter.Posts.Remove(post);
        }

        protected override void InternalUndo()
        {
            chapter.Posts.Add(post);
        }
    }
}
