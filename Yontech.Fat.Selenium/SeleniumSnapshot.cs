using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using System.Drawing.Imaging;

namespace Yontech.Fat.Selenium
{
    internal class SeleniumSnapshot : ISnapshot
    {
        private Screenshot shot;

        public SeleniumSnapshot(Screenshot shot)
        {
            this.shot = shot;
        }

        public void SaveAsFile(string path)
        {
            shot.SaveAsFile(path, ImageFormat.Png);
        }
    }
}
