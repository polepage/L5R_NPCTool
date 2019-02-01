using L5RTool.Elements;
using L5RTool.Model.Database;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace L5RTool.ViewModels
{
    class DatabaseViewModel: BindableBase
    {
        public DatabaseViewModel()
        {
            Content = Init();
        }

        public IList<Group> Content { get; }

        private IList<Group> Init()
        {
            IList<Group> groups = Enum.GetValues(typeof(ElementType)).Cast<ElementType>().Select(t => new Group(t)).ToList();

            // Dummy
            foreach (Group group in groups)
            {
                group.Entries.Add(new Entry());
            }

            return groups;
        }
    }
}
