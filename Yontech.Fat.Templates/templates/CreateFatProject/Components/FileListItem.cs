using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace CreateFatProject.Components
{
    public class FileListItem : FatCustomComponent
    {
        public ILinkControl NameLink => _.Link("[role=\"rowheader\"]");
        public ILinkControl MessageLink => _.Link(".commit-message");
    }
}
