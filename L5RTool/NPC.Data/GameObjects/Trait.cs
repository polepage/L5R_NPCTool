using CS.Utils.Collections;
using NPC.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Linq;

namespace NPC.Data.GameObjects
{
    abstract class Trait : GameObjectData, ITrait
    {
        public Trait()
        {
            SkillGroups = new ObservableHashSet<SkillGroup>();
            Spheres = new ObservableHashSet<TraitSphere>();

            (SkillGroups as INotifyCollectionChanged).CollectionChanged += (sender, args) => IsDirty = true;
            (Spheres as INotifyCollectionChanged).CollectionChanged += (sender, args) => IsDirty = true;
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => IsDirty |= SetProperty(ref _description, value);
        }

        private Ring _ring;
        public Ring Ring
        {
            get => _ring;
            set => IsDirty |= SetProperty(ref _ring, value);
        }

        public ISet<SkillGroup> SkillGroups { get; }
        public ISet<TraitSphere> Spheres { get; }

        public override void LoadXML(XElement xml)
        {
            XElement traitData = xml.Element("TraitData");

            Description = traitData.Element("Description").Value;
            Ring = (Ring)Enum.Parse(typeof(Ring), traitData.Element("Ring").Value);

            foreach (XElement skillGroup in traitData.Element("SkillGroups").Elements())
            {
                SkillGroups.Add((SkillGroup)Enum.Parse(typeof(SkillGroup), skillGroup.Value));
            }

            foreach (XElement sphere in traitData.Element("Spheres").Elements())
            {
                Spheres.Add((TraitSphere)Enum.Parse(typeof(TraitSphere), sphere.Value));
            }
        }

        public override XElement CreateXML()
        {
            return new XElement("TraitData",
                                new XElement("Description", Description),
                                new XElement("Ring", Ring),
                                new XElement("SkillGroups",
                                             SkillGroups.Select(sg => new XElement("Item", sg))),
                                new XElement("Spheres",
                                             Spheres.Select(s => new XElement("Item", s))));
        }

        //protected override void LoadXML(XElement xml)
        //{
        //    Name = xml.Element("Name").Value;
        //    Description = xml.Element("Description").Value;
        //    Ring = (Ring)Enum.Parse(typeof(Ring), xml.Element("Ring").Value);

        //    foreach (XElement skillGroup in xml.Element("SkillGroups").Elements())
        //    {
        //        SkillGroups.Add((SkillGroup)Enum.Parse(typeof(SkillGroup), skillGroup.Value));
        //    }

        //    foreach (XElement sphere in xml.Element("Spheres").Elements())
        //    {
        //        Spheres.Add((TraitSphere)Enum.Parse(typeof(TraitSphere), sphere.Value));
        //    }
        //}
    }
}
