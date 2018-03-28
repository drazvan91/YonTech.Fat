using System.Collections.Generic;

namespace Yontech.Fat.WebControls
{
    public interface IDropdownControl:IWebControl
    {
        bool IsOpen { get; }
        string ToggleText { get; }
        void ToggleTextShouldBe(string text);
        string SelectedItem { get; }
        IEnumerable<string> GetItems();
        void Open();
        void Close();
        void SelectItem(string itemText);
        void SelectItem(int index);
    }
}