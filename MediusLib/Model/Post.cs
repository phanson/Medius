using System;

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
        public string Title { get; set; }

        /// <summary>
        /// The post author(s).
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The publish date.
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// The post content in HTML form.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Flag to exclude this post from being exported.
        /// </summary>
        public bool Exclude { get; set; }

        /// <summary>
        /// The ordering index.
        /// </summary>
        public int Ordering { get; set; }
    }
}
