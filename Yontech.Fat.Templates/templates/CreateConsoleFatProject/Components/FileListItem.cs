using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace CreateConsoleFatProject.Components
{
    public class FileListItem : FatCustomComponent
    {
        public ILinkControl NameLink => _.Link(".content a");
        public ILinkControl MessageLink => _.Link(".message a");
    }
}
