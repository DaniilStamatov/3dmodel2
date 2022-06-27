using Assets.Scripts.Components.Utils.Disposables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Data.Properties
{
    [Serializable]
    public abstract class PersistentProperty<TPropertyType> :ObservableProperty<TPropertyType>
    {
        protected TPropertyType _defaultValue;
        private TPropertyType _stored;

        public PersistentProperty(TPropertyType defaultValue)
        {
            _defaultValue = defaultValue;
        }

       

        public override TPropertyType Value
        {
            get => _stored;
            set
            {
                var isEquals = _stored.Equals(value);
                if (isEquals) return;


                var oldValue = _stored;
                Write(value);
                _stored = _value = value;

                InvokeChangedEvent(value, oldValue);
            }
        }

        protected void Init()
        {
            _stored= _value = Read(_defaultValue);
        }

        public void Validate()
        {
            if (!_stored.Equals(_value))
            {
                Value = _value;
            }
        }

        public abstract void Write(TPropertyType value);

        public abstract TPropertyType Read(TPropertyType defaultValue);
    }
}
