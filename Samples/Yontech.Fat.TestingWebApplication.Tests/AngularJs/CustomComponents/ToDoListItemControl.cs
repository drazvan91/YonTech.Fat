using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.TestingWebApplication.Tests.AngularJs.CustomComponents
{
    public class ToDoListItemControl
    {
        private readonly IGenericControl _parentElement;

        public ToDoListItemControl(IGenericControl genericControl)
        {
            this._parentElement = genericControl;
        }

        public void ShouldBeDone()
        {
            if (!_parentElement.Find("span.done-true").Any())
            {
                throw new Exception("ToDo list item should be marked as done");
            }
        }

        public void ShouldNotBeDone()
        {
            if (!_parentElement.Find("span.done-false").Any())
            {
                throw new Exception("ToDo list item should be marked as not done");
            }
        }

        public bool IsDone
        {
            get
            {
                return _parentElement.Find("span.done-true").Any();
            }
        }

        public void NameShouldContain(string textToBeContained)
        {
            var textElement = _parentElement.ControlFinder.Text("span");
            textElement.ShouldContainText(textToBeContained);
        }

        public void ClickOnCheckbox()
        {
            var checkboxControl = _parentElement.ControlFinder.Checkbox("input");
            checkboxControl.Click();
        }

        public void ClickOnName()
        {
            var spanControl = _parentElement.ControlFinder.Text("span");
            spanControl.Click();
        }
    }
}
