using NPC.Presenter.GameObjects;
using System.Windows;
using System.Windows.Controls;

namespace NPC.Presenter.Windows.Templates
{
    class TreeViewTemplateSelector: DataTemplateSelector
    {
        public DataTemplate GroupTemplate { get; set; }
        public DataTemplate GameObjectTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ObjectMetadataGroup)
            {
                return GroupTemplate;
            }

            return GameObjectTemplate;
        }
    }
}
