using System;

namespace Yontech.Fat.Selenium
{
    internal class SeleniumWebBrowserTab : IWebBrowserTab
    {
        private readonly SeleniumWebBrowser _webBrowser;
        private readonly string _tabId;

        public SeleniumWebBrowserTab(SeleniumWebBrowser webBrowser, string tabId)
        {
            this._webBrowser = webBrowser;
            this._tabId = tabId;
        }

        public string Id => this._tabId;

        public void Close()
        {
            var currentTabId = this._webBrowser.WebDriver.CurrentWindowHandle;
            this.Focus();
            this._webBrowser.WebDriver.Close();

            if (currentTabId != this._tabId)
            {
                this._webBrowser.WebDriver = this._webBrowser.WebDriver.SwitchTo().Window(currentTabId);
            }
            else
            {
                var tabs = this._webBrowser.WebDriver.WindowHandles;
                if (tabs.Count > 0)
                {
                    this._webBrowser.WebDriver = this._webBrowser.WebDriver.SwitchTo().Window(tabs[0]);
                }
            }
        }

        public IWebBrowserTab Focus()
        {
            this._webBrowser.WebDriver = this._webBrowser.WebDriver.SwitchTo().Window(this._tabId);
            return this;
        }
    }
}
