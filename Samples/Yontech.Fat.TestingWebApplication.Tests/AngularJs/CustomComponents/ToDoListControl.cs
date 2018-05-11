using System;
using System.Linq;
using Xunit;
using Yontech.Fat.WebControls;

namespace Yontech.Fat.TestingWebApplication.Tests.AngularJs.CustomComponents
{
    public class ToDoListControl
    {
        private IGenericControl _parentElement;

        public ToDoListControl(IGenericControl parentElement)
        {
            this._parentElement = parentElement;
        }
        
        /// <summary>
        /// position starts from 1
        /// </summary>
        /// <param name="position">starting from 1</param>
        /// <returns></returns>
        public ToDoListItemControl ItemAtPosition(int position)
        {
            int zeroBasedPosition = position - 1;
            var element = _parentElement.Find("li").Skip(zeroBasedPosition).FirstOrDefault();
            if(element == null)
            {
                throw new Exception($"List item not found at position {position}");
            }

            return new ToDoListItemControl(element);
        }

        public void ShouldContainNumberOfItems(int expectedNumberOfItems)
        {
            int actualNumberOfItems = _parentElement.Find("li").Count();
            Assert.Equal(expectedNumberOfItems, actualNumberOfItems);
        }
    }
}
