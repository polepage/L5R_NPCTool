using CS.Utils.Prism.Mvvm;
using NPC.Presenter.GameObjects;

namespace NPC.Presenter.GameObjects
{
    abstract class GameObjectData<T> : RelayBindableBase, IGameObjectData where T: Business.GameObjects.IGameObjectData
    {
        public GameObjectData(T dataObject)
            : base(dataObject)
        {
            DataObject = dataObject;
        }

        protected T DataObject { get; }
    }
}
