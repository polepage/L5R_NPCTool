using System.Collections.Generic;
using CS.Utils.Collections;
using NPC.Common;
using System.Linq;

namespace NPC.Presenter.GameObjects
{
    class Character: GameObject, ICharacter
    {
        private Data.GameObjects.ICharacter _source;

        public Character(Data.GameObjects.ICharacter source)
            : base(source)
        {
            _source = source;

            Demeanor = _source.Demeanor.CreatePresenter() as IDemeanor;
            Advantages = new EnumerableWrapper<IAdvantage, Data.GameObjects.IAdvantage>(_source.Advantages, a => a.CreatePresenter() as IAdvantage);
            Disadvantages = new EnumerableWrapper<IDisadvantage, Data.GameObjects.IDisadvantage>(_source.Disadvantages, d => d.CreatePresenter() as IDisadvantage);
            FavoredWeapons = new EnumerableWrapper<IGear, Data.GameObjects.IGear>(_source.FavoredWeapons, w => w.CreatePresenter() as IGear);
            EquippedGear = new EnumerableWrapper<IGear, Data.GameObjects.IGear>(_source.EquippedGear, e => e.CreatePresenter() as IGear);
            OtherGear = new EnumerableWrapper<IGear, Data.GameObjects.IGear>(_source.OtherGear, o => o.CreatePresenter() as IGear);
            Abilities = new EnumerableWrapper<IAbility, Data.GameObjects.IAbility>(_source.Abilities, a => a.CreatePresenter() as IAbility);
        }

        public CharacterType CharacterType
        {
            get => _source.CharacterType;
            set => _source.CharacterType = value;
        }

        public bool HasSocietalValues
        {
            get => _source.HasSocietalValues;
            set => _source.HasSocietalValues = value;
        }

        public int CombatConflictRank
        {
            get => _source.CombatConflictRank;
            set => _source.CombatConflictRank = value;
        }

        public int IntrigueConflictRank
        {
            get => _source.IntrigueConflictRank;
            set => _source.IntrigueConflictRank = value;
        }

        public string Description
        {
            get => _source.Description;
            set => _source.Description = value;
        }

        public int Air
        {
            get => _source.Air;
            set => _source.Air = value;
        }

        public int Earth
        {
            get => _source.Earth;
            set => _source.Earth = value;
        }

        public int Fire
        {
            get => _source.Fire;
            set => _source.Fire = value;
        }

        public int Water
        {
            get => _source.Water;
            set => _source.Water = value;
        }

        public int Void
        {
            get => _source.Void;
            set => _source.Void = value;
        }

        public int Artisan
        {
            get => _source.Artisan;
            set => _source.Artisan = value;
        }

        public int Martial
        {
            get => _source.Martial;
            set => _source.Martial = value;
        }

        public int Scholar
        {
            get => _source.Scholar;
            set => _source.Scholar = value;
        }

        public int Social
        {
            get => _source.Social;
            set => _source.Social = value;
        }

        public int Trade
        {
            get => _source.Trade;
            set => _source.Trade = value;
        }

        public int Honor
        {
            get => _source.Honor;
            set => _source.Honor = value;
        }

        public int Glory
        {
            get => _source.Glory;
            set => _source.Glory = value;
        }

        public int Status
        {
            get => _source.Status;
            set => _source.Status = value;
        }

        public int Endurance
        {
            get => _source.Endurance;
            set => _source.Endurance = value;
        }

        public int Composure
        {
            get => _source.Composure;
            set => _source.Composure = value;
        }

        public int Focus
        {
            get => _source.Focus;
            set => _source.Focus = value;
        }

        public int Vigilance
        {
            get => _source.Vigilance;
            set => _source.Vigilance = value;
        }

        public IDemeanor Demeanor { get; }
        public IEnumerable<IAdvantage> Advantages { get; }
        public IEnumerable<IDisadvantage> Disadvantages { get; }
        public IEnumerable<IGear> FavoredWeapons { get; }
        public IEnumerable<IGear> EquippedGear { get; }
        public IEnumerable<IGear> OtherGear { get; }
        public IEnumerable<IAbility> Abilities { get; }

        public IAbility AddAbility()
        {
            return (IAbility)_source.AddAbility().CreatePresenter();
        }

        public IAdvantage AddAdvantage()
        {
            return (IAdvantage)_source.AddAdvantage().CreatePresenter();
        }

        public IDisadvantage AddDisadvantage()
        {
            return (IDisadvantage)_source.AddDisadvantage().CreatePresenter();
        }

        public IGear AddEquipedGear()
        {
            return (IGear)_source.AddEquipedGear().CreatePresenter();
        }

        public IGear AddFavoredWeapon()
        {
            return (IGear)_source.AddFavoredWeapon().CreatePresenter();
        }

        public IGear AddOtherGear()
        {
            return (IGear)_source.AddOtherGear().CreatePresenter();
        }

        public void RemoveAbility(IAbility ability)
        {
            _source.RemoveAbility(ability.GetSource() as Data.GameObjects.IAbility);
        }

        public void RemoveAdvantage(IAdvantage advantage)
        {
            _source.RemoveAdvantage(advantage.GetSource() as Data.GameObjects.IAdvantage);
        }

        public void RemoveDisadvantage(IDisadvantage disadvantage)
        {
            _source.RemoveDisadvantage(disadvantage.GetSource() as Data.GameObjects.IDisadvantage);
        }

        public void RemoveEquipedGear(IGear gear)
        {
            _source.RemoveEquipedGear(gear.GetSource() as Data.GameObjects.IGear);
        }

        public void RemoveFavoredWeapon(IGear gear)
        {
            _source.RemoveFavoredWeapon(gear.GetSource() as Data.GameObjects.IGear);
        }

        public void RemoveOtherGear(IGear gear)
        {
            _source.RemoveOtherGear(gear.GetSource() as Data.GameObjects.IGear);
        }

        public void ApplyTemplate(ITemplate template,
                                  IEnumerable<IAdvantage> removedAdvantages, IEnumerable<IAdvantage> newAdvantages,
                                  IEnumerable<IDisadvantage> removedDisadvantages, IEnumerable<IDisadvantage> newDisadvantages,
                                  IEnumerable<IAbility> newAbilities, IDemeanor newDemeanor)
        {
            CombatConflictRank += template.CombatConflictRank;
            IntrigueConflictRank += template.IntrigueConflictRank;

            Air += template.Air;
            Earth += template.Earth;
            Fire += template.Fire;
            Water += template.Water;
            Void += template.Void;

            Artisan += template.Artisan;
            Martial += template.Martial;
            Scholar += template.Scholar;
            Social += template.Social;
            Trade += template.Trade;

            foreach (var advantage in removedAdvantages)
            {
                RemoveAdvantage(advantage);
            }

            foreach (var advantage in newAdvantages)
            {
                var newAdvantage = (Advantage)AddAdvantage();
                newAdvantage.CopyData(advantage);
            }

            foreach (var disadvantage in removedDisadvantages)
            {
                RemoveDisadvantage(disadvantage);
            }

            foreach (var disadvantage in newDisadvantages)
            {
                var newDisadvantage = (Disadvantage)AddDisadvantage();
                newDisadvantage.CopyData(disadvantage);
            }

            foreach (var ability in newAbilities)
            {
                var newAbility = (Ability)AddAbility();
                newAbility.CopyData(ability);
            }

            (Demeanor as Demeanor)?.CopyData(newDemeanor);
        }

        public override void CopyData(IGameObject copySource)
        {
            base.CopyData(copySource);
            if (copySource is ICharacter character)
            {
                CharacterType = character.CharacterType;
                HasSocietalValues = character.HasSocietalValues;
                CombatConflictRank = character.CombatConflictRank;
                IntrigueConflictRank = character.IntrigueConflictRank;
                Description = character.Description;
                Air = character.Air;
                Earth = character.Earth;
                Fire = character.Fire;
                Water = character.Water;
                Void = character.Void;
                Artisan = character.Artisan;
                Martial = character.Martial;
                Scholar = character.Scholar;
                Social = character.Social;
                Trade = character.Trade;
                Honor = character.Honor;
                Glory = character.Glory;
                Status = character.Status;
                Endurance = character.Endurance;
                Composure = character.Composure;
                Focus = character.Focus;
                Vigilance = character.Vigilance;

                (Demeanor as Demeanor).CopyData(character.Demeanor);

                foreach (var advantage in character.Advantages)
                {
                    var newAdvantage = (Advantage)AddAdvantage();
                    newAdvantage.CopyData(advantage);
                }

                foreach (var disadvantage in character.Disadvantages)
                {
                    var newDisadvantage = (Disadvantage)AddDisadvantage();
                    newDisadvantage.CopyData(disadvantage);
                }

                foreach (var weapon in character.FavoredWeapons)
                {
                    var newWeapon = (Gear)AddFavoredWeapon();
                    newWeapon.CopyData(weapon);
                }

                foreach (var gear in character.EquippedGear)
                {
                    var newGear = (Gear)AddEquipedGear();
                    newGear.CopyData(gear);
                }

                foreach (var gear in character.OtherGear)
                {
                    var newGear = (Gear)AddOtherGear();
                    newGear.CopyData(gear);
                }

                foreach (var ability in character.Abilities)
                {
                    var newAbility = (Ability)AddAbility();
                    newAbility.CopyData(ability);
                }
            }
        }

        protected override void RegisterBindings()
        {
            base.RegisterBindings();
            AddBinding(nameof(_source.CharacterType), nameof(CharacterType));
            AddBinding(nameof(_source.HasSocietalValues), nameof(HasSocietalValues));
            AddBinding(nameof(_source.CombatConflictRank), nameof(CombatConflictRank));
            AddBinding(nameof(_source.IntrigueConflictRank), nameof(IntrigueConflictRank));
            AddBinding(nameof(_source.Description), nameof(Description));
            AddBinding(nameof(_source.Air), nameof(Air));
            AddBinding(nameof(_source.Earth), nameof(Earth));
            AddBinding(nameof(_source.Fire), nameof(Fire));
            AddBinding(nameof(_source.Water), nameof(Water));
            AddBinding(nameof(_source.Void), nameof(Void));
            AddBinding(nameof(_source.Artisan), nameof(Artisan));
            AddBinding(nameof(_source.Martial), nameof(Martial));
            AddBinding(nameof(_source.Scholar), nameof(Scholar));
            AddBinding(nameof(_source.Social), nameof(Social));
            AddBinding(nameof(_source.Trade), nameof(Trade));
            AddBinding(nameof(_source.Honor), nameof(Honor));
            AddBinding(nameof(_source.Glory), nameof(Glory));
            AddBinding(nameof(_source.Status), nameof(Status));
            AddBinding(nameof(_source.Endurance), nameof(Endurance));
            AddBinding(nameof(_source.Composure), nameof(Composure));
            AddBinding(nameof(_source.Focus), nameof(Focus));
            AddBinding(nameof(_source.Vigilance), nameof(Vigilance));
        }
    }
}
