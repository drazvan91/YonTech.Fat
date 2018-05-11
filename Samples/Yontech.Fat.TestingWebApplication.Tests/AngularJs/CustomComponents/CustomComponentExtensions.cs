using System;
using System.Collections.Generic;
using System.Text;

namespace Yontech.Fat.TestingWebApplication.Tests.AngularJs.CustomComponents
{
    public static class CustomComponentExtensions
    {
        public static ToDoListControl ToDoList(this IControlFinder controlFinder, string cssSelector)
        {
            var parentElement = controlFinder.Generic(cssSelector);
            return new ToDoListControl(parentElement);
        }
    }
}
