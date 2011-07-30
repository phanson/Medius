using Medius.Model;

namespace Medius.Util
{
    /// <summary>
    /// A collection of sort functions and miscellaneous static helpers.
    /// </summary>
    public static class Helpers
    {
        public static string[] TextFileExtensions = { "txt", "xml", "css", "htm", "html", "tex", "bib" };

        /// <summary>
        /// Generalized sort comparison function for ordering posts.
        /// </summary>
        /// <returns><c>0</c> if equal, <c>&lt;0</c> if <c>a</c> sorts before <c>b</c>, and <c>&gt;0</c> if <c>b</c> sorts before <c>a</c>.</returns>
        public static int PostSort(Post a, Post b)
        {
            int c = a.Ordering - b.Ordering;
            if (c != 0) return c;

            return a.Title.CompareTo(b.Title);
        }

        /// <summary>
        /// Generalized sort comparison function for ordering posts by publication date.
        /// </summary>
        /// <returns><c>0</c> if equal, <c>&lt;0</c> if <c>a</c> sorts before <c>b</c>, and <c>&gt;0</c> if <c>b</c> sorts before <c>a</c>.</returns>
        public static int PostSortByPubDate(Post a, Post b)
        {
            int c = (int)a.PublishDate.Subtract(b.PublishDate).TotalSeconds;
            if (c != 0) return c;

            return a.Title.CompareTo(b.Title);
        }

        /// <summary>
        /// Generalized sort comparison function for ordering chapters.
        /// </summary>
        /// <returns><c>0</c> if equal, <c>&lt;0</c> if <c>a</c> sorts before <c>b</c>, and <c>&gt;0</c> if <c>b</c> sorts before <c>a</c>.</returns>
        public static int ChapterSort(Chapter a, Chapter b)
        {
            int c = a.Ordering - b.Ordering;
            if (c != 0) return c;

            return a.Title.CompareTo(b.Title);
        }

        /// <summary>
        /// Returns a string containing the HTML representation of the given <see cref="Post"/>.
        /// </summary>
        /// <param name="p">The post.</param>
        public static string ToHtml(Post p)
        {
            return ToHtml(p.Content, p.Title);
        }

        public static string ToHtml(string content, string title = "")
        {
            return "<!DOCTYPE html><html><head><title>" + title + "</title></head><body>" + content + "</body></html>";
        }
    }
}
