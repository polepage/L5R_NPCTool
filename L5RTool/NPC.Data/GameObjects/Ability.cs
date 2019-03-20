using NPC.Common;
using System;
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
            xml.Add(new XElement("AbilityData", Content));

            return xml;
        }

        protected override void LoadXml(XElement xml)
        {
            base.LoadXml(xml);
            Content = xml.Element("AbilityData").Value.Replace("\n", Environment.NewLine);
        }
    }
}
