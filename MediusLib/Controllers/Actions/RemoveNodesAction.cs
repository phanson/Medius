using System.Collections.Generic;
using System.Xml;
using Medius.Model;

namespace Medius.Controllers.Actions
{
    public class RemoveNodesAction : AbstractOperateOnEachAction<Post>
    {
        // this is less than optimal, but it is expedient. TODO: fix it later.
        Dictionary<string, string> undoTable = new Dictionary<string, string>();

        private XmlDocument doc = new XmlDocument();  // dummy xmldocument
        string xpath;

        public RemoveNodesAction(IEnumerable<Post> items, string xpath)
            : base(items)
        {
            this.xpath = xpath;
        }

        protected override void InternalDoForEach(Post item)
        {
            string before = item.Content;
            string after = string.Empty;

            try
            {
                var xml = doc.CreateDocumentFragment();
                xml.InnerXml = before;
                foreach (XmlNode node in xml.SelectNodes(xpath))
                    node.ParentNode.RemoveChild(node);

                after = xml.InnerXml;
            }
            catch (XmlException)
            {
                // skip malformed items for now
                return;
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
