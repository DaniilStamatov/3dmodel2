using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Defs/WeaponRepository", fileName = "WeaponRepository")]

    public class WeaponRepository : DefRepository<WeaponDef>
    {
    }

    [Serializable]
    public class WeaponDef : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private GameObject _weapon;
        public string Id => _id;
    }

    public enum WeaponType
    {
        Sword,
        Bow
    }

}
