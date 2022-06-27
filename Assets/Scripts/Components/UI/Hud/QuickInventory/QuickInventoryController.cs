using Assets.Scripts.Components.Model;
using Assets.Scripts.Components.Model.Data;
using Assets.Scripts.Components.Utils.Disposables;
using Assets.Scripts.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.UI.Hud.QuickInventory
{
    public class QuickInventoryController : MonoBehaviour
    {
        //adds and deletes items from quickInventory
        [SerializeField] private Transform _container;
        [SerializeField] private InventoryItemWidget _prefab;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private GameSession _session;

        private List<InventoryItemWidget> _createdItems = new List<InventoryItemWidget>();

        private DataGroup<InventoryItemData, InventoryItemWidget> _dataGroup;
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _dataGroup = new DataGroup<InventoryItemData, InventoryItemWidget> (_prefab, _container);
            _trash.Retain(_session.QuickInventoryModel.Subscribe(Rebuilt));
            Rebuilt();
        }

        private void Rebuilt()
        {
            var inventory = _session.QuickInventoryModel.Inventory;
            _dataGroup.SetData(inventory);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
