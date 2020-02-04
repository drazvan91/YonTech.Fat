using System;
using System.Collections.Generic;
using System.Linq;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.Plugins.Bootstrap
{
    public class BootstrapDropdownControl : IDropdownControl
    {
        private readonly IGenericControl _container;


        public BootstrapDropdownControl(IGenericControl containerControl)
        {
            this._container = containerControl;
        }

        public bool IsOpen
        {
            get
            {
                return _container.GetAttribute("class").Contains("open");
            }
        }

        public string SelectedItem
        {
            get
            {
                var input = _container.Find(".dropdown-toggle input").First();
                return input.GetAttribute("value");
            }
        }

        public string ToggleText
        {
            get
            {
                var toggle = _container.Find(".dropdown-toggle").First();
                return toggle.Text;
            }
        }

        public bool IsVisible
        {
            get
            {
                return _container.IsVisible;
            }
        }

        public bool Exists
        {
            get
            {
                return _container != null;
            }
        }

        public bool IsDisabled => throw new NotImplementedException();

        public void ToggleTextShouldBe(string text)
        {
            if (this.ToggleText != text)
            {
                throw new Exception($"Toggle text contains '{this.ToggleText}' instead of '{text}'");
            }
        }

        public void Close()
        {
            if (IsOpen)
            {
                _container.Click();
            }
        }

        private IEnumerable<IGenericControl> GetItemGenericControls()
        {
            return _container.Find(".dropdown-menu li");
        }

        public IEnumerable<string> GetItems()
        {
            foreach (var element in GetItemGenericControls())
            {
                yield return element.Text;
            }
        }

        public void Open()
        {
            _container.Click();
        }

        public void SelectItem(string itemText)
        {
            if (!IsOpen)
            {
                Open();
            }

            foreach (var element in GetItemGenericControls())
            {
                if (element.Text == itemText)
                {
                    element.ScrollTo();
                    element.Click();
                    return;
                }
            }

            //todo: this should be a specific exception type
            throw new Exception("Cannot find specified item in the dropdown");
        }

        public void SelectItem(int index)
        {
            if (!IsOpen)
            {
                Open();
            }

            var element = GetItemGenericControls().Skip(index - 1).FirstOrDefault();
            if (element != null)
            {
                element.ScrollTo();
                element.Click();
            }

            //todo: this should be a specific exception type
            throw new Exception("Cannot find specified item in the dropdown");
        }

        public void ScrollTo()
        {
            _container.ScrollTo();
        }

        public void ShouldBeVisible()
        {
            _container.ShouldBeVisible();
        }

        public void ShouldNotBeVisible()
        {
            _container.ShouldNotBeVisible();
        }

        private void EnsureElementExists()
        {
            throw new NotImplementedException();
        }


    }
}
