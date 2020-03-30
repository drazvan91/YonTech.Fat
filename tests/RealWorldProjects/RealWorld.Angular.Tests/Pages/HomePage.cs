using RealWorld.Angular.Tests.PageSections;
using RealWorld.Angular.Tests.Components;
using Yontech.Fat;

namespace RealWorld.Angular.Tests.Pages
{
  public class HomePage : FatPage
  {
    public HomePageBannerSection Banner { get; set; }

    public TagList TagList => _.Custom<TagList>(".sidebar .tag-list");

    public ArticleList ArticleList => _.Custom<ArticleList>("app-article-list");
  }
}