using System;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;

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
        [XmlIgnore]
        [Browsable(false)]
        public string Content { get; set; }

        [XmlText]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        public XmlNode[] CDataContent
        {
            // shamelessly lifted from http://stackoverflow.com/questions/1379888/how-do-you-serialize-a-string-as-cdata-using-xmlserializer/1379936#1379936
            get
            {
                var dummy = new XmlDocument();
                return new XmlNode[] { dummy.CreateCDataSection(Content) };
            }
            set
            {
                if (value == null)
                {
                    Content = null;
                    return;
                }

                if (value.Length != 1)
                {
                    throw new InvalidOperationException(
                        String.Format(
                            "Invalid array length {0}", value.Length));
                }

                Content = value[0].Value;
            }
        }

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

        public override bool Equals(object obj)
        {
            Post that = obj as Post;
            return (that != null)
                && string.Equals(this.Title, that.Title)
                && string.Equals(this.Author, that.Author)
                && string.Equals(this.Content, that.Content)
                && DateTime.Equals(this.PublishDate, that.PublishDate);
            // consciously ignoring the impermanent Exclude and Ordering fields
        }

        public override int GetHashCode()
        {
            return Title.GetHashCode() + Content.GetHashCode();
        }
    }
}
