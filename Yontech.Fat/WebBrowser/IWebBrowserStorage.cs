using System.Collections.ObjectModel;

namespace Yontech.Fat.WebBrowser
{
    public interface IWebBrowserStorage
    {
        //
        // Summary:
        //     Gets the number of items in local storage.
        int Count { get; }

        //
        // Summary:
        //     Clears local storage.
        void Clear();
        //
        // Summary:
        //     Returns value of the local storage given a key.
        //
        // Parameters:
        //   key:
        //     key to for a local storage entry
        //
        // Returns:
        //     Value of the local storage entry as System.String given a key.
        string GetItem(string key);
        //
        // Summary:
        //     Returns the set of keys associated with local storage.
        //
        // Returns:
        //     Returns the set of keys associated with local storage as System.Collections.Generic.HashSet`1.
        ReadOnlyCollection<string> GetAll();
        //
        // Summary:
        //     Removes key/value pair from local storage.
        //
        // Parameters:
        //   key:
        //     key to remove from storage
        //
        // Returns:
        //     Value from local storage as string for the given key.
        string RemoveItem(string key);
        //
        // Summary:
        //     Adds key/value pair to local storage.
        //
        // Parameters:
        //   key:
        //     storage key
        //
        //   value:
        //     storage value
        void SetItem(string key, string value);
    }
}
