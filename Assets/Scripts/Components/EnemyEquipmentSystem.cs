using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class EnemyEquipmentSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponHolder;
        [SerializeField] private GameObject _weapon;
        private GameObject _currentWeaponInHand;

        public GameObject CurrentWeaponInHand => _currentWeaponInHand;

        private void Start()
        {
            _currentWeaponInHand = Instantiate(_weapon, _weaponHolder.transform);
        }
    }
}
