using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Telemedic.iOS
{
    class StatusBar : IStatusBar
    {
        public int GetHeight()
        {
            return (int)UIApplication.SharedApplication.StatusBarFrame.Height;
        }
    }
}