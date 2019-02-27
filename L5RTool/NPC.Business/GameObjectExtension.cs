using NPC.Business.GameObjects;
using System;

namespace NPC.Business
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
    }
}
