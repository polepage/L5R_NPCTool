using CS.Utils.Prism.Mvvm;
using NPC.Common;

namespace NPC.Business.GameObjects
{
    class ObjectReference : RelayBindableBase, IObjectReference
    {
        public ObjectReference(Data.GameObjects.IObjectReference source)
            : base(source)
        {
            Source = source;
        }

        public Data.GameObjects.IObjectReference Source { get; }

        public ObjectType Type => Source.Type;
        public string Name => Source.Name;

        public override int GetHashCode()
        {
            return Source.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is ObjectReference or)
            {
                return Source.Equals(or.Source);
            }

            return false;
        }

        protected override void RegisterBindings()
        {
            AddBinding(nameof(Source.Type), nameof(Type));
            AddBinding(nameof(Source.Name), nameof(Name));
        }
    }
}
