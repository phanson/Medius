using Medius.Model;

namespace Medius.Controllers.Actions
{
    public class SplitPostAction : AbstractAction
    {
        protected EditPostAction edit;
        protected AddPostAction add;

        public SplitPostAction(Chapter chapter, Post post, string existingTitle, string existingContent, string splitTitle, string splitContent) : base()
        {
            Post splitPost = new Post(splitTitle, post.Author, splitContent);

            edit = new EditPostAction(post, existingContent, existingTitle);
            add = new AddPostAction(chapter, splitPost);
        }

        protected override void InternalDo()
        {
            add.Do();
            edit.Do();
        }

        protected override void InternalUndo()
        {
            edit.Undo();
            add.Undo();
        }
    }
}
