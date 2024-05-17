using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Nadim.Views.Controls
{
    public sealed partial class ActivityOverviewCalendar : UserControl
    {
        public ActivityOverviewCalendar()
        {
            this.InitializeComponent();
            AddBordersToGrid(calendarGrid, 2024);
        }

        public void AddBordersToGrid(Grid grid, int year)
        {
            // Define the number of rows and columns
            int rows = 7;
            int columns = 53; // Maximum number of weeks in a year

            // Clear any existing definitions
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            // Define the rows and columns
            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            for (int i = 0; i < columns; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Start from the first day of the year
            DateTime date = new DateTime(year, 1, 1);

            // Determine the starting row based on the day of the week
            int startRow = (int)date.DayOfWeek;

            // Add a border to each cell
            for (int col = 0; col < columns; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    if (col == 0 && row < startRow)
                    {
                        continue;
                    }
                    else
                    {
                        // Only add a border if the date is in the current year
                        if (date.Year == year)
                        {
                            Border border = new Border
                            {
                                Background = new SolidColorBrush(Color.FromArgb(32, 128, 128, 128)),
                                Name = $"Cell_{date:yyyyMMdd}",
                                CornerRadius = new CornerRadius(2),
                                Width = 16,
                                Height = 16,
                                Margin = new Thickness(1)
                            };

                            // Add a tooltip to the border
                            ToolTip tooltip = new ToolTip
                            {
                                Content = $"{date:D}"
                            };
                            ToolTipService.SetToolTip(border, tooltip);

                            // Highlight today's date
                            if (date.Date == DateTime.Today)
                            {
                                border.Background = new SolidColorBrush(Color.FromArgb(128, 0, 128, 128));
                            }
                            if (date.Date == DateTime.Today.AddDays(1))
                            {
                                border.Background = new SolidColorBrush(Color.FromArgb(64, 0, 128, 128));
                            }
                            if (date.Date == DateTime.Today.AddDays(-1))
                            {
                                border.Background = new SolidColorBrush(Color.FromArgb(192, 0, 128, 128));
                            }
                            if (date.Date == DateTime.Today.AddDays(-2))
                            {
                                border.Background = new SolidColorBrush(Color.FromArgb(255, 0, 128, 128));
                            }

                            Grid.SetRow(border, row);
                            Grid.SetColumn(border, col);

                            grid.Children.Add(border);
                        }

                        // Move to the next day
                        date = date.AddDays(1);
                    }
                }
            }
        }
    }
}
