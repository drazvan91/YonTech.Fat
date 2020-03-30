using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace RealWorld.Angular.Tests.PageSections
{
  public class HeaderSection : FatPageSection
  {
    public ILinkControl LogoLink => _.Link("app-layout-header a.navbar-brand");
    public ILinkControl SignInLink => _.Link("app-layout-header a[routerlink=\"/login\"]");
    public ILinkControl UserNameLink => _.Link("app-layout-header ul li:last-child a");
  }
}