using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private bool _isTouchingLayer;

        public bool Check()
        {
            _isTouchingLayer = Physics.Raycast(transform.position, Vector3.down*0.2f, 0.3f , _layer);
            return _isTouchingLayer;    
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            UnityEditor.Handles.DrawLine(this.transform.position, Vector3.down * 0.5f);
        }


#endif

    }
}
