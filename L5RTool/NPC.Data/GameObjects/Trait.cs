using CS.Utils.Collections;
using NPC.Common;
using System.Collections.Generic;

namespace NPC.Data.GameObjects
{
    abstract class Trait : BaseGameObject, ITrait
    {
        public Trait()
        {
            SkillGroups = new ObservableHashSet<SkillGroup>();
            Spheres = new ObservableHashSet<TraitSphere>();
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private Ring _ring;
        public Ring Ring
        {
            get => _ring;
            set => SetProperty(ref _ring, value);
        }

        public ISet<SkillGroup> SkillGroups { get; }
        public ISet<TraitSphere> Spheres { get; }

        public ObjectType Type { get; protected set; }
    }
}
