using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

namespace Assets.Scripts.Components
{
    public class AttackRangeComponent : MonoBehaviour
    {
        [SerializeField] private float _weaponLength;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private OnHitEvent _onHit;
        [SerializeField] private Cooldown _cooldown;

        private RaycastHit[] _interactionResults = new RaycastHit[1];


        public void FixedUpdate()
        {

                var size = Physics.RaycastNonAlloc(transform.position, -transform.up, _interactionResults, _weaponLength, _layerMask);
                for (var i = 0; i < size; i++)
                {
                    if (_cooldown.IsReady)
                    {
                        var overlapResult = _interactionResults[i];

                        _onHit?.Invoke(overlapResult.collider.gameObject);
                        Debug.Log("damage");
                        _cooldown.Reset();
                    }
                }
            
            
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position, transform.position - transform.up * _weaponLength);
        }

        [Serializable]
        public class OnHitEvent : UnityEvent<GameObject>
        {

        }
    }
}