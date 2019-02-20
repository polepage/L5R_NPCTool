using CS.Utils.Prism.Mvvm;

namespace NPC.Business.GameObjects
{
    abstract class GameObjectData<T> : RelayBindableBase, IGameObjectData, ICopyTarget where T: Data.GameObjects.IGameObjectData
    {
        public GameObjectData(T dataObject)
            : base(dataObject)
        {
            DataObject = dataObject;
        }

        protected T DataObject { get; }

        public abstract void CopyData(IGameObjectData copySource);
    }
}
