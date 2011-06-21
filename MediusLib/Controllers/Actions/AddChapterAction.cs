using System.Collections.Generic;
using Medius.Model;

namespace Medius.Controllers.Actions
{
    public class AddChapterAction : AbstractAction
    {
        Book book;
        Chapter newChapter, precedingChapter;
        Dictionary<Post, Chapter> restoreMap = new Dictionary<Post, Chapter>();

        /// <summary>
        /// Adds <c>newChapter</c> to the given <see cref="Book"/> immediately
        /// following the <c>precedingChapter</c>.
        /// </summary>
        /// <param name="newChapter">The chapter to add.</param>
        /// <param name="precedingChapter">The chapter immediately before the
        /// insertion point, or <c>null</c> if adding to the beginning.</param>
        public AddChapterAction(Book book, Chapter newChapter, Chapter precedingChapter)
        {
            this.book = book;
            this.newChapter = newChapter;
            this.precedingChapter = precedingChapter;
        }

        protected override void InternalDo()
        {
            foreach (Post p in newChapter.Posts)
            {
                // yep, it's 'inefficient'. I know. there aren't enough chapters for it to matter.
                foreach (Chapter c in book.Chapters)
                {
                    if (c.Posts.Remove(p))
                        restoreMap.Add(p, c);
                }
            }

            int idx = 0;
            if (precedingChapter != null)
                idx = book.Chapters.IndexOf(precedingChapter) + 1;

            for (int i = 0; i < book.Chapters.Count; i++)
            {
                if (i >= idx)
                    book.Chapters[i].Ordering++;
            }

            newChapter.Ordering = idx + 1;
            book.Chapters.Insert(idx, newChapter);
        }

        protected override void InternalUndo()
        {
            book.Chapters.Remove(newChapter);

            foreach (KeyValuePair<Post, Chapter> r in restoreMap)
            {
                r.Value.Posts.Add(r.Key);
            }
            // done with the current restore map
            restoreMap.Clear();
        }
    }
}
