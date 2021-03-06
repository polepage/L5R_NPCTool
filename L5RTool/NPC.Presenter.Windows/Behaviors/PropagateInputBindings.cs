﻿using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace NPC.Presenter.Windows.Behaviors
{
    class PropagateInputBindings: Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject == null)
            {
                return;
            }

            AssociatedObject.Loaded += OnObjectLoaded;
        }

        private void OnObjectLoaded(object sender, RoutedEventArgs e)
        {
            AssociatedObject.Loaded -= OnObjectLoaded;

            var window = Window.GetWindow(AssociatedObject);
            for (int i = AssociatedObject.InputBindings.Count - 1; i >= 0; i--)
            {
                var inputBinding = AssociatedObject.InputBindings[i];
                window.InputBindings.Add(inputBinding);
                AssociatedObject.InputBindings.Remove(inputBinding);
            }
        }
    }
}
