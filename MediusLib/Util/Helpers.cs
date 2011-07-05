using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medius.Model;

namespace Medius.Util
{
    /// <summary>
    /// A collection of sort functions, for now.
    /// </summary>
    public static class Helpers
    {
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
    }
}
