using CS.Utils.Collections;
using NPC.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;

namespace NPC.Data.GameObjects
{
    class Template : GameObject, ITemplate
    {
        public Template()
            : base(ObjectType.Template)
        {
            Initialize();
        }

        private Template(Guid id)
            : base(id, ObjectType.Template)
        {
            Initialize();
        }

        private void Initialize()
        {
            _suggestedAdvantages = new ObservableCollection<Advantage>();
            _suggestedAdvantages.CollectionChanged += OnCollectionChanged;

            _suggestedDisadvantages = new ObservableCollection<Disadvantage>();
            _suggestedDisadvantages.CollectionChanged += OnCollectionChanged;

            _abilityTypes = new ObservableHashSet<AbilityType>();
            _abilityTypes.CollectionChanged += (s, e) => IsDirty = true;

            _suggestedDemeanors = new ObservableCollection<Demeanor>();
            _suggestedDemeanors.CollectionChanged += OnCollectionChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            IsDirty = true;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IsDirty = true;
            if (e.OldItems != null)
            {
                foreach (var gameObject in e.OldItems.OfType<GameObject>())
                {
                    gameObject.PropertyChanged -= OnPropertyChanged;
                }
            }

            if (e.NewItems != null)
            {
                foreach (var gameObject in e.NewItems.OfType<GameObject>())
                {
                    gameObject.PropertyChanged += OnPropertyChanged;
                }
            }
        }

        // Conflict Ranks
        private int _combatConflictRank;
        public int CombatConflictRank
        {
            get => _combatConflictRank;
            set => IsDirty |= SetProperty(ref _combatConflictRank, value);
        }

        private int _intrigueConflictRank;
        public int IntrigueConflictRank
        {
            get => _intrigueConflictRank;
            set => IsDirty |= SetProperty(ref _intrigueConflictRank, value);
        }

        // Rings
        private int _air;
        public int Air
        {
            get => _air;
            set => IsDirty |= SetProperty(ref _air, value);
        }

        private int _earth;
        public int Earth
        {
            get => _earth;
            set => IsDirty |= SetProperty(ref _earth, value);
        }

        private int _fire;
        public int Fire
        {
            get => _fire;
            set => IsDirty |= SetProperty(ref _fire, value);
        }

        private int _water;
        public int Water
        {
            get => _water;
            set => IsDirty |= SetProperty(ref _water, value);
        }

        private int _void;
        public int Void
        {
            get => _void;
            set => IsDirty |= SetProperty(ref _void, value);
        }

        // Skills
        private int _artisan;
        public int Artisan
        {
            get => _artisan;
            set => IsDirty |= SetProperty(ref _artisan, value);
        }

        private int _martial;
        public int Martial
        {
            get => _martial;
            set => IsDirty |= SetProperty(ref _martial, value);
        }

        private int _scholar;
        public int Scholar
        {
            get => _scholar;
            set => IsDirty |= SetProperty(ref _scholar, value);
        }

        private int _social;
        public int Social
        {
            get => _social;
            set => IsDirty |= SetProperty(ref _social, value);
        }

        private int _trade;
        public int Trade
        {
            get => _trade;
            set => IsDirty |= SetProperty(ref _trade, value);
        }

        // Sub elements
        private int _advantageRemplacements;
        public int AdvantageRemplacements
        {
            get => _advantageRemplacements;
            set => IsDirty |= SetProperty(ref _advantageRemplacements, value);
        }

        private int _disadvantageRemplacements;
        public int DisadvantageRemplacements
        {
            get => _disadvantageRemplacements;
            set => IsDirty |= SetProperty(ref _disadvantageRemplacements, value);
        }

        private ObservableCollection<Advantage> _suggestedAdvantages;
        public IEnumerable<IAdvantage> SuggestedAdvantages => _suggestedAdvantages;

        private ObservableCollection<Disadvantage> _suggestedDisadvantages;
        public IEnumerable<IDisadvantage> SuggestedDisadvantages => _suggestedDisadvantages;

        private int _abilityAdditions;
        public int AbilityAdditions
        {
            get => _abilityAdditions;
            set => IsDirty |= SetProperty(ref _abilityAdditions, value);
        }

        private ObservableHashSet<AbilityType> _abilityTypes;
        public ISet<AbilityType> AbilityTypes => _abilityTypes;

        private ObservableCollection<Demeanor> _suggestedDemeanors;
        public IEnumerable<IDemeanor> SuggestedDemeanors => _suggestedDemeanors;

        // Methods
        public void AddAdvantage()
        {
            _suggestedAdvantages.Add(new Advantage());
        }

        public void RemoveAdvantage(IAdvantage advantage)
        {
            RemoveElement(_suggestedAdvantages, advantage);
        }

        public void AddDisadvantage()
        {
            _suggestedDisadvantages.Add(new Disadvantage());
        }

        public void RemoveDisadvantage(IDisadvantage disadvantage)
        {
            RemoveElement(_suggestedDisadvantages, disadvantage);
        }

        public void AddDemeanor()
        {
            _suggestedDemeanors.Add(new Demeanor());
        }

        public void RemoveDemeanor(IDemeanor demeanor)
        {
            RemoveElement(_suggestedDemeanors, demeanor);
        }

        private void RemoveElement<T>(IList<T> collection, object element)
        {
            if (element is T gameObject)
            {
                collection.Remove(gameObject);
            }
            else
            {
                throw new ArgumentException("Remove Character Element: Bad Element");
            }
        }

        // Not interface
        public static GameObject FromXml(XElement xml)
        {
            return FromXml(xml, t =>
            {
                if (t.type != ObjectType.Template)
                {
                    throw new ArgumentException("Template.FromXml: xml is not a template.");
                }

                return t.id.HasValue ? new Template(t.id.Value) : new Template();
            });
        }

        public override XElement CreateXml(bool external = false)
        {
            var xml = base.CreateXml(external);
            xml.Add(new XElement("TemplateData",
                                 CreateConflictRanksXml(),
                                 CreateRingsXml(),
                                 CreateSkillsXml(),
                                 CreateAdditionList("Advantages", AdvantageRemplacements, _suggestedAdvantages.Select(a => a.CreateXml(true))),
                                 CreateAdditionList("Disadvantages", DisadvantageRemplacements, _suggestedDisadvantages.Select(d => d.CreateXml(true))),
                                 CreateAdditionList("AbilityTypes", AbilityAdditions, _abilityTypes.Select(at => new XElement("Item", at))),
                                 CreateCollection("Demeanors", _suggestedDemeanors)));
            return xml;
        }

        protected override void LoadXml(XElement xml)
        {
            base.LoadXml(xml);

            XElement templateData = xml.Element("TemplateData");

            LoadConflictRanksXml(templateData);
            LoadRingsXml(templateData);
            LoadSkillsXml(templateData);
            AdvantageRemplacements = LoadAdditionList(xml, "Advantages", _suggestedAdvantages, x => Advantage.FromXml(x));
            DisadvantageRemplacements = LoadAdditionList(xml, "Disadvantages", _suggestedDisadvantages, x => Disadvantage.FromXml(x));
            AbilityAdditions = LoadAdditionList(xml, "AbilityTypes", _abilityTypes, x => (AbilityType)Enum.Parse(typeof(AbilityType), x.Value));
            LoadCollection(xml, "Demeanors", _suggestedDemeanors, x => Demeanor.FromXml(x));
        }

        private XElement CreateConflictRanksXml()
        {
            return new XElement("ConflictRank",
                                new XElement("Combat", CombatConflictRank),
                                new XElement("Intrigue", IntrigueConflictRank));
        }

        private void LoadConflictRanksXml(XElement xml)
        {
            XElement conflictData = xml.Element("ConflictRank");

            CombatConflictRank = int.Parse(conflictData.Element("Combat").Value);
            IntrigueConflictRank = int.Parse(conflictData.Element("Intrigue").Value);
        }

        private XElement CreateRingsXml()
        {
            return new XElement("Rings",
                                new XElement("Air", Air),
                                new XElement("Earth", Earth),
                                new XElement("Fire", Fire),
                                new XElement("Water", Water),
                                new XElement("Void", Void));
        }

        private void LoadRingsXml(XElement xml)
        {
            XElement ringsData = xml.Element("Rings");

            Air = int.Parse(ringsData.Element("Air").Value);
            Earth = int.Parse(ringsData.Element("Earth").Value);
            Fire = int.Parse(ringsData.Element("Fire").Value);
            Water = int.Parse(ringsData.Element("Water").Value);
            Void = int.Parse(ringsData.Element("Void").Value);
        }

        private XElement CreateSkillsXml()
        {
            return new XElement("Skills",
                                new XElement("Artisan", Artisan),
                                new XElement("Martial", Martial),
                                new XElement("Scholar", Scholar),
                                new XElement("Social", Social),
                                new XElement("Trade", Trade));
        }

        private void LoadSkillsXml(XElement xml)
        {
            XElement skillsData = xml.Element("Skills");

            Artisan = int.Parse(skillsData.Element("Artisan").Value);
            Martial = int.Parse(skillsData.Element("Martial").Value);
            Scholar = int.Parse(skillsData.Element("Scholar").Value);
            Social = int.Parse(skillsData.Element("Social").Value);
            Trade = int.Parse(skillsData.Element("Trade").Value);
        }

        private XElement CreateAdditionList(string name, int count, IEnumerable<XElement> elements)
        {
            var list = new XElement(name, new XAttribute("Count", count));
            list.Add(elements);

            return list;
        }

        private int LoadAdditionList<T>(XElement xml, string name, ICollection<T> collection, Func<XElement, T> loader)
        {
            var list = xml.Element(name);

            foreach (XElement element in list.Elements())
            {
                collection.Add(loader(element));
            }

            return int.Parse(list.Attribute("Count").Value);
        }

        private XElement CreateCollection(string name, IEnumerable<GameObject> collection)
        {
            return new XElement(name, collection.Select(go => go.CreateXml(true)));
        }

        private void LoadCollection<T>(XElement xml, string name, ICollection<T> collection, Func<XElement, T> loader)
        {
            foreach (XElement element in xml.Element(name).Elements())
            {
                collection.Add(loader(element));
            }
        }
    }
}
