using NPC.Common;
using System;
using System.Xml.Linq;

namespace NPC.Data.GameObjects
{
    class Demeanor : GameObject, IDemeanor
    {
        public Demeanor()
            : base(ObjectType.Ability)
        {
        }

        private Demeanor(Guid id)
            : base(id, ObjectType.Ability)
        {
        }

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

        private string _unmasking;
        public string Unmasking
        {
            get => _unmasking;
            set => IsDirty |= SetProperty(ref _unmasking, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => IsDirty |= SetProperty(ref _description, value);
        }

        public static GameObject FromXml(XElement xml)
        {
            return FromXml(xml, t =>
            {
                if (t.type != ObjectType.Demeanor)
                {
                    throw new ArgumentException("Demeanor.FromXml: xml is not a demeanor.");
                }

                return t.id.HasValue ? new Demeanor(t.id.Value) : new Demeanor();
            });
        }

        public override XElement CreateXml(bool external = false)
        {
            var xml = base.CreateXml(external);
            xml.Add(new XElement("DemeanorData",
                                 new XElement("Air", Air),
                                 new XElement("Earth", Earth),
                                 new XElement("Fire", Fire),
                                 new XElement("Water", Water),
                                 new XElement("Void", Void),
                                 new XElement("Unmasking", Unmasking),
                                 new XElement("Description", Description)));

            return xml;
        }

        protected override void LoadXml(XElement xml)
        {
            base.LoadXml(xml);

            XElement demeanorData = xml.Element("DemeanorData");

            Air = int.Parse(demeanorData.Element("Air").Value);
            Earth = int.Parse(demeanorData.Element("Earth").Value);
            Fire = int.Parse(demeanorData.Element("Fire").Value);
            Water = int.Parse(demeanorData.Element("Water").Value);
            Void = int.Parse(demeanorData.Element("Void").Value);
            Unmasking = demeanorData.Element("Unmasking").Value.Replace("\n", Environment.NewLine);
            Description = demeanorData.Element("Description").Value.Replace("\n", Environment.NewLine);
        }
    }
}
