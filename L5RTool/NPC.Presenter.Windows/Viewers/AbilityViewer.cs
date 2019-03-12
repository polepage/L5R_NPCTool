using NPC.Parser;
using NPC.Parser.Structure;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Extensions;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NPC.Presenter.Windows.Viewers
{
    class AbilityViewer : GameObjectViewer
    {
        public AbilityViewer(IGameObject gameObject, double maxWidth, double maxHeight, IParser parser)
            : base(gameObject, parser, maxWidth, maxHeight)
        {
        }

        protected override void CreateElements()
        {
            var ability = (IAbility)GameObject.Data;

            var parsedContent = Parser.Parse(ability.Content);
            var grid = CreateGrid(parsedContent.Count() + 1);

            int currentRow = 0;

            var name = new TextBlock
            {
                Text = GameObject.Name,
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily(FontUri, "./#Linux Biolinum")
            };
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

            AddElement(grid);
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
