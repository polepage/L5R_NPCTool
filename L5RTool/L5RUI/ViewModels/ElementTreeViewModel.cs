using L5RUI.Model.Database;
using L5RUI.Utils;
using NPC.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace L5RUI.ViewModels
{
    public class ElementTreeViewModel : BindableBase
    {
        public ElementTreeViewModel()
        {
            Content = Init();
        }

        public IList<Group> Content { get; }

        private IList<Group> Init()
        {
            IList<Group> groups = EnumHelpers.GetValues<ElementType>().Select(t => new Group(t)).ToList();

            // Dummy
            foreach (Group group in groups)
            {
                group.Entries.Add(new Entry());
            }

            return groups;
        }
    }
}
