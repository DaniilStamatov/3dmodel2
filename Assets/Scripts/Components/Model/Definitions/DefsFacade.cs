
using System;
using UnityEngine;
using Assets.Scripts.Components.Model.Definitions.Repositories;

namespace Assets.Scripts.Components.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]

    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private InventoryItemDef _items;
        [SerializeField] private PlayerDef _player;
        [SerializeField] private PotionRepository _potion;
        [SerializeField] private WeaponRepository _weapon;


        public InventoryItemDef Items => _items;
        public PlayerDef Player => _player;
        public PotionRepository Potion => _potion;
        public WeaponRepository Weapon => _weapon;

        private static DefsFacade _instance;
        public static DefsFacade Instance => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}
