using System.Collections.Generic;

namespace Medius.Model
{
    /// <summary>
    /// Describes a book as a collection of blog posts organized into chapters.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// The book title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The book author(s).
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The book editor, if applicable.
        /// </summary>
        /// <value><c>null</c> if no editor.</value>
        public string Editor { get; set; }

        /// <summary>
        /// Year of publication.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Short description of the book publisher, if applicable.
        /// </summary>
        /// <value><c>null</c> if no publisher or self-published.</value>
        public string Publisher { get; set; }

        /// <summary>
        /// Copyright declaration, if applicable.
        /// </summary>
        /// <value><c>null</c> if no copyright restrictions.</value>
        public string Copyright { get; set; }

        /// <summary>
        /// List of chapters.
        /// </summary>
        public List<Chapter> Chapters { get; set; }
    }
}
