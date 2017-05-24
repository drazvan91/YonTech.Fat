using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.Exceptions
{
    public class WebControlNotFoundException : Exception
    {
        public WebControlNotFoundException() : base("Element not visible")
        {
        }
        public WebControlNotFoundException(string message) : base(message)
        {
        }
    }
}
