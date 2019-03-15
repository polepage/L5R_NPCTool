using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NPC.Presenter.Windows.Print
{
    abstract class BaseGameObjectPrinter
    {
        protected static readonly Uri FontUri = new Uri("pack://application:,,,/NPC.Presenter.Windows;component/Fonts/");

        public BaseGameObjectPrinter(double maxWidth, double maxHeight)
        {
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
        }

        protected double MaxWidth { get; }
        protected double MaxHeight { get; }

        protected Grid CreateGrid(int rows)
        {
            var grid = new Grid
            {
                Width = MaxWidth,
                Margin = new Thickness(0, 0, 0, 12)
            };

            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });
            }

            return grid;
        }

        protected TextBlock CreateObjectName(string name)
        {
            return new TextBlock
            {
                Text = name,
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily(FontUri, "./#Linux Biolinum")
            };
        }

        protected void DoMeasure(FrameworkElement element)
        {
            element.Measure(new Size(MaxWidth, MaxHeight));
            element.Arrange(new Rect(element.DesiredSize));
        }
    }
}
