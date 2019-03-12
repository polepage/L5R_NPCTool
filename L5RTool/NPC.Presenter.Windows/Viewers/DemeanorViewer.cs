using CS.Utils;
using NPC.Common;
using NPC.Presenter.GameObjects;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace NPC.Presenter.Windows.Viewers
{
    class DemeanorViewer : GameObjectViewer
    {
        public DemeanorViewer(IGameObject gameObject, double maxWidth, double maxHeight)
            : base(gameObject, maxWidth, maxHeight)
        {
        }

        protected override void CreateElements()
        {
            var demeanor = (IDemeanor)GameObject.Data;

            bool hasModifier = demeanor.Air != 0 || demeanor.Earth != 0 || demeanor.Fire != 0 || demeanor.Water != 0 || demeanor.Void != 0;
            bool hasUnmasking = !string.IsNullOrWhiteSpace(demeanor.Unmasking);
            bool hasDescription = !string.IsNullOrWhiteSpace(demeanor.Description);

            var grid = CreateGrid(BoolHelpers.CountTrue(hasModifier, hasUnmasking, hasDescription) + 1);

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

            if (hasDescription)
            {
                var description = new TextBlock
                {
                    Text = demeanor.Description.Trim(),
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(0, 4, 0, 0)
                };
                Grid.SetRow(description, currentRow);
                grid.Children.Add(description);
                currentRow++;
            }

            if (hasModifier)
            {
                var mods = new List<(Ring ring, int mod)>();
                mods.Add((Ring.Air, demeanor.Air));
                mods.Add((Ring.Earth, demeanor.Earth));
                mods.Add((Ring.Fire, demeanor.Fire));
                mods.Add((Ring.Water, demeanor.Water));
                mods.Add((Ring.Void, demeanor.Void));

                mods.Sort((t1, t2) =>
                {
                    int returnValue = t2.mod.CompareTo(t1.mod);
                    if (returnValue == 0)
                    {
                        returnValue = t1.ring.CompareTo(t2.ring);
                    }

                    return returnValue;
                });

                string modText = string.Join(", ", mods.Where(t => t.mod != 0).Select(t => GetModifierString(t.ring, t.mod)));
                var modifiers = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(0, 3, 0, 0)
                };
                modifiers.Inlines.Add(new Run("Social Check TN Modifers: ") { FontWeight = FontWeights.Bold });
                modifiers.Inlines.Add(modText);

                Grid.SetRow(modifiers, currentRow);
                grid.Children.Add(modifiers);
                currentRow++;
            }

            if (hasUnmasking)
            {
                var unmasking = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 3, 0, 0)
                };
                unmasking.Inlines.Add(new Run("Common Unmasking: ") { FontWeight = FontWeights.Bold });
                unmasking.Inlines.Add(demeanor.Unmasking);

                Grid.SetRow(unmasking, currentRow);
                grid.Children.Add(unmasking);
            }

            AddElement(grid);
        }

        private string GetModifierString(Ring ring, int modifier)
        {
            return ring.ToString() + " " + (modifier > 0 ? "+" : "") + modifier.ToString();
        }
    }
}
