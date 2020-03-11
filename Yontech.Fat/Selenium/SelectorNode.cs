using System.Collections.Generic;

namespace Yontech.Fat.Selenium
{
    internal class SelectorNode
    {
        public string Selector { get; set; }
        public int? Index { get; set; }
        public SelectorNode Parent { get; set; }

        public SelectorNode(string selector, int? index, SelectorNode parent = null)
        {
            this.Selector = selector;
            this.Index = index;
            this.Parent = parent;
        }

        public string GetFullPath()
        {
            return string.Join(" | ", GetSelectorDescriptions());
        }

        private IEnumerable<string> GetSelectorDescriptions()
        {
            if (this.Parent != null)
            {
                foreach (var selector in this.Parent.GetSelectorDescriptions())
                {
                    yield return selector;
                }
            }

            if (this.Index.HasValue)
            {
                yield return $"{this.Selector}:{this.Index}";
            }
            else
            {
                yield return this.Selector;
            }
        }
    }
}
