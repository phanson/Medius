using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Medius.Model;
using System.Xml;

namespace Medius.Controllers.Actions
{
    public class RemoveImagesAction : AbstractOperateOnEachAction<Post>
    {
        // this is less than optimal, but it is expedient. TODO: fix it later.
        Dictionary<string, string> undoTable = new Dictionary<string, string>();

        public RemoveImagesAction(List<Post> items)
            : base(items)
        {
        }

        protected override void InternalDoForEach(Post item)
        {
            string before = item.Content;
            string after = string.Empty;

            try
            {
                var xml = new XmlDocument().CreateDocumentFragment();
                xml.InnerXml = before;
                foreach (XmlNode node in xml.SelectNodes("//img"))
                    node.ParentNode.RemoveChild(node);

                after = xml.InnerXml;
            }
            catch (XmlException)
            {
                return;  // ignore malformed documents
                // TODO: log errors
            }

            undoTable[after] = before;
            item.Content = after;
        }

        protected override void InternalUndoForEach(Post item)
        {
            string restored;
            if (undoTable.TryGetValue(item.Content, out restored))
            {
                undoTable.Remove(item.Content);
                item.Content = restored;
            }
        }
    }
}
