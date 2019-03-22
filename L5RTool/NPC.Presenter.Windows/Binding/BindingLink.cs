using System.Windows;

namespace NPC.Presenter.Windows.Binding
{
    class BindingLink : Freezable
    {
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source",
                                        typeof(object),
                                        typeof(BindingLink),
                                        new PropertyMetadata());

        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target",
                                        typeof(object),
                                        typeof(BindingLink));

        public object Source
        {
            get => GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public object Target
        {
            get => GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new BindingLink();
        }

        private static void OnSourceChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is BindingLink link)
            {
                link.Target = link.Source;
            }
        }
    }
}
