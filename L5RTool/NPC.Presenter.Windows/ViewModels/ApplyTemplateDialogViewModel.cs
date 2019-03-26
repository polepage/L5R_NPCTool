using NPC.Common;
using NPC.Parser;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Dialogs;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class ApplyTemplateDialogViewModel: BaseDialogViewModel
    {
        private IStorage _storage;
        private Dictionary<IGameObjectReference, IGameObject> _cache;

        public ApplyTemplateDialogViewModel(IStorage storage, IParser parser)
        {
            _storage = storage;
            Parser = parser;

            var keptAdvantages = new ObservableCollection<IAdvantage>();
            keptAdvantages.CollectionChanged += AdvantageSelectionChanged;
            KeptAdvantages = keptAdvantages;

            var newAdvantages = new ObservableCollection<IAdvantage>();
            newAdvantages.CollectionChanged += AdvantageSelectionChanged;
            NewAdvantages = newAdvantages;

            var keptDisadvantages = new ObservableCollection<IDisadvantage>();
            keptDisadvantages.CollectionChanged += DisadvantageSelectionChanged;
            KeptDisadvantages = keptDisadvantages;

            var newDisadvantages = new ObservableCollection<IDisadvantage>();
            newDisadvantages.CollectionChanged += DisadvantageSelectionChanged;
            NewDisadvantages = newDisadvantages;

            _currentAbilities = new ObservableCollection<IAbility>();
            CurrentAbilities = _currentAbilities;

            var selectedAbilities = new ObservableCollection<IGameObjectMetadata>();
            selectedAbilities.CollectionChanged += OnAbilitiesUpdated;
            SelectedAbilities = selectedAbilities;
        }

        private DelegateCommand _applyCommand;
        public ICommand ApplyCommand => _applyCommand ?? (_applyCommand = new DelegateCommand(Apply));

        public IParser Parser { get; }

        private ICharacter _character;
        public ICharacter Character
        {
            get => _character;
            private set => SetProperty(ref _character, value);
        }

        private ITemplate _currentTemplate;
        public ITemplate CurrentTemplate
        {
            get => _currentTemplate;
            private set => SetProperty(ref _currentTemplate, value);
        }

        private IGameObjectMetadata _selectedTemplate;
        public IGameObjectMetadata SelectedTemplate
        {
            get => _selectedTemplate;
            set
            {
                if (SetProperty(ref _selectedTemplate, value))
                {
                    OnTemplateUpdated();
                }
            }
        }

        private IEnumerable<IGameObjectMetadata> _templates;
        public IEnumerable<IGameObjectMetadata> Templates => _templates ?? (_templates = _storage.Database.GameObjects.Where(go => go.Type == ObjectType.Template));

        public bool CanGoToAdvantages => SelectedTemplate != null;

        public IList<IAdvantage> KeptAdvantages { get; }
        public IList<IAdvantage> NewAdvantages { get; }
        public bool CanGoToDisadvantages => Character != null && CurrentTemplate != null &&
                                            CanTraitProgress(Character.Advantages.Count() - KeptAdvantages.Count, NewAdvantages.Count, CurrentTemplate.AdvantageRemplacements);

        public IList<IDisadvantage> KeptDisadvantages { get; }
        public IList<IDisadvantage> NewDisadvantages { get; }
        public bool CanGoToAbilities => Character != null && CurrentTemplate != null &&
                                        CanTraitProgress(Character.Disadvantages.Count() - KeptDisadvantages.Count, NewDisadvantages.Count, CurrentTemplate.DisadvantageRemplacements);

        private ObservableCollection<IAbility> _currentAbilities;
        public IEnumerable<IAbility> CurrentAbilities { get; }

        public IList<IGameObjectMetadata> SelectedAbilities { get; }

        public IEnumerable<IGameObjectMetadata> Abilities => _storage.Database.GameObjects
            .Where(go => go.Type == ObjectType.Ability)
            .Where(go => CurrentTemplate != null && CurrentTemplate.AbilityTypes.Any(at => go.Keywords.Contains(at.ToString())))
            .OrderBy(go => go.Name);

        public bool CanGoToDemeanor => CurrentTemplate != null && SelectedAbilities.Count <= CurrentTemplate.AbilityAdditions;

        private IDemeanor _currentDemeanor;
        public IDemeanor CurrentDemeanor
        {
            get => _currentDemeanor;
            set
            {
                if (SetProperty(ref _currentDemeanor, value))
                {
                    OnDemeanorUpdated();
                }
            }
        }

        public bool CanGoToConfirmation => CurrentDemeanor != null;

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            Title = parameters.GetValue<string>(Dialog.Title);

            Character = parameters.GetValue<ICharacter>(Dialog.ApplyTemplate.Character);
            SelectCharacterTraits();

            _cache = new Dictionary<IGameObjectReference, IGameObject>();
        }

        private void OnTemplateUpdated()
        {
            CurrentTemplate = SelectedTemplate != null ? (ITemplate)Open(SelectedTemplate) : null;
            SelectCharacterTraits();
            NewAdvantages.Clear();
            NewDisadvantages.Clear();
            SelectedAbilities.Clear();
            CurrentDemeanor = null;
            RaisePropertyChanged(nameof(CanGoToAdvantages));
            RaisePropertyChanged(nameof(Abilities));
        }

        private void OnAbilitiesUpdated(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                _currentAbilities.Clear();
            }

            if (e.OldItems != null)
            {
                foreach (IGameObjectMetadata reference in e.OldItems)
                {
                    _currentAbilities.Remove((IAbility)Open(reference));
                }
            }

            if (e.NewItems != null)
            {
                foreach (IGameObjectMetadata reference in e.NewItems)
                {
                    _currentAbilities.Add((IAbility)Open(reference));
                }
            }

            RaisePropertyChanged(nameof(CanGoToDemeanor));
        }

        private void OnDemeanorUpdated()
        {
            RaisePropertyChanged(nameof(CanGoToConfirmation));
        }

        private void AdvantageSelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(CanGoToDisadvantages));
        }

        private void DisadvantageSelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(CanGoToAbilities));
        }

        private void Apply()
        {
            Character.ApplyTemplate(CurrentTemplate,
                                    Character.Advantages.Where(a => !KeptAdvantages.Contains(a)).ToList(),
                                    NewAdvantages,
                                    Character.Disadvantages.Where(d => !KeptDisadvantages.Contains(d)).ToList(),
                                    NewDisadvantages,
                                    CurrentAbilities,
                                    CurrentDemeanor);

            RaiseRequestClose(new DialogResult(true));
        }

        private IGameObject Open(IGameObjectMetadata reference)
        {
            if (_cache.TryGetValue(reference, out IGameObject gameObject))
            {
                return gameObject;
            }

            gameObject = _storage.Open(reference);
            _cache.Add(reference, gameObject);

            return gameObject;
        }

        private void SelectCharacterTraits()
        {
            KeptAdvantages.Clear();
            KeptDisadvantages.Clear();

            foreach (var advantage in Character.Advantages)
            {
                KeptAdvantages.Add(advantage);
            }

            foreach (var disadvantage in Character.Disadvantages)
            {
                KeptDisadvantages.Add(disadvantage);
            }
        }

        private bool CanTraitProgress(int removed, int selection, int max)
        {
            return selection <= max && selection >= removed;
        }
    }
}
