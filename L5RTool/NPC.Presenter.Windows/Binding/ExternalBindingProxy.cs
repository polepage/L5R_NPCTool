using System.Windows;

namespace NPC.Presenter.Windows.Binding
{
    abstract class ExternalBindingProxy : Freezable
    {
        private static readonly DependencyPropertyKey DataPropertyKey =
            DependencyProperty.RegisterReadOnly("Data",
                                                typeof(object),
                                                typeof(ExternalBindingProxy),
                                                new PropertyMetadata());

        public static readonly DependencyProperty DataProperty = DataPropertyKey.DependencyProperty;

        public object Data
        {
            get => GetValue(DataProperty);
            private set => SetValue(DataPropertyKey, value);
        }

        protected ExternalBindingProxy()
        {
            GetData();
        }

        private void GetData()
        {
            Data = ProxyDataLocator.Resolve(this);
        }
    }
}
