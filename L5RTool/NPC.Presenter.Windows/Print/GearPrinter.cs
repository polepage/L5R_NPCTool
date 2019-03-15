using NPC.Presenter.GameObjects;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NPC.Presenter.Windows.Print
{
    class GearPrinter : BaseGameObjectPrinter
    {
        public GearPrinter(double maxWidth, double maxHeight)
            : base(maxWidth, maxHeight)
        {
        }

        public IEnumerable<FrameworkElement> CreatePrintView(IGear gear)
        {
            var grid = CreateGrid(string.IsNullOrEmpty(gear.Description) ? 1 : 2);

            var name = CreateObjectName(gear.Name.Trim() + " (" + gear.GearType.ToString() + ")");
            Grid.SetRow(name, 0);
            grid.Children.Add(name);

            if (!string.IsNullOrWhiteSpace(gear.Description))
            {
                var description = new TextBlock
                {
                    Text = gear.Description.Trim(),
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 4, 0, 0)
                };
                Grid.SetRow(description, 1);
                grid.Children.Add(description);
            }

            DoMeasure(grid);
            return new List<FrameworkElement> { grid };
        }
    }
}
