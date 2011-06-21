using Medius.Model;

namespace Medius.Controllers.Actions
{
    public class AddChapterAction : AbstractAction
    {
        Book book;
        Chapter newChapter, precedingChapter;

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
            int idx = 0;
            if (precedingChapter != null)
                idx = book.Chapters.IndexOf(precedingChapter) + 1;

            newChapter.Ordering = idx + 1;
            book.Chapters.Insert(idx, newChapter);
        }

        protected override void InternalUndo()
        {
            book.Chapters.Remove(newChapter);
        }
    }
}
