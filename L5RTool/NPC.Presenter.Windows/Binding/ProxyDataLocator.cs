using System;
using System.Collections.Generic;

namespace NPC.Presenter.Windows.Binding
{
    static class ProxyDataLocator
    {
        private static Dictionary<string, Func<object>> _factories = new Dictionary<string, Func<object>>();

        public static void Register<T>(Func<object> factory)
        {
            string type = typeof(T).ToString();
            if (_factories.ContainsKey(type))
            {
                throw new ArgumentException($"Type {type} was already registered.");
            }

            _factories.Add(type, factory);
        }

        public static T Resolve<T>(object target)
        {
            if (_factories.TryGetValue(target.GetType().ToString(), out var factory))
            {
                return (T)factory();
            }

            throw new ArgumentException($"Type {target.GetType().ToString()} was not registered");
        }
    }
}
