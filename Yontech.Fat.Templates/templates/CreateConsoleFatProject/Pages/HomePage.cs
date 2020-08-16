using CreateConsoleFatProject.Components;
using CreateConsoleFatProject.PageSections;
using Yontech.Fat;

namespace CreateConsoleFatProject.Pages
{
    public class HomePage : FatPage
    {
        public HeaderSection HeaderSection { get; set; }

        public FileList FileList => _.Custom<FileList>(".js-details-container.Details [aria-labelledby=\"files\"]");
    }
}
