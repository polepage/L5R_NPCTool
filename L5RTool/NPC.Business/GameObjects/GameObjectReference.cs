using CS.Utils.Prism.Mvvm;

namespace NPC.Business.GameObjects
{
    abstract class GameObjectReference: RelayBindableBase, IGameObjectReference
    {
        Data.GameObjects.IGameObjectReference _source;

        protected GameObjectReference(Data.GameObjects.IGameObjectReference source)
            : base (source)
        {
            _source = source;
        }

        public override int GetHashCode()
        {
            return _source.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is GameObjectReference go)
            {
                return _source.Equals(go._source);
            }

            return false;
        }
    }
}
