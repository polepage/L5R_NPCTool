using CS.Utils;
using CS.Utils.Collections;
using NPC.Common;
using NPC.Presenter.GameObjects;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace NPC.Presenter.Windows.ViewModels
{
    class GameObjectTreeViewModel : BindableBase
    {
        private RelayObservableHashSet<IObjectReference, Business.GameObjects.IObjectReference> _collection;

        public GameObjectTreeViewModel(Business.IStorage storage)
        {
            _collection = new RelayObservableHashSet<IObjectReference, Business.GameObjects.IObjectReference>(
                storage.Database.References,
                r => new ObjectReference(r));

            References = EnumHelpers.GetValues<ObjectType>()
                .Select(ot => new ObjectReferenceGroup(
                    ot,
                    _collection.Where(r => r.Type == ot)
                               .OrderBy(or => or.Name)));

            _collection.CollectionChanged += CollectionChanged;
        }

        public IEnumerable<ObjectReferenceGroup> References { get; }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                RaisePropertyChanged(nameof(References));
            }
            else if (e.Action != NotifyCollectionChangedAction.Move)
            {
                IEnumerable<ObjectType> addedTypes = e.NewItems?
                    .Cast<IObjectReference>()
                    .Select(or => or.Type)
                    .Distinct();

                IEnumerable<ObjectType> removedTypes = e.OldItems?
                    .Cast<IObjectReference>()
                    .Select(or => or.Type)
                    .Distinct();

                IEnumerable<ObjectType> modifiedTypes;
                if (addedTypes != null && removedTypes != null)
                {
                    modifiedTypes = addedTypes.Concat(removedTypes).Distinct();
                }
                else if (addedTypes != null)
                {
                    modifiedTypes = addedTypes;
                }
                else
                {
                    modifiedTypes = removedTypes;
                }

                foreach (var group in References.Where(r => modifiedTypes.Contains(r.Type)))
                {
                    group.UpdateReferences();
                }
            }
        }
    }
}
