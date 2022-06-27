using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components.ColliderBased
{
    public class CheckSphereComponent :  MonoBehaviour
    {
        [SerializeField] private string[] _tags;
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _mask;
        [SerializeField] private OnOverlapEvent _onOverlap;
        [SerializeField] private bool _isTouchingLayer = false;
        private Collider[] _interactionResults = new Collider[10];


        public void Check()
        {
            var size = Physics.OverlapSphereNonAlloc(transform.position, _radius, _interactionResults, _mask);
            

            for (var i = 0; i < size; i++)
            {
                var overlapResult = _interactionResults[i];
                var isInTag = _tags.Any(tag => overlapResult.CompareTag(tag));
                if (isInTag)
                {
                    _isTouchingLayer = true;
                    _onOverlap?.Invoke(overlapResult.gameObject);
                } 
            }
        }



#if UNITY_EDITOR
            private void OnDrawGizmos()
            {

                Gizmos.color = new Color(1, 0, 0, 0.2f);
                Gizmos.DrawSphere(transform.position, _radius);
            }
#endif
        }

         [Serializable]
        public class OnOverlapEvent : UnityEvent<GameObject>
        {

        }
}
