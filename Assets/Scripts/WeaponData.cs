using Assets.Scripts.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "Weapon/WeaponType", fileName = "WeaponType")]
    public class WeaponData : ScriptableObject
    {
        public WeaponSlot WeaponType;
        public GameObject Weapon;
    }
}
