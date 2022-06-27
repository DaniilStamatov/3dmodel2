using Assets.Scripts.Components.Model;
using Assets.Scripts.Components.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components.Collectables
{
    public class RequiredItemComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onSuccess;
        [SerializeField] private UnityEvent _onFail;
        [SerializeField] private bool _removeAfterUse;
        [SerializeField] private InventoryItemData[] _requiredItem;

        public void Check()
        {
            var session = GetComponent<GameSession>();
            var allRequirementsMet = true;

            foreach (var requiredItem in _requiredItem)
            {
                var numOfItems = session.Data.Inventory.Count(requiredItem.Id);
                if (numOfItems < requiredItem.Value)
                {
                    allRequirementsMet = false;
                }
            }

            if (allRequirementsMet)
            {
                if (_removeAfterUse)
                {
                    foreach (var requiredItem in _requiredItem)
                        session.Data.Inventory.Remove(requiredItem.Id, requiredItem.Value);
                }
                _onSuccess?.Invoke();
            }

            else
            {
                _onFail?.Invoke();
            }
        }

    }
}
