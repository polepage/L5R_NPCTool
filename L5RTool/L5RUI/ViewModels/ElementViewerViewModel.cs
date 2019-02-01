using NPC;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace L5RUI.ViewModels
{
    public class ElementViewerViewModel: BindableBase
    {
        IEventAggregator _eventAggregator;

        public ElementViewerViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenElementEvent>().Subscribe(ElementOppened);

            Elements = new ObservableCollection<IElement>();
        }
        
        public IList<IElement> Elements { get; }

        private IElement _selectedElement;
        public IElement SelectedElement
        {
            get => _selectedElement;
            set => SetProperty(ref _selectedElement, value);
        }

        private void ElementOppened(IElement element)
        {
            IElement toShow = Elements.FirstOrDefault(e => e.Equals(element));
            if (toShow == null)
            {
                toShow = element;
                Elements.Add(toShow);
            }

            SelectedElement = toShow;
        }
    }
}
