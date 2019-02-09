using L5RUI.Events;
using L5RUI.Extensions;
using L5RUI.ViewModels.Elements;
using NPC;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace L5RUI.ViewModels
{
    public class ElementViewerViewModel: BindableBase
    {
        IEventAggregator _eventAggregator;
        IElementStorage _elementStorage;

        public ElementViewerViewModel(IEventAggregator eventAggregator, IElementStorage elementStorage)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenNewElementEvent>().Subscribe(e => ElementOppened(e, true));
            _eventAggregator.GetEvent<OpenElementEvent>().Subscribe(e => ElementOppened(e, false));
            _eventAggregator.GetEvent<SaveCurrentElementEvent>().Subscribe(Save);
            _eventAggregator.GetEvent<SaveAllElementsEvent>().Subscribe(SaveAll);

            _elementStorage = elementStorage;

            Elements = new ObservableCollection<IElementViewModel>();
        }
        
        public IList<IElementViewModel> Elements { get; }

        private IElementViewModel _selectedElement;
        public IElementViewModel SelectedElement
        {
            get => _selectedElement;
            set => SetProperty(ref _selectedElement, value);
        }

        private DelegateCommand _closeCommand;
        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(Close));

        private DelegateCommand _closeAllCommand;
        public ICommand CloseAllCommand => _closeAllCommand ?? (_closeAllCommand = new DelegateCommand(CloseAll));

        private DelegateCommand _closeOthersCommand;
        public ICommand CloseOthersCommand => _closeOthersCommand ?? (_closeOthersCommand = new DelegateCommand(CloseOthers));

        private DelegateCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));

        private void ElementOppened(IElement element, bool asNew)
        {
            IElementViewModel toShow = Elements.FirstOrDefault(e => e.Element.Equals(element));
            if (toShow == null)
            {
                toShow = element.CreateViewModel();
                toShow.IsDirty = asNew;
                Elements.Add(toShow);
            }

            SelectedElement = toShow;
        }

        private void Close()
        {

        }

        private void CloseAll()
        {

        }

        private void CloseOthers()
        {

        }

        private void Save()
        {
            if (SelectedElement.IsDirty)
            {
                _elementStorage.Save(SelectedElement.Element);
            }
        }

        private void SaveAll()
        {
            if (Elements.Any(e => e.IsDirty))
            {
                _elementStorage.Save(Elements.Where(e => e.IsDirty).Select(e => e.Element));
            }
        }
    }
}
