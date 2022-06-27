using Assets.Scripts.Components.Model.Definitions;
using Assets.Scripts.Components.Model.Definitions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Data
{
    [Serializable]
    //перечень предметов в инвентаре
    public class InventoryData
    {
        [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();
        public delegate void OnInventoryChanged(string id, int value);

        public OnInventoryChanged OnChanged;
        public void Add(string id, int value)
        {
            if(value <= 0 ) return;
            
            var itemDef = DefsFacade.Instance.Items.Get(id);
            if (itemDef.IsVoid) return;
            
            if (itemDef.HasTag(ItemTags.Stackable))
            {
                AddToStack(id,value);
            }
            else
            {
                AddNonStack(id,value);
            }
            OnChanged?.Invoke(id, Count(id));
        }

        public void Remove(string id, int value)
        {
            var itemDef = DefsFacade.Instance.Items.Get(id);
            if (itemDef.IsVoid) return;

           if (itemDef.HasTag(ItemTags.Stackable))
            {
                RemoveFromStack(id,value);
            }
           else
            {
                RemoveNonStack(id,value);
            }
            
            OnChanged?.Invoke(id, Count(id));
        }

        public void RemoveNonStack(string id, int value)
        {
            for (var i = 0; i < value; i++)
            {
                var item = GetItem(id);
                if(item == null) return;

                _inventory.Remove(item);
            }
        }

        public void AddNonStack(string id, int value)
        {
            var itemLasts = DefsFacade.Instance.Player.InventorySize - _inventory.Count;
            value = Mathf.Min(itemLasts, value);
            for(var i = 0; i < value; i++)
            {
                var item = new InventoryItemData(id) { Value = 1 };
                _inventory.Add(item);
            }
        }
        
        public void AddToStack(string id, int value)
        {
            var isFull = _inventory.Count >= DefsFacade.Instance.Player.InventorySize;
            var item = GetItem(id);
            if (item == null)
            {
                if (isFull) return;
                item = new InventoryItemData(id);
                _inventory.Add(item);
            }

            item.Value += value;
        }

        public void RemoveFromStack(string id, int value)
        {
            var item = GetItem(id);
            if (item == null) return;

            if (item.Value > 0)
            {
                item.Value-=value;
            }
            if (item.Value <= 0)
                 _inventory.Remove(item);
        }



        public int Count(string id)
        {
            var count = 0;
            foreach (var item in _inventory)
            {
                if (item.Id == id)
                {
                    count += item.Value;
                }
            }
            return count;
        }

        private InventoryItemData GetItem(string id)
        {
            foreach (var item in _inventory)
            {
                if(item.Id == id)
                    return item;
            }
            return null;
        }

        public InventoryItemData[] GetAll(params ItemTags[] tag)
        {
            var returnValue = new List<InventoryItemData>();
            foreach (var item in _inventory)
            {
                var itemDef = DefsFacade.Instance.Items.Get(item.Id);
                var allRequirementsMet = tag.All(x => itemDef.HasTag(x));
                if (allRequirementsMet)
                {
                    returnValue.Add(item);
                }
            }
            return returnValue.ToArray();
        }

    }

    [Serializable]

    //предмет в инвентаре
    public class InventoryItemData
    {
        [InventoryIdAtribute] public string Id;
        public int Value;

        public InventoryItemData(string id)
        {
            Id = id;
        }
    }
}
