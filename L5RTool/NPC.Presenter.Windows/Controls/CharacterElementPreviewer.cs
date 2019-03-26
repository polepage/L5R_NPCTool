using NPC.Parser;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Print;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace NPC.Presenter.Windows.Controls
{
    class CharacterElementPreviewer: Grid
    {
        #region GameObject
        public static readonly DependencyProperty GameObjectProperty =
            DependencyProperty.Register("GameObject",
                                        typeof(IGameObject),
                                        typeof(CharacterElementPreviewer),
                                        new PropertyMetadata(OnGameObjectChanged));

        public IGameObject GameObject
        {
            get => (IGameObject)GetValue(GameObjectProperty);
            set => SetValue(GameObjectProperty, value);
        }
        #endregion

        #region Parser
        public static readonly DependencyProperty ParserProperty =
            DependencyProperty.Register("Parser",
                                        typeof(IParser),
                                        typeof(CharacterElementPreviewer));

        public IParser Parser
        {
            get => (IParser)GetValue(ParserProperty);
            set => SetValue(ParserProperty, value);
        }
        #endregion

        private AbilityPrinter _abilityPrinter;
        private AbilityPrinter AbilityPrinter => _abilityPrinter ?? (_abilityPrinter = new AbilityPrinter(ActualWidth, double.PositiveInfinity, Parser));

        private DemeanorPrinter _demeanorPrinter;
        private DemeanorPrinter DemeanorPrinter => _demeanorPrinter ?? (_demeanorPrinter = new DemeanorPrinter(ActualWidth, double.PositiveInfinity));

        private GearPrinter _gearPrinter;
        private GearPrinter GearPrinter => _gearPrinter ?? (_gearPrinter = new GearPrinter(ActualWidth, double.PositiveInfinity));

        private TraitPrinter _traitPrinter;
        private TraitPrinter TraitPrinter => _traitPrinter ?? (_traitPrinter = new TraitPrinter(ActualWidth, double.PositiveInfinity));

        private TemplatePrinter _templatePrinter;
        public TemplatePrinter TemplatePrinter => _templatePrinter ?? (_templatePrinter = new TemplatePrinter(ActualWidth, double.PositiveInfinity));

        private static void OnGameObjectChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as CharacterElementPreviewer)?.UpdatePreview();
        }

        private void UpdatePreview()
        {
            Children.Clear();

            if (GameObject == null)
            {
                return;
            }

            var parts = GetPreview();
            SetupGrid(parts.Count());

            int currentRow = 0;
            foreach (var part in parts)
            {
                SetRow(part, currentRow);
                currentRow++;
                Children.Add(part);
            }

            Measure(new Size(ActualWidth, double.PositiveInfinity));
            Arrange(new Rect(DesiredSize));
        }

        private IEnumerable<FrameworkElement> GetPreview()
        {
            switch (GameObject)
            {
                case IDemeanor d:
                    return DemeanorPrinter.CreatePrintView(d);
                case ITrait t:
                    return TraitPrinter.CreatePrintView(t);
                case IGear g:
                    return GearPrinter.CreatePrintView(g);
                case IAbility a:
                    return AbilityPrinter.CreatePrintView(a);
                case ITemplate t:
                    return TemplatePrinter.CreatePrintView(t);
                default:
                    return null;
            }
        }

        private void SetupGrid(int rows)
        {
            RowDefinitions.Clear();
            for (int i = 0; i < rows; i++)
            {
                RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });
            }
        }
    }
}
