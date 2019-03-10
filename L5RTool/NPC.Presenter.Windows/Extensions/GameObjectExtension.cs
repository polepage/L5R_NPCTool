﻿using NPC.Presenter.GameObjects;
using System;

namespace NPC.Presenter.Windows.Extensions
{
    static class GameObjectExtension
    {
        public static Business.GameObjects.IGameObject GetSource(this IGameObject gameObject)
        {
            if (gameObject is GameObject go)
            {
                return go.Source;
            }

            throw new ArgumentException("IGameObject is not a GameObject.");
        }

        public static Business.GameObjects.IGameObjectMetadata GetSource(this IGameObjectMetadata gameObject)
        {
            if (gameObject is GameObjectMetadata go)
            {
                return go.Source;
            }

            throw new ArgumentException("IGameObjectMetadata is not a GameObjectMetadata.");
        }

        public static Business.GameObjects.IGameObjectReference GetSource(this IGameObjectReference gameObject)
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
