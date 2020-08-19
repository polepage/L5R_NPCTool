using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Dialogs;
using NPC.Presenter.Windows.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class GameObjectEditorViewModel: BindableBase
    {
        readonly IEventAggregator _eventAggregator;
        readonly IDialogService _dialogService;
        readonly IStorage _storage;
        readonly IFactory _factory;

        public GameObjectEditorViewModel(IEventAggregator eventAggregator, IDialogService dialogService, IStorage storage, IFactory factory)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenGameObjectEvent>().Subscribe(GameObjectOpened);
            _eventAggregator.GetEvent<SaveCurrentGameObjectEvent>().Subscribe(Save);
            _eventAggregator.GetEvent<SaveAllGameObjectsEvent>().Subscribe(SaveAll);
            _eventAggregator.GetEvent<CloseCurrentGameObjectEvent>().Subscribe(Close);
            _eventAggregator.GetEvent<CloseAllGameObjectsEvent>().Subscribe(CloseAll);
            _eventAggregator.GetEvent<CancellableCloseAllGameObjectsEvent>().Subscribe(CancellableCloseAll);
            _eventAggregator.GetEvent<ForceCloseGameObjects>().Subscribe(ForceClose);
            _eventAggregator.GetEvent<DuplicateCurrentGameObjectEvent>().Subscribe(Duplicate);

            _dialogService = dialogService;
            _storage = storage;
            _factory = factory;

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
        public ICommand CloseCommand => _closeCommand ??= new DelegateCommand(Close);

        private DelegateCommand _closeAllCommand;
        public ICommand CloseAllCommand => _closeAllCommand ??= new DelegateCommand(CloseAll);

        private DelegateCommand _closeOthersCommand;
        public ICommand CloseOthersCommand => _closeOthersCommand ??= new DelegateCommand(CloseOthers);

        private DelegateCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ??= new DelegateCommand(Save);

        private DelegateCommand _duplicateCommand;
        public ICommand DuplicateCommand => _duplicateCommand ??= new DelegateCommand(Duplicate);

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

        private void ForceClose(IEnumerable<IGameObjectReference> references)
        {
            var toRemove = GameObjects.Where(go => references.Contains(go)).ToList();
            foreach (IGameObject go in toRemove)
            {
                GameObjects.Remove(go);
            }
        }

        private bool ValidateDirtyFiles(IEnumerable<IGameObject> gameObjects)
        {
            if (!gameObjects.Where(go => go.IsDirty).Any())
            {
                return true;
            }

            IDialogParameters parameters = new DialogParameters
            {
                { Dialog.Title, "Unsaved Changes" },
                { Dialog.Save.Items, gameObjects.Where(go => go.IsDirty).ToList() },
                { Dialog.Save.Selector, (Action<IGameObject>)(go => SelectedObject = go) }
            };

            bool result = false;
            _dialogService.ShowDialog(Dialog.Save.Name, parameters, dialogResult =>
            {
                result = dialogResult.Result.GetValueOrDefault();
                if (dialogResult.Parameters.TryGetValue(Dialog.Save.NeedSave, out bool needSave) && needSave)
                {
                    _storage.Save(GameObjects.Where(o => o.IsDirty));
                }
            });

            return result;
        }

        private void Save()
        {
            if (SelectedObject != null && SelectedObject.IsDirty)
            {
                _storage.Save(SelectedObject);
            }
        }

        private void SaveAll()
        {
            if (GameObjects.Any(e => e.IsDirty))
            {
                _storage.Save(GameObjects.Where(o => o.IsDirty));
            }
        }

        private void Duplicate()
        {
            if (SelectedObject != null)
            {
                IGameObject copy = _factory.Duplicate(SelectedObject);
                GameObjectOpened(copy);
            }
        }
    }
}
