using System.Windows;
using System.Windows.Controls;

namespace NPC.Presenter.Windows.Behaviors
{
    class GridTools
    {
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.RegisterAttached("Rows",
                                                typeof(string),
                                                typeof(GridTools),
                                                new PropertyMetadata(OnRowsChanged));

        public static string GetRows(DependencyObject d)
        {
            return (string)d.GetValue(RowsProperty);
        }

        public static void SetRows(DependencyObject d, object value)
        {
            d.SetValue(RowsProperty, value);
        }

        private static void OnRowsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Grid grid)
            {
                grid.RowDefinitions.Clear();
                if (e.NewValue is string rows)
                {
                    string[] split = rows.Split(';');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (split[i] == string.Empty)
                        {
                            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        }
                        else if (split[i] == "*")
                        {
                            grid.RowDefinitions.Add(new RowDefinition());
                        }
                        else
                        {
                            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(double.Parse(split[i])) });
                        }
                    }
                }
            }
        }

        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.RegisterAttached("Columns",
                                                typeof(string),
                                                typeof(GridTools),
                                                new PropertyMetadata(OnColumnsChanged));

        public static string GetColumns(DependencyObject d)
        {
            return (string)d.GetValue(ColumnsProperty);
        }

        public static void SetColumns(DependencyObject d, object value)
        {
            d.SetValue(ColumnsProperty, value);
        }

        private static void OnColumnsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Grid grid)
            {
                grid.ColumnDefinitions.Clear();
                if (e.NewValue is string columns)
                {
                    string[] split = columns.Split(';');
                    for (int i = 0; i < split.Length; i++)
                    {
                        if (split[i] == string.Empty)
                        {
                            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        }
                        else if (split[i] == "*")
                        {
                            grid.ColumnDefinitions.Add(new ColumnDefinition());
                        }
                        else
                        {
                            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(double.Parse(split[i])) });
                        }
                    }
                }
            }
        }

        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.RegisterAttached("Position",
                                                typeof(string),
                                                typeof(GridTools),
                                                new PropertyMetadata(OnPositionChanged));

        public static string GetPosition(DependencyObject d)
        {
            return (string)d.GetValue(PositionProperty);
        }

        public static void SetPosition(DependencyObject d, object value)
        {
            d.SetValue(PositionProperty, value);
        }

        private static void OnPositionChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is UIElement element)
            {
                if (e.NewValue is string position)
                {
                    string[] positions = position.Split(';');

                    int row = string.IsNullOrEmpty(positions[0]) ? 0 : int.Parse(positions[0]);
                    int column = string.IsNullOrEmpty(positions[1]) ? 0 : int.Parse(positions[1]);

                    Grid.SetRow(element, row);
                    Grid.SetColumn(element, column);
                }
            }
        }
    }
}
