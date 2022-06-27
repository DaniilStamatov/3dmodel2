using Assets.Scripts.Components;
using Assets.Scripts.Components.Model;
using UnityEngine;

namespace Assets.Scripts
{
    public class EquipmentSystem : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponHolder;
        [SerializeField] private GameObject _weapon;

        [SerializeField] private GameObject _weaponSheath;
        [SerializeField] private Hero _hero;
        private WeaponSlot _weaponSlot;
       
        private GameObject _currentWeaponInHand;

        public GameObject CurrentWeaponInHand => _currentWeaponInHand;
        private GameObject _currentWeaponInSheath;

        private void Start()
        {
            _hero = GetComponent<Hero>();
            _currentWeaponInSheath = Instantiate(_weapon, _weaponSheath.transform);
            
        }

        //private void Update()
        //{
        //    if (_hero.IsSelectedDef(ItemTags.Bow))
        //    {
        //        _currentWeaponInSheath = Instantiate(_bow, _weaponSheath.transform);
        //    }
        //}

        public void DrawWeapon()
        {

            _currentWeaponInHand = Instantiate(_weapon,_weaponHolder.transform);
            Destroy(_currentWeaponInSheath);
        }

        public void SheathWeapon()
        {
            int _weaponSlotIndex = (int)_weaponSlot;

            _currentWeaponInSheath = Instantiate (_weapon,_weaponSheath.transform);
            Destroy (_currentWeaponInHand);
        }
    }

    public enum WeaponSlot {
        Primary = 0,
        Secondary = 1
    }

}
