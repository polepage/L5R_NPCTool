using Prism.Mvvm;
using System;

namespace NPC.Data.GameObjects
{
    abstract class GameObjectReference: BindableBase, IGameObjectReference
    {
        protected GameObjectReference(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is GameObjectReference go)
            {
                return Id == go.Id;
            }

            return false;
        }
    }
}
