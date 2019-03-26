using CS.Utils;
using NPC.Common;
using NPC.Presenter.GameObjects;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace NPC.Presenter.Windows.Print
{
    class TemplatePrinter : BaseGameObjectPrinter
    {
        public TemplatePrinter(double maxWidth, double maxHeight)
            : base(maxWidth, maxHeight)
        {
        }

        public IEnumerable<FrameworkElement> CreatePrintView(ITemplate template)
        {
            bool hasRankModifier = template.CombatConflictRank != 0 || template.IntrigueConflictRank != 0;
            bool hasRingModifier = template.Air != 0 || template.Earth != 0 || template.Fire != 0 || template.Water != 0 || template.Void != 0;
            bool hasSkillModifier = template.Artisan != 0 || template.Martial != 0 || template.Scholar != 0 || template.Social != 0 || template.Trade != 0;
            bool hasAdvantageModifier = template.SuggestedAdvantages.Any() && template.AdvantageRemplacements != 0;
            bool hasDisadvantageModifier = template.SuggestedDisadvantages.Any() && template.DisadvantageRemplacements != 0;
            bool hasAbilityModifier = template.AbilityTypes.Any() && template.AbilityAdditions != 0;
            bool hasDemeanorModifier = template.SuggestedDemeanors.Any();

            var grid = CreateGrid(BoolHelpers.CountTrue(hasRankModifier, hasRingModifier, hasSkillModifier, hasAdvantageModifier,
                                                        hasDisadvantageModifier, hasAbilityModifier, hasDemeanorModifier) + 1);

            var name = CreateObjectName(template.Name.Trim());
            Grid.SetRow(name, 0);
            grid.Children.Add(name);

            int row = 1;
            if (hasRankModifier)
            {
                var rankMods = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(0, 3, 0, 0)
                };
                rankMods.Inlines.Add(new Run("Conflict Rank: ") { FontWeight = FontWeights.Bold });
                rankMods.Inlines.Add(FormatModifierList(new List<(string name, int mod)>
                {
                    ("Combat", template.CombatConflictRank),
                    ("Intrigue", template.IntrigueConflictRank)
                }));

                Grid.SetRow(rankMods, row);
                grid.Children.Add(rankMods);
                row++;
            }

            if (hasRingModifier)
            {
                var ringMods = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(0, 3, 0, 0)
                };
                ringMods.Inlines.Add(new Run("Rings: ") { FontWeight = FontWeights.Bold });
                ringMods.Inlines.Add(FormatModifierList(new List<(string name, int mod)>
                {
                    (Ring.Air.ToString(), template.Air),
                    (Ring.Earth.ToString(), template.Earth),
                    (Ring.Fire.ToString(), template.Fire),
                    (Ring.Water.ToString(), template.Water),
                    (Ring.Void.ToString(), template.Void),
                }));

                Grid.SetRow(ringMods, row);
                grid.Children.Add(ringMods);
                row++;
            }

            if (hasSkillModifier)
            {
                var skillMods = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(0, 3, 0, 0)
                };
                skillMods.Inlines.Add(new Run("Skills: ") { FontWeight = FontWeights.Bold });
                skillMods.Inlines.Add(FormatModifierList(new List<(string name, int mod)>
                {
                    (SkillGroup.Artisan.ToString(), template.Artisan),
                    (SkillGroup.Martial.ToString(), template.Martial),
                    (SkillGroup.Scholar.ToString(), template.Scholar),
                    (SkillGroup.Social.ToString(), template.Social),
                    (SkillGroup.Trade.ToString(), template.Trade),
                }));

                Grid.SetRow(skillMods, row);
                grid.Children.Add(skillMods);
                row++;
            }

            if (hasAdvantageModifier)
            {
                var advantageMods = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(0, 3, 0, 0)
                };
                advantageMods.Inlines.Add(new Run("Advantages (add/replace 0-" + template.AdvantageRemplacements + "): ") { FontWeight = FontWeights.Bold });
                foreach (var inline in FormatTraitList(template.SuggestedAdvantages))
                {
                    advantageMods.Inlines.Add(inline);
                }

                Grid.SetRow(advantageMods, row);
                grid.Children.Add(advantageMods);
                row++;
            }

            if (hasDisadvantageModifier)
            {
                var disadvantageMods = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(0, 3, 0, 0)
                };
                disadvantageMods.Inlines.Add(new Run("Disadvantages (add/replace 0-" + template.DisadvantageRemplacements + "): ") { FontWeight = FontWeights.Bold });
                foreach (var inline in FormatTraitList(template.SuggestedDisadvantages))
                {
                    disadvantageMods.Inlines.Add(inline);
                }

                Grid.SetRow(disadvantageMods, row);
                grid.Children.Add(disadvantageMods);
                row++;
            }

            if (hasAbilityModifier)
            {
                var abilityMods = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(0, 3, 0, 0)
                };
                abilityMods.Inlines.Add(new Run("Abilities (add 0-" + template.AbilityAdditions + "): ") { FontWeight = FontWeights.Bold });
                abilityMods.Inlines.Add(string.Join(", ", template.AbilityTypes.Select(at => at.ToString())));

                Grid.SetRow(abilityMods, row);
                grid.Children.Add(abilityMods);
                row++;
            }

            if (hasDemeanorModifier)
            {
                var demeanorMods = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(0, 3, 0, 0)
                };
                demeanorMods.Inlines.Add(new Run("Demeanor (replace 1): ") { FontWeight = FontWeights.Bold });
                demeanorMods.Inlines.Add(string.Join(", ", template.SuggestedDemeanors.Select(d => d.Name.Trim()).OrderBy(s => s)));

                Grid.SetRow(demeanorMods, row);
                grid.Children.Add(demeanorMods);
                row++;
            }

            return new List<FrameworkElement> { grid };
        }

        private string FormatModifierList(List<(string name, int mod)> list)
        {
            list.Sort((t1, t2) =>
            {
                int returnValue = t2.mod.CompareTo(t1.mod);
                if (returnValue == 0)
                {
                    returnValue = t1.name.CompareTo(t2.name);
                }

                return returnValue;
            });

            return string.Join(", ", list.Where(t => t.mod != 0).Select(t => GetModifierString(t.name, t.mod)));
        }

        private string GetModifierString(string name, int modifier)
        {
            return name + " " + (modifier > 0 ? "+" : "") + modifier.ToString();
        }

        private IEnumerable<Inline> FormatTraitList(IEnumerable<ITrait> traits)
        {
            var inlines = new List<Inline>();
            foreach (var trait in traits)
            {
                bool hasSkills = trait.SkillGroups.Any();
                bool hasSpheres = trait.Spheres.Any();

                string main = trait.Name.Trim() + " (" + trait.Ring.ToString() + ")";
                if (hasSkills || hasSpheres)
                {
                    main += " [";
                }

                if (hasSkills)
                {
                    main += string.Join(", ", trait.SkillGroups.Select(sg => sg.ToString()));
                    if (hasSpheres)
                    {
                        main += "; ";
                    }
                    else
                    {
                        main += "]";
                    }
                }

                inlines.Add(new Run(main));

                if (hasSpheres)
                {
                    inlines.Add(new Run(string.Join(", ", trait.Spheres.Select(s => s.ToString()))) { FontStyle = FontStyles.Italic });
                    inlines.Add(new Run("]"));
                }

                inlines.Add(new Run(", "));
            }

            inlines.Remove(inlines.Last());
            return inlines;
        }
    }
}
