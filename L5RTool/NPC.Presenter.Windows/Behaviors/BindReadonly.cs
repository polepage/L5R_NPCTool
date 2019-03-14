using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace NPC.Presenter.Windows.Behaviors
{
    class BindReadonly: Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From",
                                        typeof(object),
                                        typeof(BindReadonly),
                                        new PropertyMetadata(OnFromChanged));

        public object From
        {
            get => GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To",
                                        typeof(object),
                                        typeof(BindReadonly));

        public object To
        {
            get => GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        private static void OnFromChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            if (sender is BindReadonly br)
            {
                br.To = br.From;
            }
        }
    }
}
