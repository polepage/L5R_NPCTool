using CS.Utils;
using Microsoft.Xaml.Behaviors;
using NPC.Common;
using NPC.Parser;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Print;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace NPC.Presenter.Windows.Behaviors
{
    class GameObjectPrintPreview: Behavior<DocumentViewer>
    {
        #region Refresh
        private static readonly DependencyPropertyKey RefreshCommandPropertyKey =
            DependencyProperty.RegisterReadOnly("RefreshCommand",
                                                typeof(ICommand),
                                                typeof(GameObjectPrintPreview),
                                                new PropertyMetadata());

        public static readonly DependencyProperty RefreshCommandProperty = RefreshCommandPropertyKey.DependencyProperty;

        public ICommand RefreshCommand
        {
            get => (ICommand)GetValue(RefreshCommandProperty);
            private set => SetValue(RefreshCommandPropertyKey, value);
        }
        #endregion

        #region GameObjects
        public static readonly DependencyProperty GameObjectsProperty =
            DependencyProperty.Register("GameObjects",
                                        typeof(IEnumerable<IGameObject>),
                                        typeof(GameObjectPrintPreview));

        public IEnumerable<IGameObject> GameObjects
        {
            get => (IEnumerable<IGameObject>)GetValue(GameObjectsProperty);
            set => SetValue(GameObjectsProperty, value);
        }
        #endregion

        #region Parser
        public static readonly DependencyProperty ParserProperty =
            DependencyProperty.Register("Parser",
                                        typeof(IParser),
                                        typeof(GameObjectPrintPreview));

        public IParser Parser
        {
            get => (IParser)GetValue(ParserProperty);
            set => SetValue(ParserProperty, value);
        }
        #endregion

        private readonly double _pageHeight = 10 * 96;
        private readonly double _verticalMargin = 0.5 * 96;

        private readonly double _columnWidth = 3.5 * 96;
        private readonly double _horizontalMargin = 0.5 * 96;
        private readonly double _columnSpacing = 0.5 * 96;
        private readonly int _columnCount = 2;

        private AbilityPrinter _abilityPrinter;
        private AbilityPrinter AbilityPrinter => _abilityPrinter ?? (_abilityPrinter = new AbilityPrinter(_columnWidth, _pageHeight, Parser));

        private DemeanorPrinter _demeanorPrinter;
        private DemeanorPrinter DemeanorPrinter => _demeanorPrinter ?? (_demeanorPrinter = new DemeanorPrinter(_columnWidth, _pageHeight));

        private GearPrinter _gearPrinter;
        private GearPrinter GearPrinter => _gearPrinter ?? (_gearPrinter = new GearPrinter(_columnWidth, _pageHeight));

        private TraitPrinter _traitPrinter;
        private TraitPrinter TraitPrinter => _traitPrinter ?? (_traitPrinter = new TraitPrinter(_columnWidth, _pageHeight));

        private CharacterPrinter _characterPrinter;
        private CharacterPrinter CharacterPrinter => _characterPrinter ?? (_characterPrinter = new CharacterPrinter(_columnWidth, _pageHeight, Parser));

        private TemplatePrinter _templatePrinter;
        private TemplatePrinter TemplatePrinter => _templatePrinter ?? (_templatePrinter = new TemplatePrinter(_columnWidth, _pageHeight));

        protected override void OnAttached()
        {
            base.OnAttached();

            RefreshCommand = new DelegateCommand(Refresh);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            RefreshCommand = null;
        }

        private void Refresh()
        {
            if (AssociatedObject == null || GameObjects == null)
            {
                return;
            }

            var document = new FixedDocument();
            var currentPage = new FixedPage();

            int currentColumn = 0;
            double currentVerticalPosition = _verticalMargin;
            double currentHorizontalPosition = _horizontalMargin;

            foreach (ObjectType type in EnumHelpers.GetValues<ObjectType>())
            {
                var elements = GameObjects.Where(go => go.Type == type).Select(go => CreateViewer(go));
                if (!elements.Any())
                {
                    continue;
                }

                FrameworkElement title = CreateTitle(type);
                AddTitleToDocument(document, ref currentPage, title, elements.First().First(), ref currentHorizontalPosition, ref currentVerticalPosition, ref currentColumn);

                foreach (FrameworkElement element in elements.SelectMany(s => s))
                {
                    AddToDocument(document, ref currentPage, element, ref currentHorizontalPosition, ref currentVerticalPosition, ref currentColumn);
                }
            }

            AddPage(document, currentPage);
            AssociatedObject.Document = document;
        }

        private void AddTitleToDocument(FixedDocument document, ref FixedPage page, FrameworkElement title, FrameworkElement first,
                                        ref double horizontalPosition, ref double verticalPosition, ref int column)
        {
            if (CalculatePosition(title.ActualHeight + title.Margin.Bottom + title.Margin.Top + first.ActualHeight + first.Margin.Bottom + first.Margin.Top,
                                  ref horizontalPosition, ref verticalPosition, ref column))
            {
                AddPage(document, page);
                page = new FixedPage();
            }
            AddToPage(page, title, horizontalPosition, ref verticalPosition);
        }

        private void AddToDocument(FixedDocument document, ref FixedPage page, FrameworkElement element,
                                   ref double horizontalPosition, ref double verticalPosition, ref int column)
        {
            if (CalculatePosition(element.ActualHeight + element.Margin.Top + element.Margin.Bottom, ref horizontalPosition, ref verticalPosition, ref column))
            {
                AddPage(document, page);
                page = new FixedPage();
            }
            AddToPage(page, element, horizontalPosition, ref verticalPosition);
        }

        private bool CalculatePosition(double elementHeight, ref double horizontalPosition, ref double verticalPosition, ref int column)
        {
            if (elementHeight > (_verticalMargin + _pageHeight - verticalPosition))
            {
                column++;
                if (column < _columnCount)
                {
                    horizontalPosition = _horizontalMargin + (column * (_columnWidth + _columnSpacing));
                    verticalPosition = _verticalMargin;
                }
                else
                {
                    column = 0;
                    horizontalPosition = _horizontalMargin;
                    verticalPosition = _verticalMargin;
                    return true;
                }
            }

            return false;
        }

        private void AddToPage(FixedPage page, FrameworkElement element, double horizontalPosition, ref double verticalPosition)
        {
            FixedPage.SetLeft(element, horizontalPosition);
            FixedPage.SetTop(element, verticalPosition);
            page.Children.Add(element);

            verticalPosition += element.ActualHeight + element.Margin.Bottom + element.Margin.Top;
        }

        private void AddPage(FixedDocument document, FixedPage page)
        {
            document.Pages.Add(new PageContent
            {
                Child = page
            });
        }

        private FrameworkElement CreateTitle(ObjectType type)
        {
            var title = new TextBlock
            {
                Text = type.ToString(),
                FontSize = 36,
                FontFamily = new FontFamily(new Uri("pack://application:,,,/NPC.Presenter.Windows;component/Fonts/"), "./#Ignite the Light"),
                Margin = new Thickness(0, 0, 0, 7)
            };

            title.Measure(new Size(_columnWidth, _pageHeight));
            title.Arrange(new Rect(title.DesiredSize));

            return title;
        }

        private IEnumerable<FrameworkElement> CreateViewer(IGameObject gameObject)
        {
            switch (gameObject)
            {
                case ICharacter c:
                    return CharacterPrinter.CreatePrintPreview(c);
                case IDemeanor d:
                    return DemeanorPrinter.CreatePrintView(d);
                case ITrait t:
                    return TraitPrinter.CreatePrintView(t);
                case IAbility a:
                    return AbilityPrinter.CreatePrintView(a);
                case IGear g:
                    return GearPrinter.CreatePrintView(g);
                case ITemplate t:
                    return TemplatePrinter.CreatePrintView(t);
                default:
                    throw new ArgumentException("Unkown object type.");
            }
        }
    }
}
