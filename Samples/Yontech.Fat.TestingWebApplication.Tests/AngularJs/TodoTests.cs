using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Yontech.Fat.BusyConditions;
using Yontech.Fat.JavaScriptUtilities.BusyConditions;
using Yontech.Fat.TestingWebApplication.Tests.AngularJs.Pages;
using Yontech.Fat.TestingWebApplication.Tests.Config;
using Yontech.Fat.TestingWebApplication.Tests.Runtime;

namespace Yontech.Fat.TestingWebApplication.Tests.AngularJs
{
    public class TodoTests : BaseFatTest
    {
        private readonly TodoPage todoPage;

        public TodoTests(BaseTestContext context) : base(context)
        {
            base.Browser.Configuration.BusyConditions.Clear();
            base.Browser.Configuration.BusyConditions.Add(new AngularPendingRequestsBusyCondition("#todoAngularApp"));
            base.Browser.Configuration.BusyConditions.Add(new ElementIsVisibileBusyCondition(".panel-warning"));
            base.Browser.Configuration.BusyConditions.Add(new FetchPendingRequestsBusyCondition());

            base.Browser.Navigate(PageUrls.ToDoPage);


            this.todoPage = new TodoPage(base.Browser);
        }

        [Fact]
        public void Todo_PageElementsAreVisible()
        {
            todoPage.AddTodoButton.ShouldBeVisible();
            todoPage.ArchiveButton.ShouldBeVisible();
            todoPage.PageTitle.ShouldContainText("Todo");
            todoPage.RemainingDescriptionText.ShouldContainText("1 of 2 remaining");
        }

        [Fact]
        public void Todo_AddOneTodoItem()
        {
            todoPage.RemainingDescriptionText.ShouldContainText("1 of 2 remaining");

            todoPage.NewTodoTextBox.SendKeys("buy tomatoes");
            todoPage.AddTodoButton.Click();

            todoPage.RemainingDescriptionText.ShouldContainText("2 of 3 remaining");

            todoPage.ToDoList.ItemAtPosition(3).ShouldNotBeDone();
            todoPage.ToDoList.ItemAtPosition(3).NameShouldContain("buy tomatoes");

            todoPage.ToDoList.ItemAtPosition(3).ClickOnCheckbox();
            todoPage.ToDoList.ItemAtPosition(3).ShouldBeDone();

            todoPage.ToDoList.ItemAtPosition(3).ClickOnName();
            todoPage.ToDoList.ItemAtPosition(3).ShouldNotBeDone();
        }

        [Fact]
        public void Todo_Add30TodoItems()
        {
            todoPage.RemainingDescriptionText.ShouldContainText("1 of 2 remaining");

            for (int i = 1; i <= 30; i++)
            {
                ;
                var itemName = $"buy tomatos {i}";
                todoPage.NewTodoTextBox.SendKeys(itemName);
                todoPage.AddTodoButton.Click();
            }

            todoPage.RemainingDescriptionText.ShouldContainText("31 of 32 remaining");
        }

        [Fact]
        public void Todo_AddFromBackendUsingHttp()
        {
            todoPage.RemainingDescriptionText.ShouldContainText("1 of 2 remaining");

            todoPage.AddFromBackendButtonUsingHttp.Click();

            todoPage.RemainingDescriptionText.ShouldContainText("2 of 3 remaining");
        }

        [Fact]
        public void Todo_AddFiveFromBackendUsingHttp()
        {
            todoPage.RemainingDescriptionText.ShouldContainText("1 of 2 remaining");

            todoPage.AddFromBackendButtonUsingHttp.RageClick(5);

            todoPage.RemainingDescriptionText.ShouldContainText("6 of 7 remaining");
        }

        [Fact]
        public void Todo_AddFiveFromBackendUsingFetch()
        {
            var elementVisibleCondition = this.Browser.Configuration.BusyConditions.FirstOrDefault(cond => cond is ElementIsVisibileBusyCondition);
            this.Browser.Configuration.BusyConditions.Remove(elementVisibleCondition);
            todoPage.RemainingDescriptionText.ShouldContainText("1 of 2 remaining");

            todoPage.AddFromBackendButtonUsingFetch.RageClick(5);

            todoPage.RemainingDescriptionText.ShouldContainText("6 of 7 remaining");
        }

        [Fact]
        public void Todo_AddDelayed()
        {
            todoPage.RemainingDescriptionText.ShouldContainText("1 of 2 remaining");

            todoPage.AddDelayedButton.Click();

            todoPage.RemainingDescriptionText.ShouldContainText("2 of 3 remaining");
        }

        [Fact]
        public void Todo_FiveDelayed()
        {
            todoPage.RemainingDescriptionText.ShouldContainText("1 of 2 remaining");

            todoPage.AddDelayedButton.RageClick(5);

            todoPage.RemainingDescriptionText.ShouldContainText("6 of 7 remaining");
        }

        [Fact]
        public void Todo_CheckDefaultItems()
        {
            todoPage.ToDoList.ShouldContainNumberOfItems(2);

            todoPage.ToDoList.ItemAtPosition(1).ShouldBeDone();
            todoPage.ToDoList.ItemAtPosition(1).NameShouldContain("learn AngularJS");
            todoPage.ToDoList.ItemAtPosition(2).ShouldNotBeDone();
        }

        [Fact]
        public void Todo_CheckClickOnCoordinates()
        {
            todoPage.ClickArea.Click(10, 20);
            todoPage.ClickArea.ShouldContainText("x=10");
            todoPage.ClickArea.ShouldContainText("y=19");

            todoPage.ClickArea.Click(34, 78);
            todoPage.ClickArea.ShouldContainText("x=34");
            todoPage.ClickArea.ShouldContainText("y=77");
        }
    }
}
