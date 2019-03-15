﻿using NPC.Common;
using NPC.Presenter.GameObjects;
using System.Windows;
using System.Windows.Controls;

namespace NPC.Presenter.Windows.Templates
{
    class EditorTemplateSelector: DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate DemeanorTemplate { get; set; }
        public DataTemplate TraitTemplate { get; set; }
        public DataTemplate AbilityTemplate { get; set; }
        public DataTemplate GearTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is IGameObject gameObject)
            {
                switch (gameObject.Type)
                {
                    case ObjectType.Demeanor:
                        return DemeanorTemplate;
                    case ObjectType.Advantage:
                    case ObjectType.Disadvantage:
                        return TraitTemplate;
                    case ObjectType.Ability:
                        return AbilityTemplate;
                    case ObjectType.Equipment:
                        return GearTemplate;
                    default:
                        break;
                }
            }

            return DefaultTemplate;
        }
    }
}