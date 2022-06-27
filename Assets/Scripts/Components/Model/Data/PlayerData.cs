using Assets.Scripts.Components.Model.Data.Properties;
using System;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private InventoryData _inventory;
        public InventoryData Inventory => _inventory;

        public IntProperty Hp;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}
