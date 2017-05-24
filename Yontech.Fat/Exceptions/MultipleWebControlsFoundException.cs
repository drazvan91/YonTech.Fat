using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat.Exceptions
{
    public class MultipleWebControlsFoundException : Exception
    {
        public MultipleWebControlsFoundException() : base("Multiple elements found")
        {
        }
        public MultipleWebControlsFoundException(string message) : base(message)
        {
        }
    }
}
