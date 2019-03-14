﻿using CS.Utils;
using CS.Utils.Collections;
using NPC.Common;
using NPC.Parser;
using NPC.Presenter.GameObjects;
using NPC.Presenter.Windows.Dialogs;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;

namespace NPC.Presenter.Windows.ViewModels
{
    class PrintDialogViewModel: BaseDialogViewModel
    {
        public PrintDialogViewModel()
        {
            var collection = new ObservableCollection<object>();
            collection.CollectionChanged += SelectionChanged;
            SelectedItems = collection;
        }

        private DelegateCommand _printCompleteCommand;
        public ICommand PrintCompleteCommand => _printCompleteCommand ?? (_printCompleteCommand = new DelegateCommand(PrintComplete));

        private ICommand _selectCommand;
        public ICommand SelectCommand
        {
            get => _selectCommand;
            set => SetProperty(ref _selectCommand, value);
        }

        private IEnumerable<ObjectMetadataGroup> _gameObjectGroups;
        public IEnumerable<ObjectMetadataGroup> GameObjectGroups
        {
            get => _gameObjectGroups;
            private set => SetProperty(ref _gameObjectGroups, value);
        }

        public bool CanPreview => SelectedItems.OfType<IGameObjectReference>().Any();

        private IList _selectedItems;
        public IList SelectedItems
        {
            get => _selectedItems;
            set => SetProperty(ref _selectedItems, value);
        }

        private IEnumerable<IGameObject> _displayedObjects;
        public IEnumerable<IGameObject> DisplayedObjects
        {
            get => _displayedObjects;
            set => SetProperty(ref _displayedObjects, value);
        }

        private IParser _parser;
        public IParser Parser
        {
            get => _parser;
            private set => SetProperty(ref _parser, value);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            base.OnDialogOpened(parameters);
            Title = parameters.GetValue<string>(Dialog.Title);

            GameObjectGroups = EnumHelpers.GetValues<ObjectType>()
                .Select(ot => new ObjectMetadataGroup(ot, parameters.GetValue<IManifest>(Dialog.Print.Source).GameObjects));

            Func<IGameObjectReference, IGameObject> open = parameters.GetValue<Func<IGameObjectReference, IGameObject>>(Dialog.Print.Opener);
            DisplayedObjects = new CachedEnumerableWrapper<IGameObject, object>(
             SelectedItems as IEnumerable<object>,
             o => open((IGameObjectReference)o),
             (o, go) => ((IGameObjectReference)o).Equals(go),
             o => o is IGameObjectReference,
             (go1, go2) =>
             {
                 int eq = go1.Type.CompareTo(go2.Type);
                 if (eq == 0)
                 {
                     eq = go1.Name.CompareTo(go2.Name);
                 }

                 return eq;
             });

            Parser = parameters.GetValue<IParser>(Dialog.Print.Parser);
        }

        private void SelectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(CanPreview));
        }

        private void PrintComplete()
        {
            RaiseRequestClose(new DialogResult(true));
        }
    }
}
