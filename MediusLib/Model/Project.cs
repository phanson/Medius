using System.Collections.Generic;

namespace Medius.Model
{
    /// <summary>
    /// Represents a complete Medius project, including auxiliary files.
    /// </summary>
    public class Project
    {
        /// <summary>
        /// The book itself. A project can contain only one book.
        /// </summary>
        public Book Book { get; set; }

        /// <summary>
        /// Set of files that are part of the project, but not part of the book content.
        /// </summary>
        public List<ISupportFile> Files { get; set; }
    }
}
