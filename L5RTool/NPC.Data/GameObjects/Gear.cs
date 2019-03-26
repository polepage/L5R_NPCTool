using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NPC.Common;

namespace NPC.Data.GameObjects
{
    class Gear : GameObject, IGear
    {
        public Gear()
            : base(ObjectType.Equipment)
        {
        }

        private Gear(Guid id)
            : base(id, ObjectType.Equipment)
        {
        }

        private GearType _gearType;
        public GearType GearType
        {
            get => _gearType;
            set => IsDirty |= SetProperty(ref _gearType, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => IsDirty |= SetProperty(ref _description, value);
        }

        public static Gear FromXml(XElement xml)
        {
            return (Gear)FromXml(xml, t =>
            {
                if (t.type != ObjectType.Equipment)
                {
                    throw new ArgumentException("Gear.FromXml: xml is not an equipment.");
                }

                return t.id.HasValue ? new Gear(t.id.Value) : new Gear();
            });
        }

        public override XElement CreateXml(bool external = false)
        {
            var xml = base.CreateXml(external);
            xml.Add(new XAttribute(XmlTools.Version, "xml_1.0"),
                    new XElement("GearData",
                                 new XElement("Description", Description),
                                 new XElement("GearType", GearType)));

            return xml;
        }

        protected override void LoadXml(XElement xml)
        {
            base.LoadXml(xml);

            XElement gearData = xml.Element("GearData");

            Description = gearData.Element("Description").Value.Replace("\n", Environment.NewLine);
            GearType = (GearType)Enum.Parse(typeof(GearType), gearData.Element("GearType").Value);
        }

        protected override IEnumerable<string> ExtractKeywords()
        {
            return new List<string>(base.ExtractKeywords())
            {
                GearType.ToString()
            };
        }
    }
}
