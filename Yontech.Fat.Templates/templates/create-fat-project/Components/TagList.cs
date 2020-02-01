using System;
using System.Collections.Generic;
using System.Linq;
using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace FatFramework.Sample.Components
{
    public class TagList : FatCustomComponent
    {
        private IEnumerable<ILinkControl> Tags => _.LinkList(".tag-pill");

        public ILinkControl TagWithText(string text)
        {
            return this.Tags.FirstOrDefault(tag => tag.Text == text);
        }

        public void ShouldContainTag(string tagText)
        {
            var contains = Tags.Any(t => t.Text == tagText);

            if (!contains)
            {
                throw new Exception("Tag lists should contain text '" + tagText + "'");
            }
        }
    }
}