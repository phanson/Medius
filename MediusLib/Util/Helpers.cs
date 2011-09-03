using Medius.Model;
using System;

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
            return ToHtml(p.Content, p.Title, p.Author);
        }

        public static string ToHtml(string content, string title = "", string author = "")
        {
            string titleText = null;
            if (!string.IsNullOrWhiteSpace(title))
                titleText = string.Format("<h1>{0}</h1>", title);
            string authorText = null;
            if (!string.IsNullOrWhiteSpace(author))
                authorText = string.Format("<p class=\"author\">by {0}</p>", author);
            return string.Format("<!DOCTYPE html><html><head><title>{0}</title></head><body>{1}{2}{3}</body></html>", title, titleText, authorText, content);
        }
    }
}
