using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Components.Model.Data.Properties
{
    public abstract class PrefsPersistentProperty<TPropertyType> : PersistentProperty<TPropertyType>
    {
        protected string Key;
        public PrefsPersistentProperty(TPropertyType defaultValue, string key) : base(defaultValue)
        {
            Key = key;
        }
    }
}
