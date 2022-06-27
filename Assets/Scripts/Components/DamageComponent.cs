using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField] private int _damage;

        public void DealDamage(GameObject go)
        {
            var healthComponent = go.GetComponent<HealthComponent>();

            if (healthComponent != null)
                healthComponent.ModifyHealth(_damage);
        }
    }
}
