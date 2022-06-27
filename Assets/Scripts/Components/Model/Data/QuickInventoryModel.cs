using Assets.Scripts.Components.Model.Data.Properties;
using Assets.Scripts.Components.Model.Definitions;
using Assets.Scripts.Components.Model.Definitions.Repositories;
using Assets.Scripts.Components.Utils.Disposables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Data
{
    public class QuickInventoryModel : IDisposable
    {
        private PlayerData _data;

        public InventoryItemData[] Inventory { get; private set; }

        public readonly IntProperty SelectedIndex = new IntProperty();
        public event Action OnChanged;

        public InventoryItemData SelectedItem
        {
            get
            {
                if(Inventory.Length > 0 && Inventory.Length > SelectedIndex.Value)
                    return Inventory[SelectedIndex.Value];

                return null;
            }
        }

        public ItemDef SelectedDef => DefsFacade.Instance.Items.Get(SelectedItem?.Id);


        public IDisposable Subscribe(Action action)
        {
            OnChanged += action;
            return new ActionDisposable(()=>OnChanged -= action);
        }

        public QuickInventoryModel(PlayerData data)
        {
            _data = data;

            Inventory = _data.Inventory.GetAll(ItemTags.Usable);
            _data.Inventory.OnChanged += OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            var indexFound = Array.FindIndex(Inventory, x => x.Id == id);
            if (indexFound != -1)
            {
                Inventory = _data.Inventory.GetAll(ItemTags.Usable);
                SelectedIndex.Value = Mathf.Clamp(SelectedIndex.Value, 0, Inventory.Length - 1);
                OnChanged?.Invoke();
            }
          
        }


        public void SetNextItem()
        {
            SelectedIndex.Value = (int)Mathf.Repeat(SelectedIndex.Value+1, Inventory.Length);
        }

        public void Dispose()
        {
            _data.Inventory.OnChanged -= OnInventoryChanged;
        }

    }
}
