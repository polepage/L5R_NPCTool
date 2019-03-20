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

        public void AddAbility()
        {
            _source.AddAbility();
        }

        public void AddAdvantage()
        {
            _source.AddAdvantage();
        }

        public void AddDisadvantage()
        {
            _source.AddDisadvantage();
        }

        public void AddEquipedGear()
        {
            _source.AddEquipedGear();
        }

        public void AddFavoredWeapon()
        {
            _source.AddFavoredWeapon();
        }

        public void AddOtherGear()
        {
            _source.AddOtherGear();
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

                foreach (var advantage in character.Advantages) { AddAdvantage(); }
                Advantages.Zip(character.Advantages, (a, source) =>
                {
                    (a as Advantage).CopyData(source);
                    return a;
                });

                foreach (var disadvantage in character.Disadvantages) { AddDisadvantage(); }
                Disadvantages.Zip(character.Disadvantages, (d, source) =>
                {
                    (d as Disadvantage).CopyData(source);
                    return d;
                });

                foreach (var weapon in character.FavoredWeapons) { AddFavoredWeapon(); }
                FavoredWeapons.Zip(character.FavoredWeapons, (f, source) =>
                {
                    (f as Gear).CopyData(source);
                    return f;
                });

                foreach (var gear in character.EquippedGear) { AddEquipedGear(); }
                EquippedGear.Zip(character.EquippedGear, (e, source) =>
                {
                    (e as Gear).CopyData(source);
                    return e;
                });

                foreach (var gear in character.OtherGear) { AddOtherGear(); }
                OtherGear.Zip(character.OtherGear, (o, source) =>
                {
                    (o as Gear).CopyData(source);
                    return o;
                });

                foreach (var ability in character.Abilities) { AddAbility(); }
                Abilities.Zip(character.Abilities, (a, source) =>
                {
                    (a as Ability).CopyData(source);
                    return a;
                });
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
