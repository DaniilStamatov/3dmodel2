using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components.ColliderBased
{
    public class InteractComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;

        public void DoInteract()
        {
            _action?.Invoke();
        }
    }
}
