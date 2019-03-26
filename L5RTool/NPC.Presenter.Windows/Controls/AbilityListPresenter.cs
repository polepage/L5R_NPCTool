using NPC.Parser;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Print;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NPC.Presenter.Windows.Controls
{
    class AbilityListPresenter: Grid
    {
        #region GameObject
        public static readonly DependencyProperty AbilitiesProperty =
            DependencyProperty.Register("Abilities",
                                        typeof(IEnumerable<IAbility>),
                                        typeof(AbilityListPresenter),
                                        new PropertyMetadata(OnAbilitiesChanged));

        public IEnumerable<IAbility> Abilities
        {
            get => (IEnumerable<IAbility>)GetValue(AbilitiesProperty);
            set => SetValue(AbilitiesProperty, value);
        }
        #endregion

        #region Parser
        public static readonly DependencyProperty ParserProperty =
            DependencyProperty.Register("Parser",
                                        typeof(IParser),
                                        typeof(AbilityListPresenter));

        public IParser Parser
        {
            get => (IParser)GetValue(ParserProperty);
            set => SetValue(ParserProperty, value);
        }
        #endregion

        private AbilityPrinter _abilityPrinter;
        private AbilityPrinter AbilityPrinter => _abilityPrinter ?? (_abilityPrinter = new AbilityPrinter(ActualWidth, double.PositiveInfinity, Parser));

        private static void OnAbilitiesChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is AbilityListPresenter abilityPresenter)
            {
                if (e.OldValue is INotifyCollectionChanged oldCollection)
                {
                    oldCollection.CollectionChanged -= abilityPresenter.OnAbilityCollectionChanged;
                }

                if (e.NewValue is INotifyCollectionChanged newCollection)
                {
                    newCollection.CollectionChanged += abilityPresenter.OnAbilityCollectionChanged;
                }
            }
        }

        private void OnAbilityCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateAbilities();
        }

        private void UpdateAbilities()
        {
            Children.Clear();
            RowDefinitions.Clear();

            foreach (var visual in Abilities.OrderBy(a => a.Name).SelectMany(a => AbilityPrinter.CreatePrintView(a)))
            {
                RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                SetRow(visual, RowDefinitions.Count - 1);
                Children.Add(visual);
            }
        }
    }
}
