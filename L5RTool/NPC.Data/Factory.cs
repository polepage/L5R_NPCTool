using NPC.Common;
using NPC.Data.GameObjects;
using System;

namespace NPC.Data
{
    class Factory : IFactory
    {
        public IGameObject Create(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Character:
                    return new Character();
                case ObjectType.Demeanor:
                    return new Demeanor();
                case ObjectType.Advantage:
                    return new Advantage();
                case ObjectType.Disadvantage:
                    return new Disadvantage();
                case ObjectType.Ability:
                    return new Ability();
                case ObjectType.Equipment:
                    return new Gear();
                case ObjectType.Template:
                    return new Template();
                default:
                    throw new ArgumentException("Create GameObject: unknown type.");
            }
        }
    }
}
