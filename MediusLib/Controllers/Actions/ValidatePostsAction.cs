using System.Collections.Generic;
using System.Xml;
using Medius.Model;

namespace Medius.Controllers.Actions
{
    /// <summary>
    /// This is a special-case action that has no effects.
    /// </summary>
    public class ValidatePostsAction : AbstractOperateOnEachAction<Post>
    {
        public List<Post> NonValidatingPosts = new List<Post>();

        private XmlDocumentFragment xmlFragment = new XmlDocument().CreateDocumentFragment();  // dummy xmldocument

        public ValidatePostsAction(IEnumerable<Post> items)
            : base(items)
        {
        }

        protected override void InternalDoForEach(Post item)
        {
            // aggregate posts that fail validation
            try
            {
                xmlFragment.InnerXml = item.Content;
            }
            catch (XmlException)
            {
                NonValidatingPosts.Add(item);
            }
        }

        protected override void InternalUndoForEach(Post item)
        {
            // nothing to undo
        }
    }
}
