using Medius.Model;

namespace Medius.Controllers
{
    public interface IOperateOnEachController
    {
        /// <summary>
        /// Performs a controller-specific action on every post in the given <see cref="Book"/>.
        /// </summary>
        /// <param name="book">The book on which to operate.</param>
        void Run(Book book);
    }
}
