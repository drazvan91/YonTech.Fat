using Yontech.Fat;
using CreateConsoleFatProject.Pages;
using CreateConsoleFatProject.Data;

namespace CreateConsoleFatProject.TestCases
{
    public class HomePageTests : FatTest
    {
        HomePage homePage { get; set; }

        public override void BeforeEachTestCase()
        {
            WebBrowser.Navigate(Urls.HOME_PAGE);
        }

        public void Test_header_elements_exist()
        {
            homePage.HeaderSection.AuthorLink.ShouldHaveText("drazvan91");
            homePage.HeaderSection.RepoLink.ShouldHaveText("YonTech.Fat");
        }

        public void Test_should_contain_docs_folder()
        {
            homePage.FileList.ShouldContainFile("docs");
        }
    }
}