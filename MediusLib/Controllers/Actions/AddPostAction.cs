using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medius.Model;

namespace Medius.Controllers.Actions
{
    public class AddPostAction : AbstractAction
    {
        protected Chapter chapter;
        protected Post post;

        public AddPostAction(Chapter chapter, Post post)
        {
            this.chapter = chapter;
            this.post = post;
        }

        protected override void InternalDo()
        {
            chapter.Posts.Add(post);
        }

        protected override void InternalUndo()
        {
            chapter.Posts.Remove(post);
        }
    }
}
