using NPC.Presenter.GameObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace NPC.Presenter.Windows.Viewers
{
    abstract class GameObjectViewer : IEnumerable<FrameworkElement>
    {
        protected static readonly Uri FontUri = new Uri("pack://application:,,,/NPC.Presenter.Windows;component/Fonts/");

        private List<FrameworkElement> _divisions;

        public GameObjectViewer(IGameObject gameObject, double maxWidth, double maxHeight)
        {
            _divisions = new List<FrameworkElement>();

            GameObject = gameObject;
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;

            CreateElements();
        }

        protected IGameObject GameObject { get; }

        protected double MaxWidth { get; }
        protected double MaxHeight { get; }

        public IEnumerator<FrameworkElement> GetEnumerator()
        {
            return _divisions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected abstract void CreateElements();

        protected void AddElement(FrameworkElement element)
        {
            DoMesure(element);
            _divisions.Add(element);
        }

        private void DoMesure(FrameworkElement element)
        {
            element.Measure(new Size(MaxWidth, MaxHeight));
            element.Arrange(new Rect(element.DesiredSize));
        }
    }
}
