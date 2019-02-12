using Prism.Mvvm;
using System.Collections.Generic;
using System.ComponentModel;

namespace CS.Utils.Prism.Mvvm
{
    public abstract class RelayBindableBase: BindableBase
    {
        private IDictionary<string, ISet<string>> _bindingMap;

        public RelayBindableBase(INotifyPropertyChanged source)
        {
            _bindingMap = new Dictionary<string, ISet<string>>();
            RegisterBindings();
            source.PropertyChanged += SourcePropertyChanged;
        }

        protected abstract void RegisterBindings();

        protected void AddBinding(string sourceProperty, string targetProperty)
        {
            if (!_bindingMap.Keys.Contains(sourceProperty))
            {
                _bindingMap.Add(sourceProperty, new HashSet<string>());
            }

            _bindingMap[sourceProperty].Add(targetProperty);
        }

        protected void RemoveBinding(string sourceProperty, string targetProperty)
        {
            ISet<string> targets;
            if (_bindingMap.TryGetValue(sourceProperty, out targets))
            {
                targets.Remove(targetProperty);
                if (targets.Count == 0)
                {
                    _bindingMap.Remove(sourceProperty);
                }
            }
        }

        private void SourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ISet<string> targets;
            if (_bindingMap.TryGetValue(e.PropertyName, out targets))
            {
                foreach(string target in targets)
                {
                    RaisePropertyChanged(target);
                }
            }
        }
    }
}
