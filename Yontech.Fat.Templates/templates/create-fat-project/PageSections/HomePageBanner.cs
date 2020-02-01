using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace FatFramework.Sample.PageSections
{
    public class HomePageBannerSection : FatPageSection
    {
        public ITextControl Title => _.Text("app-home-page .banner h1");
        public ITextControl Description => _.Text("app-home-page .banner p");

    }
}