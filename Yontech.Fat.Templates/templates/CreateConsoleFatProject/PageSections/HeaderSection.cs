﻿using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace CreateConsoleFatProject.PageSections
{
    public class HeaderSection : FatPageSection
    {
        public ILinkControl AuthorLink => _.Link("[itemprop=\"author\"] a");
        public ILinkControl RepoLink => _.Link("strong[itemprop=\"name\"] a");
    }
}
