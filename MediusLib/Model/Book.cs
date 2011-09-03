using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Medius.Model
{
    /// <summary>
    /// Describes a book as a collection of blog posts organized into chapters.
    /// </summary>
    [XmlRoot(ElementName = "book", Namespace = "http://philiphanson.org/medius/book/1.0")]
    public class Book
    {
        /// <summary>
        /// The book title.
        /// </summary>
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// The book author(s).
        /// </summary>
        [XmlAttribute(AttributeName = "author")]
        public string Author { get; set; }

        /// <summary>
        /// The book editor, if applicable.
        /// </summary>
        /// <value><c>null</c> if no editor.</value>
        [XmlAttribute(AttributeName = "editor")]
        public string Editor { get; set; }

        /// <summary>
        /// Year of publication.
        /// </summary>
        [XmlAttribute(AttributeName = "year")]
        public int Year { get; set; }

        /// <summary>
        /// Short description of the book publisher, if applicable.
        /// </summary>
        /// <value><c>null</c> if no publisher or self-published.</value>
        [XmlAttribute(AttributeName = "publisher")]
        public string Publisher { get; set; }

        /// <summary>
        /// Copyright declaration, if applicable.
        /// </summary>
        /// <value><c>null</c> if no copyright restrictions.</value>
        [XmlAttribute(AttributeName = "copyright")]
        public string Copyright { get; set; }

        /// <summary>
        /// List of chapters.
        /// </summary>
        [XmlArray(ElementName = "chapters")]
        [XmlArrayItem(ElementName = "chapter", Type = typeof(Chapter))]
        [Browsable(false)]
        public List<Chapter> Chapters { get; set; }

        /// <summary>
        /// Convenience function for iterating over all posts.
        /// </summary>
        /// <returns>Flattened list of all posts contained in this book.</returns>
        public List<Post> GetAllPosts()
        {
            List<Post> allPosts = new List<Post>();
            foreach (Chapter c in Chapters)
            {
                allPosts.AddRange(c.Posts);
            }
            return allPosts;
        }

        /// <summary>
        /// Convenience function for iterating over all authors.
        /// </summary>
        /// <returns>Flattened list of all authors contributing to this book.</returns>
        public List<string> GetAllAuthors()
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            foreach (Post p in GetAllPosts())
            {
                if (!d.ContainsKey(p.Author) && !string.IsNullOrWhiteSpace(p.Author))
                    d.Add(p.Author, null);
            }
            return new List<string>(d.Keys);
        }
    }
}
