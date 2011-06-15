using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;

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
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// An optional introductory paragraph or two, to be displayed
        /// at the beginning of the chapter.
        /// </summary>
        [XmlElement(ElementName = "introduction")]
        public string Introduction { get; set; }

        /// <summary>
        /// List of all posts in this chapter.
        /// </summary>
        [XmlArray(ElementName = "posts")]
        [XmlArrayItem(ElementName = "post", Type = typeof(Post))]
        [Browsable(false)]
        public List<Post> Posts { get; set; }

        /// <summary>
        /// The chapter ordering index.
        /// </summary>
        [XmlAttribute(AttributeName = "orderIndex")]
        public int Ordering { get; set; }
    }
}
