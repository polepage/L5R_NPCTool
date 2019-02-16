using CS.Utils.Prism.Mvvm;
using NPC.Common;

namespace NPC.Presenter.GameObjects
{
    class ObjectReference : RelayBindableBase, IObjectReference
    {
        Business.GameObjects.IObjectReference _source;

        public ObjectReference(Business.GameObjects.IObjectReference source)
            : base(source)
        {
            _source = source;
        }

        public ObjectType Type => _source.Type;
        public string Name => _source.Name;

        public override int GetHashCode()
        {
            return _source.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is ObjectReference or)
            {
                return _source.Equals(or._source);
            }

            return false;
        }

        protected override void RegisterBindings()
        {
            AddBinding(nameof(_source.Type), nameof(Type));
            AddBinding(nameof(_source.Name), nameof(Name));
        }
    }
}
