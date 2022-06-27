using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Components.Model.Definitions;

namespace Assets.Scripts.Components.Collectables
{
    public class InventoryAddComponent: MonoBehaviour
    {
        [InventoryIdAtribute][SerializeField] private string _id;
        [SerializeField] private int _value;

        public void Add(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
                hero.AddInInventory(_id, _value);
        }
    }
}
