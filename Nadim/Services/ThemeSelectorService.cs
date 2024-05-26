using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadim.Services
{
    public  static class ThemeSelectorService
    {
        public static ElementTheme GetTheme(Window window)
        {
            if (window?.Content is FrameworkElement element)
            {
                return element.ActualTheme;
            }
            return ElementTheme.Default;
        }

        public static void SetTheme(ElementTheme theme, Window window)
        {
            if (window?.Content is FrameworkElement element)
            {
                element.RequestedTheme = theme;
            }
        }
    }
}
