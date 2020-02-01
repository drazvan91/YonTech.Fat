using System.Collections.Generic;
using System.Linq;
using Yontech.Fat;

namespace FatFramework.Sample.Components
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