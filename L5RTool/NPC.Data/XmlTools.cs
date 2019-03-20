using NPC.Common;
using NPC.Data.GameObjects;
using System;
using System.Xml.Linq;

namespace NPC.Data
{
    static class XmlTools
    {
        public static readonly string GameObjectNode = "GameObject";
        public static readonly string MetadataNode = "Reference";

        public static GameObject LoadGameObject(this XElement xml)
        {
            if (xml.Name.LocalName != GameObjectNode)
            {
                throw new ArgumentException("Load GameObject: xml is not a GameObject");
            }

            var type = (ObjectType)Enum.Parse(typeof(ObjectType), xml.Attribute("Type").Value);
            switch (type)
            {
                case ObjectType.Character:
                    return Character.FromXml(xml);
                case ObjectType.Demeanor:
                    return Demeanor.FromXml(xml);
                case ObjectType.Advantage:
                    return Advantage.FromXml(xml);
                case ObjectType.Disadvantage:
                    return Disadvantage.FromXml(xml);
                case ObjectType.Ability:
                    return Ability.FromXml(xml);
                case ObjectType.Equipment:
                    return Gear.FromXml(xml);
                default:
                    throw new ArgumentException("Load GameObject: xml GameObject unknown type.");
            }
        }

        public static GameObjectMetadata LoadMetadata(this XElement xml)
        {
            if (xml.Name.LocalName != MetadataNode)
            {
                throw new ArgumentException("Load Metadata: xml is not a GameObjectMetadata.");
            }

            return GameObjectMetadata.FromXml(xml);
        }
    }
}
