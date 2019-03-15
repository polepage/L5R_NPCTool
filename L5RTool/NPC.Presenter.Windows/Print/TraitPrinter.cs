using NPC.Presenter.GameObjects;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace NPC.Presenter.Windows.Print
{
    class TraitPrinter : BaseGameObjectPrinter
    {
        public TraitPrinter(double maxWidth, double maxHeight)
            : base(maxWidth, maxHeight)
        {
        }

        public IEnumerable<FrameworkElement> CreatePrintView(ITrait trait)
        {
            var grid = CreateGrid(string.IsNullOrWhiteSpace(trait.Description) ? 2 : 3);

            var name = CreateObjectName(trait.Name + " (" + trait.Ring.ToString() + ")");
            Grid.SetRow(name, 0);
            grid.Children.Add(name);

            string skills = string.Join(", ", trait.SkillGroups.Select(s => s.ToString()));
            string spheres = string.Join(", ", trait.Spheres.Select(s => s.ToString()));
            var types = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(0, 3, 0, 0)
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

            if (!string.IsNullOrWhiteSpace(trait.Description))
            {
                var description = new TextBlock
                {
                    Text = trait.Description.Trim(),
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(0, 4, 0, 0)
                };
                Grid.SetRow(description, 2);
                grid.Children.Add(description);
            }

            DoMeasure(grid);
            return new List<FrameworkElement> { grid };
        }
    }
}
