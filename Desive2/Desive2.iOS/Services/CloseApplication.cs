using Desive2.Services;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UIKit;

namespace Desive2.iOS.Services
{
    public class CloseApplication : ICloseApplication
    {
        public void Close()
        {
            Thread.CurrentThread.Abort();
        }
    }
}
