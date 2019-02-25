using CS.Utils.Prism.Mvvm;

namespace NPC.Business.GameObjects
{
    abstract class GameObjectReference: RelayBindableBase, IGameObjectReference
    {
        protected GameObjectReference(Data.GameObjects.IGameObjectReference source)
            : base (source)
        {
            ReferenceSource = source;
        }

        public Data.GameObjects.IGameObjectReference ReferenceSource { get; }

        public override int GetHashCode()
        {
            return ReferenceSource.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is GameObjectReference go)
            {
                return ReferenceSource.Equals(go.ReferenceSource);
            }

            return false;
        }
    }
}
