using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using Yontech.Fat.Exceptions;
using Yontech.Fat.Selenium.SeleniumExtensions;
using Yontech.Fat.Waiters;

namespace Yontech.Fat.Selenium.WebControls
{
    internal class BaseSeleniumControl : IWebControl
    {
        protected internal readonly SelectorNode SelectorNode;
        protected internal readonly IWebElement WebElement;
        protected internal readonly SeleniumWebBrowser WebBrowser;


        public BaseSeleniumControl(SelectorNode selectorNode, IWebElement webElement, SeleniumWebBrowser webBrowser)
        {
            this.SelectorNode = selectorNode;
            this.WebElement = webElement;
            this.WebBrowser = webBrowser;
        }
        public bool IsVisible
        {
            get
            {
                return this.WebElement != null && this.WebElement.Displayed;
            }
        }

        public bool Exists
        {
            get
            {
                return this.WebElement != null;
            }
        }

        public void Click()
        {
            EnsureClickAction(() =>
            {
                this.WebElement.Click();
            });
        }

        public void Click(int relativeX, int relativeY)
        {
            EnsureClickAction(() =>
           {
               Actions builder = new Actions(this.WebBrowser.WebDriver);
               builder.MoveToElement(this.WebElement, relativeX, relativeY).Click().Build().Perform();
           });
        }

        public void DoubleClick()
        {
            EnsureClickAction(() =>
            {
                Actions builder = new Actions(this.WebBrowser.WebDriver);
                builder.MoveToElement(this.WebElement).DoubleClick().Build().Perform();
            });
        }

        public void DoubleClick(int relativeX, int relativeY)
        {
            EnsureClickAction(() =>
            {
                Actions builder = new Actions(this.WebBrowser.WebDriver);
                builder.MoveToElement(this.WebElement, relativeX, relativeY).DoubleClick().Build().Perform();
            });
        }

        public void RageClick(int numberOfClicks)
        {
            EnsureClickAction(() =>
            {
                Actions builder = new Actions(this.WebBrowser.WebDriver);
                var action = builder.MoveToElement(this.WebElement);

                for (int i = 0; i < numberOfClicks; i++)
                {
                    action = action.Click();
                }

                action.Build().Perform();
            });
        }

        private void EnsureClickAction(Action performClick)
        {
            EnsureElementExists();

            this.WebBrowser.WaitForIdle();
            this.WaitForClickable();
            try
            {
                try
                {
                    performClick();
                }
                catch (Exception ex) when (ex.Message.Contains("Other element would receive"))
                {
                    this.ScrollTo();
                    performClick();
                }
            }
            catch (InvalidOperationException ex)
            {
                string outerHtml = this.WebElement.GetAttribute("outerHTML");
                throw new InvalidOperationException($"Target element: {outerHtml}", ex);
            }

            this.WebBrowser.WaitForIdle();
        }

        public virtual void ScrollTo()
        {
            EnsureElementExists();
            WebBrowser.JavaScriptExecutor.ScrollToControl(this);
        }

        protected void EnsureElementExists()
        {
            if (WebElement == null)
            {
                throw new FatException($"Element with selector '{this.SelectorNode.GetFullPath()}' should exist but it doesn't.");
            }
        }

        public void ShouldBeVisible()
        {
            EnsureElementExists();

            var remoteWebElem = WebElement as RemoteWebElement;
            if (remoteWebElem == null || !remoteWebElem.Displayed)
            {
                throw new FatAssertException($"Element with selector '{this.SelectorNode.GetFullPath()}' should be visible but it isn't.");
            }
        }

        public void ShouldNotBeVisible()
        {
            var remoteWebElem = WebElement as RemoteWebElement;
            if (remoteWebElem != null && remoteWebElem.Displayed)
            {
                throw new FatAssertException($"Element with selector '{this.SelectorNode.GetFullPath()}' should NOT be visible but it is.");
            }
        }

        public bool IsDisabled
        {
            get
            {
                this.ShouldBeVisible();
                return !WebElement.Enabled;
            }
        }

        public string[] CssClasses
        {
            get
            {
                EnsureElementExists();
                return WebElement.GetAttribute("class").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public string CssClass
        {
            get
            {
                EnsureElementExists();
                return WebElement.GetAttribute("class");
            }
        }
        public bool IsClickable
        {
            get
            {
                EnsureElementExists();
                Waiter.WaitForConditionToBeTrueOrTimeout(() =>
                {
                    if (WebElement.IsClickable())
                    {
                        return true;
                    }

                    return false;
                }, WebBrowser.Configuration.DefaultTimeout);

                return WebElement.IsClickable();
            }
        }

        public string SelectorDescription => SelectorNode.GetFullPath();

        public string GetAttribute(string attributeName)
        {
            EnsureElementExists();
            return WebElement.GetAttribute(attributeName);
        }

        public string GetCssPropertyValue(string propertyName)
        {
            EnsureElementExists();
            return WebElement.GetCssValue(propertyName);
        }

        public void Hover()
        {
            EnsureElementExists();

            Actions SliderAction = new Actions(this.WebBrowser.WebDriver);
            SliderAction.MoveToElement(this.WebElement).Build().Perform();
        }

        public void DragAndDrop(int pixelsHorizontal, int pixelsVertical)
        {
            EnsureElementExists();

            Actions SliderAction = new Actions(this.WebBrowser.WebDriver);
            SliderAction.DragAndDropToOffset(this.WebElement, pixelsHorizontal, pixelsVertical).Perform();
        }

        public void WaitToDisappear()
        {
            Waiter.WaitForConditionToBeTrue(() =>
            {
                try
                {
                    return WebElement.Displayed == false;
                }
                catch
                {
                    return true;
                }
            }, WebBrowser.Configuration.DefaultTimeout);
        }

        public void WaitToAppear()
        {
            Waiter.WaitForConditionToBeTrue(() =>
            {
                try
                {
                    return WebElement.Displayed == true;
                }
                catch
                {
                    return false;
                }
            }, WebBrowser.Configuration.DefaultTimeout);
        }

        public void WaitForClickable()
        {
            Waiter.WaitForConditionToBeTrue(() =>
                {
                    if (WebElement.IsClickable())
                    {
                        return true;
                    }

                    return false;
                }, WebBrowser.Configuration.DefaultTimeout);
        }
    }
}
