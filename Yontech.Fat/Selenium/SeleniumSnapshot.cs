using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Yontech.Fat.Selenium
{
    internal class SeleniumSnapshot : ISnapshot
    {
        private Screenshot _shot;

        public SeleniumSnapshot(Screenshot shot)
        {
            this._shot = shot;
        }

        public void SaveAsFile(string path)
        {
            // shot.SaveAsFile(path, ImageFormat.Png);
        }
    }
}
