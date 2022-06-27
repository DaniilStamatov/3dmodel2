using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator _animator;

        protected virtual void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.SetTrigger("show");
        }

        public void Close()
        {
            _animator.SetTrigger("hide");
        }

        public virtual void OnCloseAnimationComplete()
        {
            Destroy(gameObject);
        }
    }
}
