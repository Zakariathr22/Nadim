using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI;
using Nadim.Services;
using Microsoft.UI.Composition.SystemBackdrops;
using System.Xml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.Settings
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (themeComboBox.SelectedIndex == 0) 
            {
                ThemeSelectorService.SetTheme(ElementTheme.Default);
            } else if (themeComboBox.SelectedIndex == 1)
            {
                ThemeSelectorService.SetTheme(ElementTheme.Light);
            } else
            {
                ThemeSelectorService.SetTheme(ElementTheme.Dark);
            }
        }

        private void backDropComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (backDropComboBox.SelectedIndex == 0)
            {
                App.mainWindow.SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.Base};
            }
            else if (backDropComboBox.SelectedIndex == 1)
            {
                App.mainWindow.SystemBackdrop = new MicaBackdrop() { Kind = MicaKind.BaseAlt };
            }
            else
            {
                App.mainWindow.SystemBackdrop = new DesktopAcrylicBackdrop();
            }
            
        }

        private void filesFontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontTestTextBlock != null)
            {
                if (filesFontFamilyComboBox.SelectedIndex == 0)
                {
                    fontTestTextBlock.FontFamily = new FontFamily("Times New Roman");
                } 
                else if (filesFontFamilyComboBox.SelectedIndex == 1)
                {
                    fontTestTextBlock.FontFamily = new FontFamily("Arial");
                }
                else if (filesFontFamilyComboBox.SelectedIndex == 2)
                {
                    fontTestTextBlock.FontFamily = new FontFamily("Calibri");
                }
                else if (filesFontFamilyComboBox.SelectedIndex == 3)
                {
                    fontTestTextBlock.FontFamily = new FontFamily("Sakkal Majalla");
                }
                else if (filesFontFamilyComboBox.SelectedIndex == 4)
                {
                    fontTestTextBlock.FontFamily = new FontFamily("Traditional Arabic");
                }
                else
                {
                    fontTestTextBlock.FontFamily = new FontFamily("Times New Roman");
                }
            }
        }
    }
}
