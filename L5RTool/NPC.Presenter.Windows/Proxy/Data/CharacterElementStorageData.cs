using NPC.Common;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Dialogs;
using NPC.Presenter.Windows.Events;
using NPC.Presenter.Windows.GameObjects;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace NPC.Presenter.Windows.Proxy.Data
{
    class CharacterElementStorageData : BindableBase
    {
        private IFactory _factory;
        private IStorage _storage;
        private IDialogService _dialogService;
        private IEventAggregator _eventAggregator;

        public CharacterElementStorageData(IFactory factory, IStorage storage, IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _factory = factory;
            _storage = storage;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
        }

        private DelegateCommand<IGameObject> _copyTemplateCommand;
        public ICommand CopyTemplateCommand => _copyTemplateCommand ?? (_copyTemplateCommand = new DelegateCommand<IGameObject>(CopyTemplate));

        private DelegateCommand<IGameObject> _openTabCommand;
        public ICommand OpenTabCommand => _openTabCommand ?? (_openTabCommand = new DelegateCommand<IGameObject>(OpenTab));

        public CharacterElement ElementType { get; set; }

        private void CopyTemplate(IGameObject target)
        {
            var parameters = new DialogParameters
            {
                { Dialog.Title, "Select Item" },
                { Dialog.CharacterElementSelection.Source, GetAvailableElements() }
            };

            _dialogService.ShowDialog(Dialog.CharacterElementSelection.Name, parameters, dialogResult =>
            {
                if (dialogResult.Result.GetValueOrDefault())
                {
                    IGameObject source = dialogResult.Parameters.GetValue<IGameObject>(Dialog.CharacterElementSelection.Selection);
                    _factory.CopyTo(target, source);
                }
            });
        }

        private void OpenTab(IGameObject target)
        {
            _eventAggregator.GetEvent<OpenGameObjectEvent>().Publish(_factory.Duplicate(target));
        }

        private IEnumerable<IGameObjectMetadata> GetAvailableElements()
        {
            switch (ElementType)
            {
                case CharacterElement.Demeanor:
                    return _storage.Database.GameObjects
                        .Where(go => go.Type == ObjectType.Demeanor)
                        .OrderBy(go => go.Name);
                case CharacterElement.Advantage:
                    return _storage.Database.GameObjects
                        .Where(go => go.Type == ObjectType.Advantage)
                        .OrderBy(go => go.Name);
                case CharacterElement.Disadvantage:
                    return _storage.Database.GameObjects
                        .Where(go => go.Type == ObjectType.Disadvantage)
                        .OrderBy(go => go.Name);
                case CharacterElement.FavoredWeapon:
                    return _storage.Database.GameObjects
                        .Where(go => go.Type == ObjectType.Equipment && go.Keywords.Contains(GearType.Weapon.ToString()))
                        .OrderBy(go => go.Name);
                case CharacterElement.EquippedGear:
                case CharacterElement.OtherGear:
                    return _storage.Database.GameObjects
                        .Where(go => go.Type == ObjectType.Equipment)
                        .OrderBy(go => go.Name);
                case CharacterElement.Ability:
                    return _storage.Database.GameObjects
                        .Where(go => go.Type == ObjectType.Ability)
                        .OrderBy(go => go.Name);
                default:
                    return null;
            }
        }
    }
}
