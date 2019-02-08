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

        public TraitElement(Trait element)
            :base(element)
        {
            _skillGroups = new ObservableCollection<SkillGroup>();
            _skillGroups.CollectionChanged += SkillGroupsChanged;
        }

        public string Name
        {
            get => TypedElement.Name;
            set => SetProperty(v => TypedElement.Name = v, () => TypedElement.Name, value);
        }

        public string Description
        {
            get => TypedElement.Description;
            set => SetProperty(v => TypedElement.Description = v, () => TypedElement.Description, value);
        }

        public Ring Ring
        {
            get => TypedElement.Ring;
            set => SetProperty(v => TypedElement.Ring = v, () => TypedElement.Ring, value);
        }

        public TraitType TraitType
        {
            get => TypedElement.TraitType;
            set => SetProperty(v => TypedElement.TraitType = v, () => TypedElement.TraitType, value);
        }

        public IList<SkillGroup> SkillGroups => _skillGroups;

        public IEnumerable<Ring> RingList => EnumHelpers.GetValues<Ring>();
        public IEnumerable<TraitType> TraitTypeList => EnumHelpers.GetValues<TraitType>();
        public IEnumerable<SkillGroup> SkillGroupList => EnumHelpers.GetValues<SkillGroup>();

        private void SkillGroupsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            SetChanged(TypedElement.SkillGroups, e);
        }

        private void SetChanged<TElement>(ISet<TElement> target, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                target.Clear();
            }
            else
            {
                if (e.OldItems != null)
                {
                    foreach (TElement item in e.OldItems)
                    {
                        target.Remove(item);
                    }
                }

                if (e.NewItems != null)
                {
                    foreach (TElement item in e.NewItems)
                    {
                        target.Add(item);
                    }
                }
            }
        }
    }
}
