using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Events;
using NPC.Presenter.Windows.Interaction;
using NPC.Presenter.Windows.Interaction.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
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
            _eventAggregator.GetEvent<CloseCurrentGameObjectEvent>().Subscribe(Close);
            _eventAggregator.GetEvent<CloseAllGameObjectsEvent>().Subscribe(CloseAll);
            _eventAggregator.GetEvent<CancellableCloseAllGameObjectsEvent>().Subscribe(CancellableCloseAll);

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
            if (ValidateDirtyFiles(new List<IGameObject> { SelectedObject }))
            {
                GameObjects.Remove(SelectedObject);                
            }
        }

        private void CloseAll()
        {
            CancellableCloseAll(null);
        }

        private void CancellableCloseAll(Action cancel)
        {
            if (ValidateDirtyFiles(GameObjects))
            {
                GameObjects.Clear();
            }
            else
            {
                cancel?.Invoke();
            }
        }

        private void CloseOthers()
        {
            if (ValidateDirtyFiles(GameObjects.Where(go => !go.Equals(SelectedObject))))
            {
                var toRemove = GameObjects.Where(go => !go.Equals(SelectedObject)).ToList();
                foreach (IGameObject go in toRemove)
                {
                    GameObjects.Remove(go);
                }
            }
        }

        private bool ValidateDirtyFiles(IEnumerable<IGameObject> gameObjects)
        {
            if (!gameObjects.Where(go => go.IsDirty).Any())
            {
                return true;
            }

            var confirmation = new SaveConfirmation
            {
                Title = "Unsaved Changes",
                Content = gameObjects.Where(go => go.IsDirty).ToList(),
                Selector = go => SelectedObject = go
            };

            InteractionRequests.SaveRequest.Raise(confirmation);
            if (confirmation.Value)
            {
                _storage.Save(GameObjects.Where(o => o.IsDirty).OfType<GameObject>().Select(s => s.Source));
            }

            return confirmation.Confirmed;
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
