using NPC.Presenter.GameObjects;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace NPC.Presenter.Windows.Controls
{
    class ShortTraitTextBlock : TextBlock
    {
        public static readonly DependencyProperty TraitProperty =
            DependencyProperty.Register("Trait",
                                        typeof(ITrait),
                                        typeof(ShortTraitTextBlock),
                                        new PropertyMetadata(OnTraitChanged));

        public ITrait Trait
        {
            get => (ITrait)GetValue(TraitProperty);
            set => SetValue(TraitProperty, value);
        }

        private static void OnTraitChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ShortTraitTextBlock block)
            {
                block.Inlines.Clear();

                if (block.Trait == null)
                {
                    return;
                }

                bool hasSkills = block.Trait.SkillGroups.Any();
                bool hasSpheres = block.Trait.Spheres.Any();

                string main = block.Trait.Name.Trim() + " (" + block.Trait.Ring.ToString() + ")";
                if (hasSkills || hasSpheres)
                {
                    main += " [";
                }

                if (hasSkills)
                {
                    main += string.Join(", ", block.Trait.SkillGroups.Select(sg => sg.ToString()));
                    if (hasSpheres)
                    {
                        main += "; ";
                    }
                    else
                    {
                        main += "]";
                    }
                }

                block.Inlines.Add(new Run(main));

                if (hasSpheres)
                {
                    block.Inlines.Add(new Run(string.Join(", ", block.Trait.Spheres.Select(s => s.ToString()))) { FontStyle = FontStyles.Italic });
                    block.Inlines.Add(new Run("]"));
                }
            }
        }
    }
}
