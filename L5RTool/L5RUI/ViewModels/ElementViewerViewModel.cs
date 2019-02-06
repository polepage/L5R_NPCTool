using L5RUI.Extensions;
using L5RUI.ViewModels.Elements;
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

            Elements = new ObservableCollection<IElementViewModel>();
        }
        
        public IList<IElementViewModel> Elements { get; }

        private IElementViewModel _selectedElement;
        public IElementViewModel SelectedElement
        {
            get => _selectedElement;
            set => SetProperty(ref _selectedElement, value);
        }

        private void ElementOppened(IElement element)
        {
            IElementViewModel toShow = Elements.FirstOrDefault(e => e.Element.Equals(element));
            if (toShow == null)
            {
                toShow = element.CreateViewModel();
                Elements.Add(toShow);
            }

            SelectedElement = toShow;
        }
    }
}
