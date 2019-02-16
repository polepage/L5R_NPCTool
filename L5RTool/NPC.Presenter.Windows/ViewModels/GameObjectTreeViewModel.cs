using CS.Utils;
using NPC.Common;
using NPC.Presenter.GameObjects;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;

namespace NPC.Presenter.Windows.ViewModels
{
    class GameObjectTreeViewModel : BindableBase
    {
        public GameObjectTreeViewModel(Business.IStorage storage)
        {
            References = EnumHelpers.GetValues<ObjectType>()
                .Select(ot => new ObjectReferenceGroup(ot, storage.Database.References));
        }

        public IEnumerable<ObjectReferenceGroup> References { get; }
    }
}
