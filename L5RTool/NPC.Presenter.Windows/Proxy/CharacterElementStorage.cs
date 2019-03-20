using NPC.Presenter.Windows.Binding;
using NPC.Presenter.Windows.GameObjects;
using NPC.Presenter.Windows.Proxy.Data;
using System.Windows;

namespace NPC.Presenter.Windows.Proxy
{
    class CharacterElementStorage : ExternalBindingProxy<CharacterElementStorageData>
    {
        public static readonly DependencyProperty ElementTypeProperty =
            DependencyProperty.Register("ElementType",
                                        typeof(CharacterElement),
                                        typeof(CharacterElementStorage),
                                        new PropertyMetadata(OnElementTypeChanged));

        public CharacterElement ElementType
        {
            get => (CharacterElement)GetValue(ElementTypeProperty);
            set => SetValue(ElementTypeProperty, value);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new CharacterElementStorage();
        }

        private static void OnElementTypeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is CharacterElementStorage elementStorage)
            {
                elementStorage.Data.ElementType = elementStorage.ElementType;
            }
        }
    }
}
