using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class DestroyComponent : MonoBehaviour
    {

        [SerializeField] private GameObject _objectToDestroy;
        public void Destroy()
        {
            Destroy(_objectToDestroy);
        }
    }
}
