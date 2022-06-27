using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Components.Model.Data.Properties
{
    [Serializable]
    public class FloatPersistentProperty : PrefsPersistentProperty<float>
    {
        public FloatPersistentProperty(float defaultValue,string key) : base(defaultValue , key)
        {
            Init();
        }

        public override float Read(float defaultValue)
        {
            return PlayerPrefs.GetFloat(Key, defaultValue);
        }

        public override void Write(float value)
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }
    }
}
