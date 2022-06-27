using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components.ColliderBased
{
    public class EnterTriggerComponent : MonoBehaviour
    {

        [SerializeField] private InterEvent _event;
        [SerializeField] private string _tag;
        [SerializeField] private LayerMask _layer;

        private void OnTriggerEnter(Collider other)
        {
            //if (!string.IsNullOrEmpty(_tag) && !other.gameObject.CompareTag(_tag)) return;
            if (other.gameObject.CompareTag(_tag))
            {
                _event?.Invoke(other.gameObject);
            }
        }

    }
    [Serializable]
    public class InterEvent : UnityEvent<GameObject>
    {
    }
}
