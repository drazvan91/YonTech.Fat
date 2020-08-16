using System;
using System.Collections.Generic;
using System.Linq;
using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace RealWorld.Angular.Tests.Components
{
    public class TagList : FatCustomComponent
    {
        private IEnumerable<ILinkControl> Tags => _.LinkList(".tag-pill");

        public ILinkControl TagWithText(string text)
        {
            var tag = this.Tags.FirstOrDefault(tag => tag.Text == text);

            if (tag == null)
            {
                Fail("Tags list with selector '{1}' does not contain '{0}'", tag, base.Container.SelectorDescription);
            }

            return tag;
        }

        public void ShouldContainTag(string tagText)
        {
            var contains = Tags.Any(t => t.Text == tagText);

            if (!contains)
            {
                Fail("Tags list with selector '{1}' should contain text '{0}'", tagText, base.Container.SelectorDescription);
            }
        }
    }
}
