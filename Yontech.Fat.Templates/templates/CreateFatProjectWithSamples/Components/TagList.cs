using System;
using System.Collections.Generic;
using System.Linq;
using Yontech.Fat;
using Yontech.Fat.Exceptions;
using Yontech.Fat.WebControls;

namespace CreateFatProjectWithSamples.Components
{
    public class TagList : FatCustomComponent
    {
        private IEnumerable<ILinkControl> Tags => _.LinkList(".tag-pill");

        public ILinkControl TagWithText(string text)
        {
            var tagLink = this.Tags.FirstOrDefault(tag => tag.Text == text);

            if (tagLink == null)
            {
                Fail("There is no tag with text '{0}' ni the tags list", text);
            }

            return tagLink;
        }

        public void ShouldContainTag(string tagText)
        {
            var contains = Tags.Any(t => t.Text == tagText);

            if (!contains)
            {
                throw new FatAssertException("Tags list should contain text '" + tagText + "'");
            }
        }
    }
}
