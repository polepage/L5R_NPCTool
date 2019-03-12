using NPC.Presenter.GameObjects;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NPC.Presenter.Windows.Viewers
{
    class GearViewer : GameObjectViewer
    {
        public GearViewer(IGameObject gameObject, double maxWidth, double maxHeight)
            : base(gameObject, maxWidth, maxHeight)
        {
        }

        protected override void CreateElements()
        {
            var gear = (IGear)GameObject.Data;
            var grid = CreateGrid(string.IsNullOrEmpty(gear.Description) ? 1 : 2);

            var name = new TextBlock
            {
                Text = GameObject.Name + " (" + gear.GearType.ToString() + ")",
                FontSize = 20,
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily(FontUri, "./#Linux Biolinum")
            };
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

            AddElement(grid);
        }
    }
}
