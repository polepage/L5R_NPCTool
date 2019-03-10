using System.Windows;

namespace NPC.Presenter.Windows.Helpers
{
    static class TreeHelper
    {
        public static T FindLogicalAncestor<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = LogicalTreeHelper.GetParent(dependencyObject);
            if (parent == null)
            {
                return null;
            }

            if (parent is T parentT)
            {
                return parentT;
            }

            return FindLogicalAncestor<T>(parent);
        }
    }
}
