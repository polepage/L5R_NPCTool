using System;
using System.Xml.Linq;
using NPC.Common;

namespace NPC.Data.GameObjects
{
    class Gear : GameObjectData, IGear
    {
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

        public override XElement CreateXML()
        {
            return new XElement("TraitData",
                                new XElement("Description", Description),
                                new XElement("GearType", GearType));
        }

        public override void LoadXML(XElement xml)
        {
            XElement traitData = xml.Element("TraitData");

            Description = traitData.Element("Description").Value;
            GearType = (GearType)Enum.Parse(typeof(GearType), traitData.Element("GearType").Value);
        }
    }
}
