using FatFramework.Sample.PageSections;
using FatFramework.Sample.Components;
using Yontech.Fat;

namespace FatFramework.Sample.Pages
{
    public class HomePage : FatPage
    {
        public HomePageBannerSection Banner { get; set; }

        public TagList TagList => _.Custom<TagList>(".sidebar .tag-list");

        public ArticleList ArticleList => _.Custom<ArticleList>("app-article-list");
    }
}