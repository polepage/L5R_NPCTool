using NPC.Parser;
using NPC.Parser.Structure;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NPC.Presenter.Windows.Print
{
    class AbilityPrinter : BaseGameObjectPrinter
    {
        private IParser _parser;

        public AbilityPrinter(double maxWidth, double maxHeight, IParser parser)
            : base(maxWidth, maxHeight)
        {
            _parser = parser;
        }

        public IEnumerable<FrameworkElement> CreatePrintView(IAbility ability)
        {
            var parsedContent = _parser.Parse(ability.Content);
            var grid = CreateGrid(parsedContent.Count() + 1);

            int currentRow = 0;
            var name = CreateObjectName(ability.Name.Trim());
            Grid.SetRow(name, currentRow);
            grid.Children.Add(name);
            currentRow++;

            foreach (var block in parsedContent)
            {
                var blockContent = CreateBlock(block);
                Grid.SetRow(blockContent, currentRow);
                grid.Children.Add(blockContent);
                currentRow++;
            }

            DoMeasure(grid);
            return new List<FrameworkElement> { grid };
        }

        private Grid CreateBlock(BlockElement block)
        {
            var grid = new Grid
            {
                Width = MaxWidth,
                Margin = new Thickness(0, 4, 0, 0)
            };

            for (int i = 0; i < block.Indentation; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(10.0)
                });
            }
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < block.Indentation; i++)
            {
                var rect = new Rectangle
                {
                    Fill = Brushes.Black,
                    StrokeThickness = 0,
                    Width = 1,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                Grid.SetColumn(rect, i);
                grid.Children.Add(rect);
            }

            var content = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Justify
            };

            foreach (var inline in block.Elements)
            {
                content.Inlines.Add(inline.GetWindowsInline());
            }

            if (content.Inlines.Last() is Run run)
            {
                run.Text = run.Text.Trim();
            }

            Grid.SetColumn(content, block.Indentation);
            grid.Children.Add(content);

            return grid;
        }
    }
}
