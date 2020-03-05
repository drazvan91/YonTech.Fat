﻿using Yontech.Fat.WebControls;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class TextBoxControl : BaseSeleniumControl, ITextBoxControl
    {
        public TextBoxControl(IWebElement webElement, SeleniumWebBrowser webBrowser)
            : base(webElement, webBrowser)
        {
        }

        public string Text
        {
            get
            {
                EnsureElementExists();
                return WebElement.GetAttribute("value");
            }
        }

        public void ClearText()
        {
            EnsureElementExists();
            // todo: investigate why this is needed, 
            // also, if needed, make sure it works also on MacOs
            WebElement.SendKeys(Keys.Control + "a");
            WebElement.SendKeys(Keys.Backspace);
            WebElement.Clear();
        }

        // public void Click()
        // {
        //     EnsureElementExists();
        //     WebElement.Click();
        // }

        public void TypeKeys(string keys)
        {
            this.TypeKeysSlowly(keys, 1);
        }

        public void TypeKeysSlowly(string keys, int delayBetweenKeys = 300)
        {
            EnsureElementExists();
            foreach (var key in keys)
            {
                WebElement.SendKeys(key.ToString());
                Thread.Sleep(delayBetweenKeys);
            }
            base.WebBrowser.WaitForIdle();
        }


        public void ClearAndTypeKeys(string keys)
        {
            this.ClearText();
            this.TypeKeys(keys);
        }

        public void ClearAndTypeKeysSlowly(string keys, int delayBetweenKeys = 300)
        {
            this.ClearText();
            this.TypeKeysSlowly(keys, delayBetweenKeys);
        }

        public void SendKeys(string keys)
        {
            this.ClearAndTypeKeys(keys);
        }
    }
}
