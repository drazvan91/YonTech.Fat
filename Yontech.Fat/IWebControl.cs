using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yontech.Fat
{
    public interface IWebControl
    {
        void ScrollTo();
        void ShouldBeVisible();
        void ShouldNotBeVisible();
        bool IsVisible { get; }
        bool Exists { get; }
    }
}
