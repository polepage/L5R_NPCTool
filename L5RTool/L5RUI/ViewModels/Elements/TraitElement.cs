using L5RUI.Utils;
using NPC.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace L5RUI.ViewModels.Elements
{
    class TraitElement : BaseElementViewModel
    {
        private Trait _element;

        public TraitElement(Trait element)
            :base(element)
        {
            _element = element;
        }

        public string Name
        {
            get => _element.Name;
            set => SetProperty(v => _element.Name = v, () => _element.Name, value);
        }

        public string Description
        {
            get => _element.Description;
            set => SetProperty(v => _element.Description = v, () => _element.Description, value);
        }

        public Ring Ring
        {
            get => _element.Ring;
            set => SetProperty(v => _element.Ring = v, () => _element.Ring, value);
        }

        public TraitType TraitType
        {
            get => _element.TraitType;
            set => SetProperty(v => _element.TraitType = v, () => _element.TraitType, value);
        }

        public IEnumerable<Ring> RingList => EnumHelpers.GetValues<Ring>();
        public IEnumerable<TraitType> TraitTypeList => EnumHelpers.GetValues<TraitType>();
        public IEnumerable<SkillGroup> SkillGroupList => EnumHelpers.GetValues<SkillGroup>();
    }
}
