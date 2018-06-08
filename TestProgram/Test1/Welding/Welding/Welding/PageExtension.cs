using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Welding
{
    public static class PageExtension
    {
        public static MainWindow ParentWindow(this Page page)
        {
            return (MainWindow) page.Parent;
        }
    }
}
