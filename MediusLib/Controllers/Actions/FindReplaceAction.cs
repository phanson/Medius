using System.Collections.Generic;
using System.Text.RegularExpressions;
using Medius.Model;

namespace Medius.Controllers.Actions
{
    public class FindReplaceAction : AbstractOperateOnEachAction<Post>
    {
        // this is less than optimal, but it is expedient. TODO: fix it later.
        Dictionary<string, string> undoTable = new Dictionary<string, string>();

        string pattern;
        string replacement;
        bool regex;

        public FindReplaceAction(IEnumerable<Post> items, string pattern, string replacement, bool useRegex)
            : base(items)
        {
            this.pattern = pattern;
            this.replacement = replacement;
            this.regex = useRegex;
        }

        protected string Replace(string original, string pattern, string replacement, bool useRegex)
        {
            if (useRegex)
                return Regex.Replace(original, pattern, replacement, RegexOptions.CultureInvariant);
            else
                return original.Replace(pattern, replacement);
        }

        protected override void InternalDoForEach(Post item)
        {
            string replaced = Replace(item.Content, pattern, replacement, regex);
            undoTable[replaced] = item.Content;
            item.Content = replaced;
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
