using System;
using UnityEngine;


namespace Assets.Scripts.Components.Model.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Defs/PotionRepository", fileName = "PotionRepository")]

    public class PotionRepository : DefRepository<PotionDef>
    {
    }

    [Serializable]
    public class PotionDef : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private int _value;
        [SerializeField] private float _duration;
        [SerializeField] private Effect _effect;

        public string Id => _id;
        public int Value => _value;
        public float Duration => _duration;
        public Effect Effect => _effect;
    }

    public enum Effect { 
        Heal,
        SpeedUp
    }

}
