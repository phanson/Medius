using Medius.Model;

namespace Medius.Controllers.Actions
{
    public class MovePostAction : AbstractAction
    {
        Chapter cSource, cTarget;
        Post pSource, pTarget;
        int srcOrder, destOrder;

        public MovePostAction(Chapter cSource, Post pSource, Chapter cTarget, Post pTarget = null)
        {
            this.cSource = cSource;
            this.pSource = pSource;
            this.cTarget = cTarget;
            this.pTarget = pTarget;
        }

        protected override void InternalDo()
        {
            srcOrder = pSource.Ordering;

            if (pTarget == null)
                destOrder = 0;
            else
                destOrder = pTarget.Ordering + 1;

            foreach (Post p in cTarget.Posts)
                if (p.Ordering >= destOrder)
                    p.Ordering++;

            foreach (Post p in cSource.Posts)
                if (p.Ordering > srcOrder)
                    p.Ordering--;

            pSource.Ordering = destOrder;

            cSource.Posts.Remove(pSource);
            cTarget.Posts.Add(pSource);
        }

        protected override void InternalUndo()
        {
            foreach (Post p in cSource.Posts)
                if (p.Ordering >= srcOrder)
                    p.Ordering++;

            foreach (Post p in cTarget.Posts)
                if (p.Ordering > destOrder)
                    p.Ordering--;

            pSource.Ordering = srcOrder;

            cTarget.Posts.Remove(pSource);
            cSource.Posts.Add(pSource);
        }
    }
}
