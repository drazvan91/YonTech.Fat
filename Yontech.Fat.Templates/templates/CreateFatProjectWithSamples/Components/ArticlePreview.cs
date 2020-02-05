using System.Linq;
using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace CreateFatProjectWithSamples.Components
{
    public class ArticlePreview : FatCustomComponent
    {
        public ILinkControl UserNameLink => _.Link(".author");
        public ITextControl PublishedDateText => _.Text("span.date");
        public ITextControl TitleText => _.Text(".preview-link h1");

        public TagList Tags => _.Custom<TagList>(".tag-list");
    }
}