using NPC.Common;
using NPC.Parser;
using NPC.Parser.Structure;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NPC.Presenter.Windows.Print
{
    class CharacterPrinter : BaseGameObjectPrinter
    {
        private IParser _parser;

        public CharacterPrinter(double maxWidth, double maxHeight, IParser parser)
            : base(maxWidth, maxHeight)
        {
            _parser = parser;
        }

        public IEnumerable<FrameworkElement> CreatePrintPreview(ICharacter character)
        {
            var elements = new List<FrameworkElement>();

            elements.Add(CreateTitlePreview(character));
            if (character.HasSocietalValues)
            {
                elements.Add(CreateNormalStatBlock(character));
            }
            else
            {
                elements.Add(CreateBeastStatBlock(character));
            }

            if (character.Advantages.Any() || character.Disadvantages.Any())
            {
                elements.Add(CreateTraitsBlock(character.Advantages, character.Disadvantages));
            }
            
            if (character.FavoredWeapons.Any() || character.EquippedGear.Any() || character.OtherGear.Any())
            {
                elements.Add(CreateGearBlock(character.FavoredWeapons, character.EquippedGear, character.OtherGear));
            }
            
            if (character.Abilities.Any())
            {
                elements.Add(CreateAbilityBlock(character.Abilities));
            }

            return elements;
        }

        private FrameworkElement CreateTitlePreview(ICharacter character)
        {
            var grid = CreateGrid(string.IsNullOrWhiteSpace(character.Description) ? 2 : 3);

            var name = CreateObjectName(character.Name);
            Grid.SetRow(name, 0);
            grid.Children.Add(name);

            var statGrid = CreateGrid(1, 4);
            statGrid.ColumnDefinitions.Add(new ColumnDefinition());
            statGrid.ColumnDefinitions.Add(new ColumnDefinition());

            var type = new TextBlock
            {
                Text = character.CharacterType.ToString().ToUpper(),
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            Grid.SetColumn(type, 0);
            statGrid.Children.Add(type);

            var ranks = new TextBlock
            {
                FontSize = 12,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            ranks.Inlines.Add(new Run("CONFLICT RANK:   "));
            ranks.Inlines.Add(new InlineUIContainer(new Image
            {
                Width = 16,
                Height = 16,
                Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Conflict/Military.png"))
            }));
            ranks.Inlines.Add(new Run($" {character.CombatConflictRank}  ") { FontWeight = FontWeights.Bold });
            ranks.Inlines.Add(new InlineUIContainer(new Image
            {
                Width = 16,
                Height = 16,
                Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Conflict/Intrigue.png"))
            }));
            ranks.Inlines.Add(new Run($" {character.IntrigueConflictRank} ") { FontWeight = FontWeights.Bold });
            Grid.SetColumn(ranks, 1);
            statGrid.Children.Add(ranks);

            var line = new Rectangle
            {
                StrokeThickness = 0,
                Fill = Brushes.LightGray,
                Height = 1,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            Grid.SetColumnSpan(line, 2);
            statGrid.Children.Add(line);

            Grid.SetRow(statGrid, 1);
            grid.Children.Add(statGrid);

            if (!string.IsNullOrWhiteSpace(character.Description))
            {
                var description = new TextBlock
                {
                    Text = character.Description.Trim(),
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(0, 4, 0, 0)
                };
                Grid.SetRow(description, 2);
                grid.Children.Add(description);
            }

            DoMeasure(grid);
            return grid;
        }

        private FrameworkElement CreateNormalStatBlock(ICharacter character)
        {
            var grid = new Grid
            {
                Margin = new Thickness(0, 0, 0, 4),
                Width = MaxWidth
            };
            Grid.SetIsSharedSizeScope(grid, true);
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(14) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(28) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(28) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(28) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto, SharedSizeGroup = "Numbers" });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto, SharedSizeGroup = "Text" });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto, SharedSizeGroup = "Text" });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto, SharedSizeGroup = "Numbers" });

            CreateSocietalStatBlock(grid, character);
            CreateNormalPersonalStatBlock(grid, character);
            CreateNormalRingBlock(grid, character);
            CreateSkillBlock(grid, character, 5, 5);
            
            DoMeasure(grid);
            return grid;
        }

        private FrameworkElement CreateBeastStatBlock(ICharacter character)
        {
            var grid = new Grid
            {
                Margin = new Thickness(0, 0, 0, 4),
                Width = MaxWidth
            };
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(28) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(28) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(40) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            CreateBeastPersonalStatBlock(grid, character);
            CreateBeastRingBlock(grid, character);
            CreateSkillBlock(grid, character, 3, 5);

            DoMeasure(grid);
            return grid;
        }

        private void CreateSocietalStatBlock(Grid grid, ICharacter character)
        {
            var background = new Rectangle
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                StrokeThickness = 0,
                Fill = Brushes.LightGray
            };
            Grid.SetRow(background, 0);
            Grid.SetColumn(background, 0);
            Grid.SetColumnSpan(background, 2);
            grid.Children.Add(background);

            var title = new TextBlock
            {
                Text = "SOCIETAL",
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                FontFamily = new FontFamily(FontUri, "./#Linux Biolinum"),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(6, 1.5, 1.5, 1.5)
            };
            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 0);
            Grid.SetColumnSpan(title, 2);
            grid.Children.Add(title);

            var sideline = new Rectangle
            {
                Width = 1,
                StrokeThickness = 0,
                Fill = Brushes.LightGray,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            Grid.SetRow(sideline, 1);
            Grid.SetRowSpan(sideline, 4);
            Grid.SetColumn(sideline, 0);
            grid.Children.Add(sideline);

            CreateStatElement(grid, 1, 1, 0, "HONOR", FormatSocietal(character.Honor), HorizontalAlignment.Left);
            CreateStatElement(grid, 2, 1, 0, "GLORY", FormatSocietal(character.Glory), HorizontalAlignment.Left);
            CreateStatElement(grid, 3, 1, 0, "STATUS", FormatSocietal(character.Status), HorizontalAlignment.Left);

            var demeanor = CreateDemeanorBlock(character.Demeanor, HorizontalAlignment.Left);
            Grid.SetRow(demeanor, 4);
            Grid.SetColumnSpan(demeanor, 3);
            grid.Children.Add(demeanor);
        }

        private void CreateNormalPersonalStatBlock(Grid grid, ICharacter character)
        {
            var background = new Rectangle
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                StrokeThickness = 0,
                Fill = Brushes.LightGray
            };
            Grid.SetRow(background, 0);
            Grid.SetColumn(background, 3);
            Grid.SetColumnSpan(background, 2);
            grid.Children.Add(background);

            var title = new TextBlock
            {
                Text = "PERSONAL",
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                FontFamily = new FontFamily(FontUri, "./#Linux Biolinum"),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(1.5, 1.5, 6, 1.5)
            };
            Grid.SetRow(title, 0);
            Grid.SetColumn(title, 3);
            Grid.SetColumnSpan(title, 2);
            grid.Children.Add(title);

            var sideline = new Rectangle
            {
                Width = 1,
                StrokeThickness = 0,
                Fill = Brushes.LightGray,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            Grid.SetRow(sideline, 1);
            Grid.SetRowSpan(sideline, 4);
            Grid.SetColumn(sideline, 4);
            grid.Children.Add(sideline);

            CreateStatElement(grid, 1, 3, 4, "ENDURANCE", character.Endurance.ToString(), HorizontalAlignment.Right);
            CreateStatElement(grid, 2, 3, 4, "COMPOSURE", FormatComposure(character.Composure), HorizontalAlignment.Right);
            CreateStatElement(grid, 3, 3, 4, "FOCUS", character.Focus.ToString(), HorizontalAlignment.Right);
            CreateStatElement(grid, 4, 3, 4, "VIGILANCE", character.Vigilance.ToString(), HorizontalAlignment.Right);
        }

        private void CreateBeastPersonalStatBlock(Grid grid, ICharacter character)
        {
            var topline = new Rectangle
            {
                Height = 1,
                StrokeThickness = 0,
                Fill = Brushes.LightGray,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top
            };
            Grid.SetRow(topline, 0);
            Grid.SetColumn(topline, 1);
            Grid.SetColumnSpan(topline, 4);
            grid.Children.Add(topline);

            var midline = new Rectangle
            {
                Width = 1,
                StrokeThickness = 0,
                Fill = Brushes.LightGray,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            Grid.SetRow(midline, 0);
            Grid.SetRowSpan(midline, 2);
            Grid.SetColumn(midline, 2);
            grid.Children.Add(midline);

            var sideline = new Rectangle
            {
                Width = 1,
                StrokeThickness = 0,
                Fill = Brushes.LightGray,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Stretch
            };
            Grid.SetRow(sideline, 0);
            Grid.SetRowSpan(sideline, 3);
            Grid.SetColumn(sideline, 4);
            grid.Children.Add(sideline);

            CreateStatElement(grid, 0, 1, 2, "ENDURANCE", character.Endurance.ToString(), HorizontalAlignment.Right);
            CreateStatElement(grid, 1, 1, 2, "COMPOSURE", FormatComposure(character.Composure), HorizontalAlignment.Right);
            CreateStatElement(grid, 0, 3, 4, "FOCUS", character.Focus.ToString(), HorizontalAlignment.Right);
            CreateStatElement(grid, 1, 3, 4, "VIGILANCE", character.Vigilance.ToString(), HorizontalAlignment.Right);

            var demeanor = CreateDemeanorBlock(character.Demeanor, HorizontalAlignment.Right);
            Grid.SetRow(demeanor, 2);
            Grid.SetColumn(demeanor, 1);
            Grid.SetColumnSpan(demeanor, 4);
            grid.Children.Add(demeanor);
        }

        private void CreateStatElement(Grid grid, int row, int titleColumn, int valueColumn, string text, string value, HorizontalAlignment alignement)
        {
            var title = new TextBlock
            {
                Text = text,
                FontSize = 7,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = alignement,
                Margin = new Thickness(6)
            };
            Grid.SetRow(title, row);
            Grid.SetColumn(title, titleColumn);
            grid.Children.Add(title);

            var number = new TextBlock
            {
                Text = value,
                FontSize = 15,
                FontWeight = FontWeights.Bold,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = alignement,
                Margin = new Thickness(4, 2, 4, 2)
            };
            Grid.SetRow(number, row);
            Grid.SetColumn(number, valueColumn);
            grid.Children.Add(number);
        }

        private FrameworkElement CreateDemeanorBlock(IDemeanor demeanor, HorizontalAlignment alignement)
        {
            var mods = new List<(Ring ring, int mod)>
            {
                (Ring.Air, demeanor.Air),
                (Ring.Earth, demeanor.Earth),
                (Ring.Fire, demeanor.Fire),
                (Ring.Water, demeanor.Water),
                (Ring.Void, demeanor.Void)
            };

            mods.Sort((t1, t2) =>
            {
                int returnValue = t2.mod.CompareTo(t1.mod);
                if (returnValue == 0)
                {
                    returnValue = t1.ring.CompareTo(t2.ring);
                }

                return returnValue;
            });

            var grid = new Grid
            {
                HorizontalAlignment = alignement
            };
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var modifiers = new TextBlock
            {
                HorizontalAlignment = alignement,
                Margin = new Thickness(6, 6, 6, 2)
            };

            foreach (var (ring, mod) in mods.Where(t => t.mod != 0))
            {
                modifiers.Inlines.Add(new InlineUIContainer(new Image
                {
                    Width = 14,
                    Height = 14,
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Rings/" + ring.ToString() + @".png"))
                }));
                modifiers.Inlines.Add(new Run(" " + FormatModifier(mod) + ",") { FontWeight = FontWeights.Bold, FontSize = 12 });
            }
            string lastString = (modifiers.Inlines.LastInline as Run).Text;
            (modifiers.Inlines.LastInline as Run).Text = lastString.Substring(0, lastString.Length - 1);
            grid.Children.Add(modifiers);

            var title = new TextBlock
            {
                Text = demeanor.Name.Trim().ToUpper(),
                FontSize = 7,
                HorizontalAlignment = alignement,
                Margin = new Thickness(6, 0, 6, 6)
            };
            Grid.SetRow(title, 1);
            grid.Children.Add(title);

            return grid;
        }

        private void CreateNormalRingBlock(Grid grid, ICharacter character)
        {
            var rings = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };
            rings.RowDefinitions.Add(new RowDefinition());
            rings.RowDefinitions.Add(new RowDefinition());
            rings.RowDefinitions.Add(new RowDefinition());
            rings.ColumnDefinitions.Add(new ColumnDefinition());
            rings.ColumnDefinitions.Add(new ColumnDefinition());

            var earthRing = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 44,
                Height = 44,
                Margin = new Thickness(0, 0, 8, 0),
                Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Rings/" + Ring.Earth.ToString() + @".png"))
            };
            Grid.SetRow(earthRing, 0);
            Grid.SetColumn(earthRing, 0);
            rings.Children.Add(earthRing);

            var earthCircle = new Ellipse
            {
                Fill = Brushes.White,
                Stroke = Brushes.Gray,
                Width = 24,
                Height = 24,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            Grid.SetRow(earthCircle, 0);
            Grid.SetColumn(earthCircle, 0);
            rings.Children.Add(earthCircle);

            var earth = new TextBlock
            {
                Text = character.Earth.ToString(),
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 7, 2)
            };
            Grid.SetRow(earth, 0);
            Grid.SetColumn(earth, 0);
            rings.Children.Add(earth);

            var airRing = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 44,
                Height = 44,
                Margin = new Thickness(8, 0, 0, 0),
                Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Rings/" + Ring.Air.ToString() + @".png"))
            };
            Grid.SetRow(airRing, 0);
            Grid.SetColumn(airRing, 1);
            rings.Children.Add(airRing);

            var airCircle = new Ellipse
            {
                Fill = Brushes.White,
                Stroke = Brushes.Gray,
                Width = 24,
                Height = 24,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(38, 0, 0, 0)
            };
            Grid.SetRow(airCircle, 0);
            Grid.SetColumn(airCircle, 1);
            rings.Children.Add(airCircle);

            var air = new TextBlock
            {
                Text = character.Air.ToString(),
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(46, 0, 0, 2)
            };
            Grid.SetRow(air, 0);
            Grid.SetColumn(air, 1);
            rings.Children.Add(air);

            var waterRing = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 44,
                Height = 44,
                Margin = new Thickness(0, 0, 20, 0),
                Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Rings/" + Ring.Water.ToString() + @".png"))
            };
            Grid.SetRow(waterRing, 1);
            Grid.SetColumn(waterRing, 0);
            rings.Children.Add(waterRing);

            var waterCircle = new Ellipse
            {
                Fill = Brushes.White,
                Stroke = Brushes.Gray,
                Width = 24,
                Height = 24,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 12, 0)
            };
            Grid.SetRow(waterCircle, 1);
            Grid.SetColumn(waterCircle, 0);
            rings.Children.Add(waterCircle);

            var water = new TextBlock
            {
                Text = character.Water.ToString(),
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(0, 0, 19, 2)
            };
            Grid.SetRow(water, 1);
            Grid.SetColumn(water, 0);
            rings.Children.Add(water);

            var fireRing = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 44,
                Height = 44,
                Margin = new Thickness(20, 0, 0, 0),
                Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Rings/" + Ring.Fire.ToString() + @".png"))
            };
            Grid.SetRow(fireRing, 1);
            Grid.SetColumn(fireRing, 1);
            rings.Children.Add(fireRing);

            var fireCircle = new Ellipse
            {
                Fill = Brushes.White,
                Stroke = Brushes.Gray,
                Width = 24,
                Height = 24,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(48, 0, 0, 0)
            };
            Grid.SetRow(fireCircle, 1);
            Grid.SetColumn(fireCircle, 1);
            rings.Children.Add(fireCircle);

            var fire = new TextBlock
            {
                Text = character.Fire.ToString(),
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(56, 0, 0, 2)
            };
            Grid.SetRow(fire, 1);
            Grid.SetColumn(fire, 1);
            rings.Children.Add(fire);

            var voidRing = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 44,
                Height = 44,
                Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Rings/" + Ring.Void.ToString() + @".png"))
            };
            Grid.SetRow(voidRing, 2);
            Grid.SetColumn(voidRing, 0);
            Grid.SetColumnSpan(voidRing, 2);
            rings.Children.Add(voidRing);

            var voidCircle = new Ellipse
            {
                Fill = Brushes.White,
                Stroke = Brushes.Gray,
                Width = 24,
                Height = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(36, 0, 0, 0)
            };
            Grid.SetRow(voidCircle, 2);
            Grid.SetColumn(voidCircle, 0);
            Grid.SetColumnSpan(voidCircle, 2);
            rings.Children.Add(voidCircle);

            var voidValue = new TextBlock
            {
                Text = character.Void.ToString(),
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(38, 0, 0, 2)
            };
            Grid.SetRow(voidValue, 2);
            Grid.SetColumn(voidValue, 0);
            Grid.SetColumnSpan(voidValue, 2);
            rings.Children.Add(voidValue);


            Grid.SetRow(rings, 0);
            Grid.SetRowSpan(rings, 5);
            Grid.SetColumn(rings, 2);
            grid.Children.Add(rings);
        }

        private void CreateBeastRingBlock(Grid grid, ICharacter character)
        {
            var rings = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Margin = new Thickness(4)
            };
            rings.RowDefinitions.Add(new RowDefinition());
            rings.RowDefinitions.Add(new RowDefinition());
            rings.ColumnDefinitions.Add(new ColumnDefinition());
            rings.ColumnDefinitions.Add(new ColumnDefinition());
            rings.ColumnDefinitions.Add(new ColumnDefinition());

            var earthRing = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 44,
                Height = 44,
                Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Rings/" + Ring.Earth.ToString() + @".png"))
            };
            Grid.SetRow(earthRing, 0);
            Grid.SetColumn(earthRing, 0);
            Grid.SetColumnSpan(earthRing, 2);
            rings.Children.Add(earthRing);

            var earthCircle = new Ellipse
            {
                Fill = Brushes.White,
                Stroke = Brushes.Gray,
                Width = 24,
                Height = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(36, 0, 0, 0)
            };
            Grid.SetRow(earthCircle, 0);
            Grid.SetColumn(earthCircle, 0);
            Grid.SetColumnSpan(earthCircle, 2);
            rings.Children.Add(earthCircle);

            var earth = new TextBlock
            {
                Text = character.Earth.ToString(),
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(38, 0, 0, 2)
            };
            Grid.SetRow(earth, 0);
            Grid.SetColumn(earth, 0);
            Grid.SetColumnSpan(earth, 2);
            rings.Children.Add(earth);

            var airRing = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 44,
                Height = 44,
                Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Rings/" + Ring.Air.ToString() + @".png"))
            };
            Grid.SetRow(airRing, 0);
            Grid.SetColumn(airRing, 1);
            Grid.SetColumnSpan(airRing, 2);
            rings.Children.Add(airRing);

            var airCircle = new Ellipse
            {
                Fill = Brushes.White,
                Stroke = Brushes.Gray,
                Width = 24,
                Height = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(36, 0, 0, 0)
            };
            Grid.SetRow(airCircle, 0);
            Grid.SetColumn(airCircle, 1);
            Grid.SetColumnSpan(airCircle, 2);
            rings.Children.Add(airCircle);

            var air = new TextBlock
            {
                Text = character.Air.ToString(),
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(38, 0, 0, 2)
            };
            Grid.SetRow(air, 0);
            Grid.SetColumn(air, 1);
            Grid.SetColumnSpan(air, 2);
            rings.Children.Add(air);

            var waterRing = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 44,
                Height = 44,
                Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Rings/" + Ring.Water.ToString() + @".png"))
            };
            Grid.SetRow(waterRing, 1);
            Grid.SetColumn(waterRing, 0);
            rings.Children.Add(waterRing);

            var waterCircle = new Ellipse
            {
                Fill = Brushes.White,
                Stroke = Brushes.Gray,
                Width = 24,
                Height = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(36, 0, 0, 0)
            };
            Grid.SetRow(waterCircle, 1);
            Grid.SetColumn(waterCircle, 0);
            rings.Children.Add(waterCircle);

            var water = new TextBlock
            {
                Text = character.Water.ToString(),
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(38, 0, 0, 2)
            };
            Grid.SetRow(water, 1);
            Grid.SetColumn(water, 0);
            rings.Children.Add(water);

            var fireRing = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 44,
                Height = 44,
                Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Rings/" + Ring.Fire.ToString() + @".png"))
            };
            Grid.SetRow(fireRing, 1);
            Grid.SetColumn(fireRing, 2);
            rings.Children.Add(fireRing);

            var fireCircle = new Ellipse
            {
                Fill = Brushes.White,
                Stroke = Brushes.Gray,
                Width = 24,
                Height = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(36, 0, 0, 0)
            };
            Grid.SetRow(fireCircle, 1);
            Grid.SetColumn(fireCircle, 2);
            rings.Children.Add(fireCircle);

            var fire = new TextBlock
            {
                Text = character.Fire.ToString(),
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(38, 0, 0, 2)
            };
            Grid.SetRow(fire, 1);
            Grid.SetColumn(fire, 2);
            rings.Children.Add(fire);

            var voidRing = new Image
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 44,
                Height = 44,
                Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Rings/" + Ring.Void.ToString() + @".png"))
            };
            Grid.SetRow(voidRing, 1);
            Grid.SetColumn(voidRing, 1);
            rings.Children.Add(voidRing);

            var voidCircle = new Ellipse
            {
                Fill = Brushes.White,
                Stroke = Brushes.Gray,
                Width = 24,
                Height = 24,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(36, 0, 0, 0)
            };
            Grid.SetRow(voidCircle, 1);
            Grid.SetColumn(voidCircle, 1);
            rings.Children.Add(voidCircle);

            var voidValue = new TextBlock
            {
                Text = character.Void.ToString(),
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new Thickness(38, 0, 0, 2)
            };
            Grid.SetRow(voidValue, 1);
            Grid.SetColumn(voidValue, 1);
            rings.Children.Add(voidValue);


            Grid.SetRow(rings, 0);
            Grid.SetRowSpan(rings, 3);
            Grid.SetColumn(rings, 0);
            grid.Children.Add(rings);
        }

        private void CreateSkillBlock(Grid grid, ICharacter character, int row, int columnSpan)
        {
            var skills = CreateGrid(1, 4);
            skills.ColumnDefinitions.Add(new ColumnDefinition());
            skills.ColumnDefinitions.Add(new ColumnDefinition());
            skills.ColumnDefinitions.Add(new ColumnDefinition());
            skills.ColumnDefinitions.Add(new ColumnDefinition());
            skills.ColumnDefinitions.Add(new ColumnDefinition());

            CreateSkillElement(skills, 0, SkillGroup.Artisan.ToString(), character.Artisan);
            CreateSkillElement(skills, 1, SkillGroup.Martial.ToString(), character.Martial);
            CreateSkillElement(skills, 2, SkillGroup.Scholar.ToString(), character.Scholar);
            CreateSkillElement(skills, 3, SkillGroup.Social.ToString(), character.Social);
            CreateSkillElement(skills, 4, SkillGroup.Trade.ToString(), character.Trade);

            Grid.SetRow(skills, row);
            Grid.SetColumn(skills, 0);
            Grid.SetColumnSpan(skills, columnSpan);
            grid.Children.Add(skills);
        }

        private void CreateSkillElement(Grid grid, int column, string skill, int value)
        {
            bool hasSkill = value > 0;

            var background = new Rectangle
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Fill = hasSkill ? Brushes.LightGray : Brushes.Transparent,
                Stroke = Brushes.LightGray
            };
            Grid.SetColumn(background, column);
            grid.Children.Add(background);

            var skillValue = new TextBlock
            {
                Text = skill + " " + value.ToString(),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Bold,
                Foreground = hasSkill ? Brushes.Black : Brushes.Gray,
                Margin = new Thickness(0, 3, 0, 3)
            };
            Grid.SetColumn(skillValue, column);
            grid.Children.Add(skillValue);
        }

        private FrameworkElement CreateTraitsBlock(IEnumerable<IAdvantage> advantages, IEnumerable<IDisadvantage> disadvantages)
        {
            var grid = CreateGrid(0);
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(14) });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            var background = new Rectangle
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                StrokeThickness = 0,
                Fill = Brushes.LightGray
            };
            Grid.SetRow(background, 0);
            Grid.SetColumn(background, 0);
            Grid.SetColumnSpan(background, 2);
            grid.Children.Add(background);

            var advantagesTitle = new TextBlock
            {
                Text = "ADVANTAGES",
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                FontFamily = new FontFamily(FontUri, "./#Linux Biolinum"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(1.5, 1.5, 6, 1.5)
            };
            Grid.SetRow(advantagesTitle, 0);
            Grid.SetColumn(advantagesTitle, 0);
            grid.Children.Add(advantagesTitle);

            var disadvantagesTitle = new TextBlock
            {
                Text = "DISADVANTAGES",
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                FontFamily = new FontFamily(FontUri, "./#Linux Biolinum"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(1.5, 1.5, 6, 1.5)
            };
            Grid.SetRow(disadvantagesTitle, 0);
            Grid.SetColumn(disadvantagesTitle, 1);
            grid.Children.Add(disadvantagesTitle);

            var midline = new Rectangle
            {
                Width = 1,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Right,
                StrokeThickness = 0,
                Fill = Brushes.LightGray
            };
            Grid.SetRow(midline, 1);
            Grid.SetColumn(midline, 0);
            grid.Children.Add(midline);

            CreateTraitsList(grid, 0, advantages);
            CreateTraitsList(grid, 1, disadvantages);

            DoMeasure(grid);
            return grid;
        }

        private void CreateTraitsList(Grid grid, int column, IEnumerable<ITrait> traits)
        {
            var listGrid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(3)
            };

            int row = 0;
            foreach (var trait in traits)
            {
                listGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                var traitText = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 4, 0, 0)
                };
                traitText.Inlines.Add(new Run(trait.Name + ":" + Environment.NewLine) { FontWeight = FontWeights.Bold });
                traitText.Inlines.Add(new InlineUIContainer(new Image
                {
                    Width = 14,
                    Height = 14,
                    Source = new BitmapImage(new Uri(@"pack://application:,,,/NPC.Presenter.Windows;component/Icons/Rings/" + trait.Ring.ToString() + @".png"))
                }));

                string skills = string.Join(", ", trait.SkillGroups.Select(s => s.ToString()));
                string spheres = string.Join(", ", trait.Spheres.Select(s => s.ToString()));

                traitText.Inlines.Add(new Run(" " + skills));
                if (!string.IsNullOrEmpty(skills) && !string.IsNullOrEmpty(spheres))
                {
                    traitText.Inlines.Add(new Run("; "));
                }
                traitText.Inlines.Add(new Run(spheres) { FontStyle = FontStyles.Italic });

                Grid.SetRow(traitText, row);
                row++;
                listGrid.Children.Add(traitText);
            }

            Grid.SetRow(listGrid, 1);
            Grid.SetColumn(listGrid, column);
            grid.Children.Add(listGrid);
        }

        private FrameworkElement CreateGearBlock(IEnumerable<IGear> weapons, IEnumerable<IGear> equipped, IEnumerable<IGear> other)
        {
            var grid = CreateGrid(0);
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(14) });

            var background = new Rectangle
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                StrokeThickness = 0,
                Fill = Brushes.LightGray
            };
            Grid.SetRow(background, 0);
            grid.Children.Add(background);

            var title = new TextBlock
            {
                Text = "WEAPONS & GEAR",
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                FontFamily = new FontFamily(FontUri, "./#Linux Biolinum"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(1.5, 1.5, 6, 1.5)
            };
            Grid.SetRow(title, 0);
            grid.Children.Add(title);

            int lastRow = weapons.Count() + (equipped.Any() ? 1 : 0) + (other.Any() ? 1 : 0);

            int row = 1;
            foreach (var weapon in weapons)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                var weaponText = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(3)
                };
                weaponText.Inlines.Add(new Run(weapon.Name.Trim() + ": ") { FontWeight = FontWeights.Bold });
                weaponText.Inlines.Add(new Run(weapon.Description.Trim()));

                Grid.SetRow(weaponText, row);
                grid.Children.Add(weaponText);

                if (row != lastRow)
                {
                    var line = new Rectangle
                    {
                        Height = 1,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        StrokeThickness = 0,
                        Fill = Brushes.LightGray
                    };
                    Grid.SetRow(line, row);
                    grid.Children.Add(line);
                }

                row++;
            }

            IEnumerable<string> ListGear(IEnumerable<IGear> gear)
            {
                return gear.Select(g =>
                {
                    if (g.GearType == GearType.Armor)
                    {
                        return g.Name.Trim() + " (" + g.Description.Trim() + ")";
                    }

                    return g.Name.Trim();
                });
            }

            if (equipped.Any())
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                var gearText = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(3)
                };
                gearText.Inlines.Add(new Run("Equipped Gear: ") { FontWeight = FontWeights.Bold });
                gearText.Inlines.Add(new Run(string.Join(", ", ListGear(equipped))));

                Grid.SetRow(gearText, row);
                grid.Children.Add(gearText);

                if (row != lastRow)
                {
                    var line = new Rectangle
                    {
                        Height = 1,
                        HorizontalAlignment = HorizontalAlignment.Stretch,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        StrokeThickness = 0,
                        Fill = Brushes.LightGray
                    };
                    Grid.SetRow(line, row);
                    grid.Children.Add(line);
                }

                row++;
            }

            if (other.Any())
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                var gearText = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Justify,
                    Margin = new Thickness(3)
                };
                gearText.Inlines.Add(new Run("Other Gear: ") { FontWeight = FontWeights.Bold });
                gearText.Inlines.Add(new Run(string.Join(", ", ListGear(other))));

                Grid.SetRow(gearText, row);
                grid.Children.Add(gearText);
            }

            DoMeasure(grid);
            return grid;
        }

        private FrameworkElement CreateAbilityBlock(IEnumerable<IAbility> abilities)
        {
            var grid = CreateGrid(0);
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(14) });

            var background = new Rectangle
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                StrokeThickness = 0,
                Fill = Brushes.LightGray
            };
            Grid.SetRow(background, 0);
            grid.Children.Add(background);

            var title = new TextBlock
            {
                Text = "ABILITIES",
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                FontFamily = new FontFamily(FontUri, "./#Linux Biolinum"),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(1.5, 1.5, 6, 1.5)
            };
            Grid.SetRow(title, 0);
            grid.Children.Add(title);

            int row = 1;
            foreach (var ability in abilities)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                var name = new TextBlock
                {
                    Text = ability.Name.Trim().ToUpper(),
                    FontSize = 12,
                    FontWeight = FontWeights.Bold,
                    TextWrapping = TextWrapping.Wrap,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(3, 1.5, 3, 0)
                };
                Grid.SetRow(name, row);
                grid.Children.Add(name);

                var line = new Rectangle
                {
                    Height = 1,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Fill = Brushes.LightGray,
                    StrokeThickness = 0
                };
                Grid.SetRow(line, row);
                grid.Children.Add(line);

                row++;

                var parsedContent = _parser.Parse(ability.Content);
                foreach (var block in parsedContent)
                {
                    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                    var blockContent = CreateBlock(block);
                    Grid.SetRow(blockContent, row);
                    grid.Children.Add(blockContent);

                    row++;
                }
            }

            DoMeasure(grid);
            return grid;
        }

        private FrameworkElement CreateBlock(BlockElement block)
        {
            var grid = new Grid
            {
                Width = MaxWidth,
                Margin = new Thickness(0, 4, 0, 0)
            };

            for (int i = 0; i < block.Indentation; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(10.0)
                });
            }
            grid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < block.Indentation; i++)
            {
                var rect = new Rectangle
                {
                    Fill = Brushes.Black,
                    StrokeThickness = 0,
                    Width = 1,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Left
                };
                Grid.SetColumn(rect, i);
                grid.Children.Add(rect);
            }

            var content = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Justify
            };

            foreach (var inline in block.Elements)
            {
                content.Inlines.Add(inline.GetWindowsInline());
            }

            if (content.Inlines.Last() is Run run)
            {
                run.Text = run.Text.Trim();
            }

            Grid.SetColumn(content, block.Indentation);
            grid.Children.Add(content);

            return grid;
        }

        private string FormatComposure(int composure)
        {
            if (composure > 0)
            {
                return composure.ToString();
            }

            return "∞";
        }

        private string FormatSocietal(int value)
        {
            return value.ToString("D2");
        }

        private string FormatModifier(int modifier)
        {
            return (modifier > 0 ? "+" : "") + modifier.ToString();
        }
    }
}
