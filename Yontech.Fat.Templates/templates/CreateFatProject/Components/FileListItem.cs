using System;
using System.Collections.Generic;
using System.Linq;
using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace CreateFatProject.Components
{
    public class FileListItem : FatCustomComponent
    {
        public ILinkControl NameLink => _.Link(".content a");
        public ILinkControl MessageLink => _.Link(".message a");
    }
}