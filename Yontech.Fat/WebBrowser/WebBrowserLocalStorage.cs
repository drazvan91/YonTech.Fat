using System.Collections.ObjectModel;
using OpenQA.Selenium.Html5;
using OpenQA.Selenium.Remote;

namespace Yontech.Fat.WebBrowser
{
    internal class WebBrowserLocalStorage : IWebBrowserStorage
    {
        private readonly RemoteWebDriver _webDriver;

        public WebBrowserLocalStorage(RemoteWebDriver webDriver)
        {
            this._webDriver = webDriver;
        }

        private ILocalStorage GetStorage()
        {
            return _webDriver.WebStorage.LocalStorage;
        }

        public int Count => GetStorage().Count;

        public void Clear()
        {
            GetStorage().Clear();
        }

        public string GetItem(string key)
        {
            return GetStorage().GetItem(key);
        }

        public ReadOnlyCollection<string> GetAll()
        {
            return GetStorage().KeySet();
        }

        public string RemoveItem(string key)
        {
            return GetStorage().RemoveItem(key);
        }

        public void SetItem(string key, string value)
        {
            GetStorage().SetItem(key, value);
        }
    }
}
