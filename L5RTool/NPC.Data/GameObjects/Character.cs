using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using NPC.Common;

namespace NPC.Data.GameObjects
{
    class Character : GameObject, ICharacter
    {
        public Character()
            : base(ObjectType.Character)
        {
            Initialize();
        }

        private Character(Guid id)
            : base(id, ObjectType.Character)
        {
            Initialize();
        }

        private void Initialize()
        {
            _demeanor = new Demeanor();
            _demeanor.PropertyChanged += OnPropertyChanged;

            _advantages = new ObservableCollection<Advantage>();
            _advantages.CollectionChanged += OnCollectionChanged;

            _disadvantages = new ObservableCollection<Disadvantage>();
            _disadvantages.CollectionChanged += OnCollectionChanged;

            _favoredWeapons = new ObservableCollection<Gear>();
            _favoredWeapons.CollectionChanged += OnCollectionChanged;

            _equipedGear = new ObservableCollection<Gear>();
            _equipedGear.CollectionChanged += OnCollectionChanged;

            _otherGear = new ObservableCollection<Gear>();
            _otherGear.CollectionChanged += OnCollectionChanged;

            _abilities = new ObservableCollection<Ability>();
            _abilities.CollectionChanged += OnCollectionChanged;
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

        // Base Character
        private CharacterType _characterType;
        public CharacterType CharacterType
        {
            get => _characterType;
            set => IsDirty |= SetProperty(ref _characterType, value);
        }

        private bool _hasSocietalValues = true;
        public bool HasSocietalValues
        {
            get => _hasSocietalValues;
            set => IsDirty |= SetProperty(ref _hasSocietalValues, value);
        }

        private int _combatConflictRank = 1;
        public int CombatConflictRank
        {
            get => _combatConflictRank;
            set => IsDirty |= SetProperty(ref _combatConflictRank, value);
        }

        private int _intrigueConflictRank = 1;
        public int IntrigueConflictRank
        {
            get => _intrigueConflictRank;
            set => IsDirty |= SetProperty(ref _intrigueConflictRank, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => IsDirty |= SetProperty(ref _description, value);
        }

        // Rings
        private int _air = 1;
        public int Air
        {
            get => _air;
            set => IsDirty |= SetProperty(ref _air, value);
        }

        private int _earth = 1;
        public int Earth
        {
            get => _earth;
            set => IsDirty |= SetProperty(ref _earth, value);
        }

        private int _fire = 1;
        public int Fire
        {
            get => _fire;
            set => IsDirty |= SetProperty(ref _fire, value);
        }

        private int _water = 1;
        public int Water
        {
            get => _water;
            set => IsDirty |= SetProperty(ref _water, value);
        }

        private int _void = 1;
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

        // Societal
        private int _honor;
        public int Honor
        {
            get => _honor;
            set => IsDirty |= SetProperty(ref _honor, value);
        }

        private int _glory;
        public int Glory
        {
            get => _glory;
            set => IsDirty |= SetProperty(ref _glory, value);
        }

        private int _status;
        public int Status
        {
            get => _status;
            set => IsDirty |= SetProperty(ref _status, value);
        }

        // Personal
        private int _endurance = 1;
        public int Endurance
        {
            get => _endurance;
            set => IsDirty |= SetProperty(ref _endurance, value);
        }

        private int _composure = 1;
        public int Composure
        {
            get => _composure;
            set => IsDirty |= SetProperty(ref _composure, value);
        }

        private int _focus = 1;
        public int Focus
        {
            get => _focus;
            set => IsDirty |= SetProperty(ref _focus, value);
        }

        private int _vigilance;
        public int Vigilance
        {
            get => _vigilance;
            set => IsDirty |= SetProperty(ref _vigilance, value);
        }

        // Sub elements
        private Demeanor _demeanor;
        public IDemeanor Demeanor => _demeanor;

        private ObservableCollection<Advantage> _advantages;
        public IEnumerable<IAdvantage> Advantages => _advantages;

        private ObservableCollection<Disadvantage> _disadvantages;
        public IEnumerable<IDisadvantage> Disadvantages => _disadvantages;

        private ObservableCollection<Gear> _favoredWeapons;
        public IEnumerable<IGear> FavoredWeapons => _favoredWeapons;

        private ObservableCollection<Gear> _equipedGear;
        public IEnumerable<IGear> EquippedGear => _equipedGear;

        private ObservableCollection<Gear> _otherGear;
        public IEnumerable<IGear> OtherGear => _otherGear;

        private ObservableCollection<Ability> _abilities;
        public IEnumerable<IAbility> Abilities => _abilities;

        // Methods
        public IAdvantage AddAdvantage()
        {
            var advantage = new Advantage();
            _advantages.Add(advantage);
            return advantage;
        }

        public void RemoveAdvantage(IAdvantage advantage)
        {
            RemoveElement(_advantages, advantage);
        }

        public IDisadvantage AddDisadvantage()
        {
            var disadvantage = new Disadvantage();
            _disadvantages.Add(disadvantage);
            return disadvantage;
        }

        public void RemoveDisadvantage(IDisadvantage disadvantage)
        {
            RemoveElement(_disadvantages, disadvantage);
        }

        public IGear AddFavoredWeapon()
        {
            var weapon = new Gear
            {
                GearType = GearType.Weapon
            };

            _favoredWeapons.Add(weapon);
            return weapon;
        }

        public void RemoveFavoredWeapon(IGear gear)
        {
            RemoveElement(_favoredWeapons, gear);
        }

        public IGear AddEquipedGear()
        {
            var gear = new Gear();
            _equipedGear.Add(gear);
            return gear;
        }

        public void RemoveEquipedGear(IGear gear)
        {
            RemoveElement(_equipedGear, gear);
        }

        public IGear AddOtherGear()
        {
            var gear = new Gear();
            _otherGear.Add(gear);
            return gear;
        }

        public void RemoveOtherGear(IGear gear)
        {
            RemoveElement(_otherGear, gear);
        }

        public IAbility AddAbility()
        {
            var ability = new Ability();
            _abilities.Add(ability);
            return ability;
        }

        public void RemoveAbility(IAbility ability)
        {
            RemoveElement(_abilities, ability);
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
                if (t.type != ObjectType.Character)
                {
                    throw new ArgumentException("Character.FromXml: xml is not a character.");
                }

                return t.id.HasValue ? new Character(t.id.Value) : new Character();
            });
        }

        public override XElement CreateXml(bool external = false)
        {
            var xml = base.CreateXml(external);
            xml.Add(new XElement("CharacterData",
                                 new XElement("CharacterType", CharacterType),
                                 new XElement("Description", Description),
                                 CreateConflictRanksXml(),
                                 CreateRingsXml(),
                                 CreateSkillsXml(),
                                 CreateSocietalXml(),
                                 CreatePersonalXml(),
                                 new XElement("Demeanor", _demeanor.CreateXml(true)),
                                 CreateCollection("Advantages", _advantages),
                                 CreateCollection("Disadvantages", _disadvantages),
                                 CreateCollection("FavoredWeapons", _favoredWeapons),
                                 CreateCollection("EquippedGear", _equipedGear),
                                 CreateCollection("OtherGear", _otherGear),
                                 CreateCollection("Abilities", _abilities)));
            return xml;
        }

        protected override void LoadXml(XElement xml)
        {
            base.LoadXml(xml);

            XElement characterData = xml.Element("CharacterData");

            CharacterType = (CharacterType)Enum.Parse(typeof(CharacterType), characterData.Element("CharacterType").Value);
            Description = characterData.Element("Description").Value.Replace("\n", Environment.NewLine);

            LoadConflictRanksXml(characterData);
            LoadRingsXml(characterData);
            LoadSkillsXml(characterData);
            LoadSocietalXml(characterData);
            LoadPersonalXml(characterData);

            _demeanor = GameObjects.Demeanor.FromXml(characterData.Element("Demeanor").Elements().First());

            LoadCollection(characterData, "Advantages", _advantages, x => Advantage.FromXml(x));
            LoadCollection(characterData, "Disadvantages", _disadvantages, x => Disadvantage.FromXml(x));
            LoadCollection(characterData, "FavoredWeapons", _favoredWeapons, x => Gear.FromXml(x));
            LoadCollection(characterData, "EquippedGear", _equipedGear, x => Gear.FromXml(x));
            LoadCollection(characterData, "OtherGear", _otherGear, x => Gear.FromXml(x));
            LoadCollection(characterData, "Abilities", _abilities, x => Ability.FromXml(x));
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

        private XElement CreateSocietalXml()
        {
            var societal = new XElement("Societal",
                                        new XAttribute("HasSocietal", HasSocietalValues));

            if (HasSocietalValues)
            {
                societal.Add(new XElement("Honor", Honor),
                             new XElement("Glory", Glory),
                             new XElement("Status", Status));
            }

            return societal;
        }

        private void LoadSocietalXml(XElement xml)
        {
            XElement societalData = xml.Element("Societal");

            HasSocietalValues = bool.Parse(societalData.Attribute("HasSocietal").Value);

            if (HasSocietalValues)
            {
                Honor = int.Parse(societalData.Element("Honor").Value);
                Glory = int.Parse(societalData.Element("Glory").Value);
                Status = int.Parse(societalData.Element("Status").Value);
            }
        }

        private XElement CreatePersonalXml()
        {
            return new XElement("Personal",
                                new XElement("Endurance", Endurance),
                                new XElement("Composure", Composure),
                                new XElement("Focus", Focus),
                                new XElement("Vigilance", Vigilance));
        }

        private void LoadPersonalXml(XElement xml)
        {
            XElement personalData = xml.Element("Personal");

            Endurance = int.Parse(personalData.Element("Endurance").Value);
            Composure = int.Parse(personalData.Element("Composure").Value);
            Focus = int.Parse(personalData.Element("Focus").Value);
            Vigilance = int.Parse(personalData.Element("Vigilance").Value);
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
