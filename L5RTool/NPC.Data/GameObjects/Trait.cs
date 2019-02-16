using CS.Utils.Collections;
using NPC.Common;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Linq;

namespace NPC.Data.GameObjects
{
    abstract class Trait : BaseGameObject, ITrait
    {
        public Trait()
        {
            SkillGroups = new ObservableHashSet<SkillGroup>();
            Spheres = new ObservableHashSet<TraitSphere>();

            (SkillGroups as INotifyCollectionChanged).CollectionChanged += (sender, args) => IsDirty = true;
            (Spheres as INotifyCollectionChanged).CollectionChanged += (sender, args) => IsDirty = true;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => IsDirty |= SetProperty(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => IsDirty |= SetProperty(ref _description, value);
        }

        private Ring _ring;
        public Ring Ring
        {
            get => _ring;
            set => IsDirty |= SetProperty(ref _ring, value);
        }

        public ISet<SkillGroup> SkillGroups { get; }
        public ISet<TraitSphere> Spheres { get; }

        public ObjectType Type { get; protected set; }

        public override XElement GenerateXML()
        {
            return new XElement("GameObject",
                                new XAttribute("Type", Type),
                                new XAttribute("Id", Id),
                                new XElement("Name", Name),
                                new XElement("Description", Description),
                                new XElement("Ring", Ring),
                                new XElement("SkillGroups",
                                             SkillGroups.Select(sg => new XElement("Item", sg))),
                                new XElement("Spheres",
                                             Spheres.Select(s => new XElement("Item", s))));
        }

        public override ObjectReference CreateReference()
        {
            return new ObjectReference(Id, Type)
            {
                Name = Name
            };
        }
    }
}
