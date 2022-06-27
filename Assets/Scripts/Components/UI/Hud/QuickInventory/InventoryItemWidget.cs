using Assets.Scripts.Components.Model;
using Assets.Scripts.Components.Model.Data;
using Assets.Scripts.Components.Model.Definitions;
using Assets.Scripts.Components.Utils.Disposables;
using Assets.Scripts.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components.UI.Hud.QuickInventory
{
    public class InventoryItemWidget : MonoBehaviour, ItemRenderer<InventoryItemData>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Text _value;
        [SerializeField] private GameObject _container;
        private readonly CompositeDisposable _trash = new CompositeDisposable();


        private int _index;

        private void Start()
        {
            var session = FindObjectOfType<GameSession>();
            var index = session.QuickInventoryModel.SelectedIndex;
           _trash.Retain(index.SubscribeAndInvoke(OnIndexChanged));
        }

        private void OnIndexChanged(int newValue, int _)
        {
            _container.SetActive(_index == newValue);
        }

        public void SetData(InventoryItemData item, int index)
        {
            _index = index;
            var def = DefsFacade.Instance.Items.Get(item.Id);
            _icon.sprite = def.Icon;

            _value.text = def.HasTag(ItemTags.Stackable) ? item.Value.ToString() : string.Empty;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
