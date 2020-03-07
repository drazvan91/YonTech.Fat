using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace CreateFatProject.PageSections
{
    public class HeaderSection : FatPageSection
    {
        public ILinkControl AuthorLink => _.Link(".pagehead [itemprop=\"author\"] a");
        public ILinkControl RepoLink => _.Link(".pagehead strong[itemprop=\"name\"] a");
    }
}
