using L5RUI.Utils;
using NPC.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace L5RUI.ViewModels.Elements
{
    class TraitElement : BaseElementViewModel<Trait>
    {
        private ObservableCollection<SkillGroup> _skillGroups;
        private ObservableCollection<TraitSphere> _spheres;

        public TraitElement(Trait element)
            :base(element)
        {
            _skillGroups = new ObservableCollection<SkillGroup>();
            _skillGroups.CollectionChanged += SkillGroupsChanged;

            _spheres = new ObservableCollection<TraitSphere>();
            _spheres.CollectionChanged += SpheresChanged;
        }

        public string Name
        {
            get => TypedElement.Name;
            set => IsDirty |= SetProperty(v => TypedElement.Name = v, () => TypedElement.Name, value);
        }

        public string Description
        {
            get => TypedElement.Description;
            set => IsDirty |= SetProperty(v => TypedElement.Description = v, () => TypedElement.Description, value);
        }

        public Ring Ring
        {
            get => TypedElement.Ring;
            set => IsDirty |= SetProperty(v => TypedElement.Ring = v, () => TypedElement.Ring, value);
        }

        public TraitType TraitType
        {
            get => TypedElement.TraitType;
            set => IsDirty |= SetProperty(v => TypedElement.TraitType = v, () => TypedElement.TraitType, value);
        }

        public IList<SkillGroup> SkillGroups => _skillGroups;
        public IList<TraitSphere> Spheres => _spheres;

        public IEnumerable<Ring> RingList => EnumHelpers.GetValues<Ring>();
        public IEnumerable<TraitType> TraitTypeList => EnumHelpers.GetValues<TraitType>();
        public IEnumerable<SkillGroup> SkillGroupList => EnumHelpers.GetValues<SkillGroup>();
        public IEnumerable<TraitSphere> SpheresList => EnumHelpers.GetValues<TraitSphere>();

        private void SkillGroupsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IsDirty |= SetChanged(TypedElement.SkillGroups, e);
        }

        private void SpheresChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IsDirty |= SetChanged(TypedElement.Spheres, e);
        }

        private bool SetChanged<TElement>(ISet<TElement> target, NotifyCollectionChangedEventArgs e)
        {
            bool hasChanged = false;

            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                hasChanged |= target.Count > 0;
                target.Clear();
            }
            else
            {
                if (e.OldItems != null)
                {
                    foreach (TElement item in e.OldItems)
                    {
                        hasChanged |= target.Remove(item);
                    }
                }

                if (e.NewItems != null)
                {
                    foreach (TElement item in e.NewItems)
                    {
                        hasChanged |= target.Add(item);
                    }
                }
            }

            return hasChanged;
        }
    }
}
