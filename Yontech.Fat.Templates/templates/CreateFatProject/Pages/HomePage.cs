using CreateFatProject.Components;
using CreateFatProject.PageSections;
using Yontech.Fat;

namespace CreateFatProject.Pages
{
    public class HomePage : FatPage
    {
        public HeaderSection HeaderSection { get; set; }

        public FileList FileList => _.Custom<FileList>(".js-details-container.Details [aria-labelledby=\"files\"]");
    }
}
