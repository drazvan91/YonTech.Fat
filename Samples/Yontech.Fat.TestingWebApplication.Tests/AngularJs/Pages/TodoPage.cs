using System;
using System.Collections.Generic;
using System.Text;
using Yontech.Fat.TestingWebApplication.Tests.AngularJs.CustomComponents;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.TestingWebApplication.Tests.AngularJs.Pages
{
    public class TodoPage : BasePageHandler
    {
        public TodoPage(IWebBrowser webBrowser) : base(webBrowser)
        {

        }

        public ITextControl PageTitle
        {
            get
            {
                return ControlFinder.Text("#pageTitle");
            }
        }

        public ITextControl RemainingDescriptionText
        {
            get
            {
                return ControlFinder.Text("#remainingDescription");
            }
        }

        public ITextBoxControl NewTodoTextBox
        {
            get
            {
                return ControlFinder.TextBox("#newTodoInput");
            }
        }

        public IButtonControl AddTodoButton
        {
            get
            {
                return ControlFinder.Button("#newTodoButton");
            }
        }

        public IButtonControl AddFromBackendButton
        {
            get
            {
                return ControlFinder.Button("#addFromBackend");
            }
        }

        public IButtonControl AddDelayedButton
        {
            get
            {
                return ControlFinder.Button("#addDelayed");
            }
        }

        public IButtonControl ArchiveButton
        {
            get
            {
                return ControlFinder.Button("#archiveButton");
            }
        }

        public ToDoListControl ToDoList
        {
            get
            {
                return ControlFinder.ToDoList("ul");
            }
        }
    }
}
