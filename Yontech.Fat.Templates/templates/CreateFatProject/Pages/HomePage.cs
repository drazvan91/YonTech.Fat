using CreateFatProject.Components;
using CreateFatProject.PageSections;
using Yontech.Fat;
using Yontech.Fat.WebControls;

namespace CreateFatProject.Pages
{
    public class HomePage : FatPage
    {
        public HeaderSection HeaderSection { get; set; }

        public FileList FileList => _.Custom<FileList>("table.files");
    }
}