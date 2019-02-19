using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class GameObjectEditorViewModel: BindableBase
    {
        IEventAggregator _eventAggregator;
        Business.IStorage _storage;

        public GameObjectEditorViewModel(IEventAggregator eventAggregator, Business.IStorage storage)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenGameObjectEvent>().Subscribe(GameObjectOpened);
            _eventAggregator.GetEvent<SaveCurrentGameObjectEvent>().Subscribe(Save);
            _eventAggregator.GetEvent<SaveAllGameObjectsEvent>().Subscribe(SaveAll);

            _storage = storage;

            GameObjects = new ObservableCollection<IGameObject>();
        }
        
        public IList<IGameObject> GameObjects { get; }

        private IGameObject _selectedObject;
        public IGameObject SelectedObject
        {
            get => _selectedObject;
            set => SetProperty(ref _selectedObject, value);
        }

        private DelegateCommand _closeCommand;
        public ICommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(Close));

        private DelegateCommand _closeAllCommand;
        public ICommand CloseAllCommand => _closeAllCommand ?? (_closeAllCommand = new DelegateCommand(CloseAll));

        private DelegateCommand _closeOthersCommand;
        public ICommand CloseOthersCommand => _closeOthersCommand ?? (_closeOthersCommand = new DelegateCommand(CloseOthers));

        private DelegateCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(Save));

        private void GameObjectOpened(IGameObject gameObject)
        {
            IGameObject toShow = GameObjects.FirstOrDefault(e => e.Equals(gameObject));
            if (toShow == null)
            {
                GameObjects.Add(gameObject);
                toShow = gameObject;
            }

            SelectedObject = toShow;
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
            if (SelectedObject.IsDirty)
            {
                if (SelectedObject is GameObject go)
                {
                    _storage.Save(go.Source);
                }
            }
        }

        private void SaveAll()
        {
            if (GameObjects.Any(e => e.IsDirty))
            {
                _storage.Save(GameObjects.Where(o => o.IsDirty).OfType<GameObject>().Select(s => s.Source));
            }
        }
    }
}
