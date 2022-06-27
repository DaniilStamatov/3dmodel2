using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]
    //description of inventory
    public class InventoryItemDef : DefRepository<ItemDef>
    {
        

#if UNITY_EDITOR
        public ItemDef[] ItemsForEditor => _items;
#endif
    }

    //description of object
    [Serializable]
    public struct ItemDef : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private ItemTags[] _tags;
        [SerializeField] private Sprite _icon;

        public Sprite Icon => _icon;

        public string Id => _id;

        public bool IsVoid => string.IsNullOrEmpty(_id);

        public bool HasTag(ItemTags tag)
        {
            return _tags.Contains(tag);
        }
    }
}
