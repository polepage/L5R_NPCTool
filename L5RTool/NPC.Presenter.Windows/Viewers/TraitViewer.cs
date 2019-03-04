using NPC.Presenter.GameObjects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace NPC.Presenter.Windows.Viewers
{
    class TraitViewer: GameObjectViewer
    {
        public TraitViewer(IGameObject gameObject, double maxWidth, double maxHeight)
            : base(gameObject, maxWidth, maxHeight)
        {
        }

        protected override void CreateElements()
        {
            var trait = (ITrait)GameObject.Data;
            var grid = CreateGrid(string.IsNullOrEmpty(trait.Description) ? 2 : 3);

            var name = new TextBlock
            {
                Text = GameObject.Name + " (" + trait.Ring.ToString() + ")",
                FontSize = 17,
                FontWeight = FontWeights.Bold,
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily(FontUri, "./#Linux Biolinum"),
                Margin = new Thickness(0, 0, 0, 3)
            };

            Grid.SetRow(name, 0);
            grid.Children.Add(name);

            string skills = string.Join(", ", trait.SkillGroups.Select(s => s.ToString()));
            string spheres = string.Join(", ", trait.Spheres.Select(s => s.ToString()));
            var types = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 0, 0, 4)
            };
            types.Inlines.Add(new Run("Types: ") { FontWeight = FontWeights.Bold });
            types.Inlines.Add(skills);
            if (!string.IsNullOrEmpty(skills) && !string.IsNullOrEmpty(spheres))
            {
                types.Inlines.Add("; ");
            }
            types.Inlines.Add(new Run(spheres) { FontStyle = FontStyles.Italic });
            Grid.SetRow(types, 1);
            grid.Children.Add(types);

            if (!string.IsNullOrEmpty(trait.Description))
            {
                var description = new TextBlock
                {
                    Text = trait.Description,
                    TextWrapping = TextWrapping.Wrap
                };
                Grid.SetRow(description, 2);
                grid.Children.Add(description);
            }

            AddElement(grid);
        }

        private Grid CreateGrid(int rows)
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
    }
}
