using NPC.Common;
using NPC.Presenter.GameObjects;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace NPC.Presenter.Windows.Controls
{
    class TemplateModifiersPresenter: Grid
    {
        #region Character Template
        public static readonly DependencyProperty CharacterTemplateProperty =
            DependencyProperty.Register("CharacterTemplate",
                                        typeof(ITemplate),
                                        typeof(TemplateModifiersPresenter),
                                        new PropertyMetadata(OnTemplateChanged));

        public ITemplate CharacterTemplate
        {
            get => (ITemplate)GetValue(CharacterTemplateProperty);
            set => SetValue(CharacterTemplateProperty, value);
        }
        #endregion

        private static void OnTemplateChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TemplateModifiersPresenter templatePresenter)
            {
                templatePresenter.UpdateTemplate(templatePresenter.CharacterTemplate);
            }
        }

        private void UpdateTemplate(ITemplate template)
        {
            Children.Clear();
            RowDefinitions.Clear();

            if (template == null)
            {
                return;
            }
            
            int row = 0;
            if (template.CombatConflictRank != 0 || template.IntrigueConflictRank != 0)
            {
                RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                var rankMods = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify
                };
                rankMods.Inlines.Add(new Run("Conflict Rank: ") { FontWeight = FontWeights.Bold });
                rankMods.Inlines.Add(FormatModifierList(new List<(string name, int mod)>
                {
                    ("Combat", template.CombatConflictRank),
                    ("Intrigue", template.IntrigueConflictRank)
                }));

                SetRow(rankMods, row);
                Children.Add(rankMods);
                row++;
            }

            if (template.Air != 0 || template.Earth != 0 || template.Fire != 0 || template.Water != 0 || template.Void != 0)
            {
                RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

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

                SetRow(ringMods, row);
                Children.Add(ringMods);
                row++;
            }

            if (template.Artisan != 0 || template.Martial != 0 || template.Scholar != 0 || template.Social != 0 || template.Trade != 0)
            {
                RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

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

                SetRow(skillMods, row);
                Children.Add(skillMods);
            }
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
    }
}
