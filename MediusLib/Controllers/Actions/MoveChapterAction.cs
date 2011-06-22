using Medius.Model;

namespace Medius.Controllers.Actions
{
    public class MoveChapterAction : AbstractAction
    {
        Book book;
        Chapter source, target;
        int srcOrder, destOrder;

        public MoveChapterAction(Chapter source, Book book, Chapter target = null)
        {
            this.book = book;
            this.source = source;
            this.target = target;
        }

        protected override void InternalDo()
        {
            srcOrder = source.Ordering;

            if (target == null)
                destOrder = 0;
            else
                destOrder = target.Ordering + 1;

            foreach (Chapter c in book.Chapters)
            {
                if (c.Ordering > srcOrder)
                    c.Ordering--;
                if (c.Ordering >= destOrder)
                    c.Ordering++;
            }

            source.Ordering = destOrder;
        }

        protected override void InternalUndo()
        {
            foreach (Chapter c in book.Chapters)
            {
                if (c.Ordering > destOrder)
                    c.Ordering--;
                if (c.Ordering >= srcOrder)
                    c.Ordering++;
            }

            source.Ordering = srcOrder;
        }
    }
}
