﻿using NPC.Common;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace NPC.Data.GameObjects
{
    class Ability : GameObject, IAbility
    {
        public Ability()
            : base(ObjectType.Ability)
        {
        }

        private Ability(Guid id)
            : base(id, ObjectType.Ability)
        {
        }

        private AbilityType _abilityType;
        public AbilityType AbilityType
        {
            get => _abilityType;
            set => IsDirty |= SetProperty(ref _abilityType, value);
        }

        private string _content;
        public string Content
        {
            get => _content;
            set => IsDirty |= SetProperty(ref _content, value);
        }

        public static Ability FromXml(XElement xml)
        {
            return (Ability)FromXml(xml, t =>
            {
                if (t.type != ObjectType.Ability)
                {
                    throw new ArgumentException("Ability.FromXml: xml is not an ability.");
                }

                return t.id.HasValue ? new Ability(t.id.Value) : new Ability();
            });
        }

        public override XElement CreateXml(bool external = false)
        {
            var xml = base.CreateXml(external);
            xml.Add(new XAttribute(XmlTools.Version, "xml_1.0"),
                    new XElement("AbilityData",
                                 new XElement("AbilityType", AbilityType),
                                 new XElement("Content", Content)));

            return xml;
        }

        protected override void LoadXml(XElement xml)
        {
            base.LoadXml(xml);

            XElement abilityData = xml.Element("AbilityData");

            AbilityType = (AbilityType)Enum.Parse(typeof(AbilityType), abilityData.Element("AbilityType").Value);
            Content = abilityData.Element("Content").Value.Replace("\n", Environment.NewLine);
        }

        protected override IEnumerable<string> ExtractKeywords()
        {
            return new List<string>(base.ExtractKeywords())
            {
                AbilityType.ToString()
            };
        }
    }
}
