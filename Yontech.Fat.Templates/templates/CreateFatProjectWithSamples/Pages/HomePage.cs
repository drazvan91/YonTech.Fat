using CreateFatProjectWithSamples.PageSections;
using CreateFatProjectWithSamples.Components;
using Yontech.Fat;

namespace CreateFatProjectWithSamples.Pages
{
    public class HomePage : FatPage
    {
        public HomePageBannerSection Banner { get; set; }

        public TagList TagList => _.Custom<TagList>(".sidebar .tag-list");

        public ArticleList ArticleList => _.Custom<ArticleList>("app-article-list");
    }
}