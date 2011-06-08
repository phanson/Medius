using System;
using System.Xml.Serialization;

namespace Medius.Model
{
    /// <summary>
    /// Describes a single blog post.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// The post title.
        /// </summary>
        [XmlAttribute(AttributeName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// The post author(s).
        /// </summary>
        [XmlAttribute(AttributeName = "author")]
        public string Author { get; set; }

        /// <summary>
        /// The publish date.
        /// </summary>
        [XmlAttribute(AttributeName = "publishDate")]
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// The post content in HTML form.
        /// </summary>
        [XmlText]
        public string Content { get; set; }

        /// <summary>
        /// Flag to exclude this post from being exported.
        /// </summary>
        [XmlAttribute(AttributeName = "exclude")]
        public bool Exclude { get; set; }

        /// <summary>
        /// The ordering index.
        /// </summary>
        [XmlAttribute(AttributeName = "orderIndex")]
        public int Ordering { get; set; }
    }
}
