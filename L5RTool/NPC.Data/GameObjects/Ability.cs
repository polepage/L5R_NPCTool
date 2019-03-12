using System;
using System.Xml.Linq;

namespace NPC.Data.GameObjects
{
    class Ability : GameObjectData, IAbility
    {
        private string _content;
        public string Content
        {
            get => _content;
            set => IsDirty |= SetProperty(ref _content, value);
        }

        public override XElement CreateXML()
        {
            return new XElement("AbilityData", Content);
        }

        public override void LoadXML(XElement xml)
        {
            Content = xml.Element("AbilityData").Value.Replace("\n", Environment.NewLine);
        }
    }
}
