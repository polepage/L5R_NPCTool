using CS.Utils.Prism.Mvvm;

namespace NPC.Presenter.GameObjects
{
    abstract class GameObjectData<T> : RelayBindableBase, IGameObjectData where T: Data.GameObjects.IGameObjectData
    {
        public GameObjectData(T dataObject)
            : base(dataObject)
        {
            DataObject = dataObject;
        }

        protected T DataObject { get; }
    }
}
