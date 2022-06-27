using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Definitions.Repositories
{
    public class DefRepository <TDefType> : ScriptableObject where TDefType : IHaveId
    {
        [SerializeField] protected TDefType[] _items;

        public TDefType Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                return default;
            if (string.IsNullOrEmpty(id)) return default;

            foreach (var item in _items)
            {
                if (item.Id == id)
                    return item;
            }
            return default;
        }
    }
}
