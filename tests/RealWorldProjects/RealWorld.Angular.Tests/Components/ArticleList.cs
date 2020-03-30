using System.Collections.Generic;
using System.Linq;
using Yontech.Fat;

namespace RealWorld.Angular.Tests.Components
{
  public class ArticleList : FatCustomComponent
  {
    private IEnumerable<ArticlePreview> Articles => _.CustomList<ArticlePreview>("app-article-preview");

    public ArticlePreview ArticleAtPosition(int index)
    {
      return Articles.ElementAtOrDefault(index);
    }
  }
}