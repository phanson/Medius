using Medius.Model;

namespace Medius.Controllers
{
    /// <summary>
    /// Provides generic structure for all OperateOnEach controllers.
    /// </summary>
    public abstract class AbstractOperateOnEachController : IOperateOnEachController
    {
        /// <summary>
        /// When overridden in a derived class, performs a class-specific action on a single blog post.
        /// </summary>
        /// <param name="post">The blog post.</param>
        protected abstract void PerformSpecificAction(Post post);

        public void Run(Book book)
        {
            foreach (Chapter chapter in book.Chapters)
            {
                foreach (Post post in chapter.Posts)
                {
                    PerformSpecificAction(post);
                }
            }
        }
    }
}
