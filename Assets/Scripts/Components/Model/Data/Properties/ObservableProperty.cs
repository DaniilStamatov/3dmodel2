using Assets.Scripts.Components.Utils.Disposables;
using System;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Data.Properties
{
    [Serializable]
    public class ObservableProperty<TPropertyType>
    {
        [SerializeField] protected TPropertyType _value;

        public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
        public event OnPropertyChanged OnChanged;

        public IDisposable Subscribe(OnPropertyChanged onChanged)
        {
            OnChanged += onChanged;
            return new ActionDisposable(() => OnChanged -= onChanged);
        }

        public IDisposable SubscribeAndInvoke(OnPropertyChanged onChanged)
        {
            OnChanged += onChanged;
            var dispose = new ActionDisposable(() => OnChanged -= onChanged);
            onChanged(_value, _value);
            return dispose;
        }

        public virtual TPropertyType Value
        {
            get => _value;
            set
            {
                var isEquals = _value.Equals(value);
                if (isEquals) return;


                var oldValue = _value;
                _value = value;

                InvokeChangedEvent(_value, oldValue);
            }
        }

        protected void InvokeChangedEvent(TPropertyType newValue, TPropertyType oldValue)
        {
            OnChanged?.Invoke(newValue, oldValue);

        }
    }
}
