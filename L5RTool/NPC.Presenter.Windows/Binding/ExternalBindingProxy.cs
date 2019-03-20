using System.Windows;

namespace NPC.Presenter.Windows.Binding
{
    abstract class ExternalBindingProxy<T> : Freezable
    {
        private static readonly DependencyPropertyKey DataPropertyKey =
            DependencyProperty.RegisterReadOnly("Data",
                                                typeof(T),
                                                typeof(ExternalBindingProxy<T>),
                                                new PropertyMetadata());

        public static readonly DependencyProperty DataProperty = DataPropertyKey.DependencyProperty;

        public T Data
        {
            get => (T)GetValue(DataProperty);
            private set => SetValue(DataPropertyKey, value);
        }

        protected ExternalBindingProxy()
        {
            GetData();
        }

        private void GetData()
        {
            Data = ProxyDataLocator.Resolve<T>(this);
        }
    }
}
