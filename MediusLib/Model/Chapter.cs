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
        public Chapter()
        {
            // required for XML serialization
        }

        public Chapter(string title = "", List<Post> posts = null)
        {
            this.Title = title;
            this.Posts = new List<Post>();
            if (posts != null)
                this.Posts.AddRange(posts);
        }

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

        public override bool Equals(object obj)
        {
            Chapter that = obj as Chapter;
            return (that != null)
                && string.Equals(this.Title, that.Title)
                && string.Equals(this.Introduction, that.Introduction);
            // consciously ignoring the impermanent Exclude and Ordering fields
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
    }
}
