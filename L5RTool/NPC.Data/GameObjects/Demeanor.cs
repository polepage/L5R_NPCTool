using System;
using System.Xml.Linq;

namespace NPC.Data.GameObjects
{
    class Demeanor : GameObjectData, IDemeanor
    {
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

        public override XElement CreateXML()
        {
            return new XElement("DemeanorData",
                                new XElement("Air", Air),
                                new XElement("Earth", Earth),
                                new XElement("Fire", Fire),
                                new XElement("Water", Water),
                                new XElement("Void", Void),
                                new XElement("Unmasking", Unmasking),
                                new XElement("Description", Description));
        }

        public override void LoadXML(XElement xml)
        {
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
