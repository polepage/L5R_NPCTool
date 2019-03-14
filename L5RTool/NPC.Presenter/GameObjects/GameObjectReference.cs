using CS.Utils.Prism.Mvvm;

namespace NPC.Presenter.GameObjects
{
    abstract class GameObjectReference: RelayBindableBase, IGameObjectReference
    {
        Data.GameObjects.IGameObjectReference _source;

        public GameObjectReference(Data.GameObjects.IGameObjectReference source)
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
            if (obj is GameObjectReference or)
            {
                return _source.Equals(or._source);
            }

            return false;
        }
    }
}
