using CS.Utils.Collections;
using NPC.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace NPC.Presenter.Windows.Converters
{
    abstract class SetWrapperConverter<T> : IValueConverter where T: IComparable
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ISet<T> set)
            {
                return new SetWrapper<T>(set, (t1, t2) =>
                {
                    return t1.CompareTo(t2);
                });
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    // There is no generic in xaml
    class SkillGroupSetConverter: SetWrapperConverter<SkillGroup> { }
    class TraitSphereSetConverter : SetWrapperConverter<TraitSphere> { }
    class AbilityTypeSetConverter : SetWrapperConverter<AbilityType> { }
}
