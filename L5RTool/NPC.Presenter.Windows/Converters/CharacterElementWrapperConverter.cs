using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.GameObjects;
using System;
using System.Globalization;
using System.Windows.Data;

namespace NPC.Presenter.Windows.Converters
{
    class CharacterElementWrapperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IGameObject go)
            {
                return new WrappedElement(go, (CharacterElement)parameter);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is WrappedElement w)
            {
                return w.Element;
            }

            return null;
        }

        public class WrappedElement
        {
            public WrappedElement(IGameObject element, CharacterElement type)
            {
                Element = element;
                ElementType = type;
            }

            public IGameObject Element { get; }
            public CharacterElement ElementType { get; }
        }
    }
}
