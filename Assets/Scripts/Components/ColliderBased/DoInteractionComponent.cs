
using System;
using UnityEngine;

namespace Assets.Scripts.Components.ColliderBased
{
    [Serializable]
    public class DoInteractionComponent : MonoBehaviour
    {
        [SerializeField] private bool _interactableSet;
        public void DoInteraction(GameObject go)
        {
            var interactable = go.GetComponent<InteractComponent>();
            if (interactable != null)
            {
                _interactableSet = true;
                interactable.DoInteract();
            }
        }
    }
}
