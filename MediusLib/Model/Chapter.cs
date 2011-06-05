using System.Collections.Generic;

namespace Medius.Model
{
    /// <summary>
    /// Describes a single chapter of a book.
    /// </summary>
    public class Chapter
    {
        /// <summary>
        /// The chapter title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// An optional introductory paragraph or two, to be displayed
        /// at the beginning of the chapter.
        /// </summary>
        public string Introduction { get; set; }

        /// <summary>
        /// List of all posts in this chapter.
        /// </summary>
        public List<Post> Posts { get; set; }

        /// <summary>
        /// The chapter ordering index.
        /// </summary>
        public int Ordering { get; set; }
    }
}
