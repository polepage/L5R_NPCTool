using System;

namespace NPC.Presenter.GameObjects
{
    static class GameObjectExtension
    {
        public static Data.GameObjects.IGameObject GetSource(this IGameObject gameObject)
        {
            if (gameObject is GameObject go)
            {
                return go.Source;
            }

            throw new ArgumentException("IGameObject is not a GameObject.");
        }

        public static Data.GameObjects.IGameObjectMetadata GetSource(this IGameObjectMetadata gameObject)
        {
            if (gameObject is GameObjectMetadata go)
            {
                return go.Source;
            }

            throw new ArgumentException("IGameObjectMetadata is not a GameObjectMetadata.");
        }

        public static Data.GameObjects.IGameObjectReference GetSource(this IGameObjectReference gameObject)
        {
            if (gameObject is GameObject go)
            {
                return go.Source;
            }
            else if (gameObject is GameObjectMetadata gom)
            {
                return gom.Source;
            }

            throw new ArgumentException("IGameObjectReference is not a GameObject or a GameObjectMetadata.");
        }

        public static IGameObject CreatePresenter(this Data.GameObjects.IGameObject gameObject)
        {
            switch (gameObject)
            {
                case Data.GameObjects.ICharacter s:
                    return new Character(s);
                case Data.GameObjects.IDemeanor s:
                    return new Demeanor(s);
                case Data.GameObjects.IAdvantage s:
                    return new Advantage(s);
                case Data.GameObjects.IDisadvantage s:
                    return new Disadvantage(s);
                case Data.GameObjects.IGear s:
                    return new Gear(s);
                case Data.GameObjects.IAbility s:
                    return new Ability(s);
                default:
                    throw new ArgumentException("Create Presenter: Unknown type");
            }
        }

        public static IGameObjectMetadata CreatePresenter(this Data.GameObjects.IGameObjectMetadata gameObject)
        {
            return new GameObjectMetadata(gameObject);
        }
    }
}
