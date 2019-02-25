using CS.Utils.Prism.Mvvm;
using NPC.Presenter.GameObjects;

namespace NPC.Presenter.GameObjects
{
    abstract class GameObjectReference: RelayBindableBase, IGameObjectReference
    {
        public GameObjectReference(Business.GameObjects.IGameObjectReference source)
            : base (source)
        {
            ReferenceSource = source;
        }

        public Business.GameObjects.IGameObjectReference ReferenceSource { get; }

        public override int GetHashCode()
        {
            return ReferenceSource.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is GameObjectReference or)
            {
                return ReferenceSource.Equals(or.ReferenceSource);
            }

            return false;
        }
    }
}
