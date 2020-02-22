using System;
using System.Collections.Generic;
using System.Linq;
using Yontech.Fat;

namespace CreateConsoleFatProject.Components
{
    public class FileList : FatCustomComponent
    {
        private IEnumerable<FileListItem> items => _.CustomList<FileListItem>("tr.js-navigation-item");

        public FileListItem FileWithName(string name)
        {
            return this.items.FirstOrDefault(item => item.NameLink.Text == name);
        }
        public void ShouldContainFile(string fileName)
        {
            var contains = items.Any(t => t.NameLink.Text == fileName);

            if (!contains)
            {
                throw new Exception("Tag lists should contain text '" + fileName + "'");
            }
        }
    }
}