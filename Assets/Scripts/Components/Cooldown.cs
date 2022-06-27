using System;
using UnityEngine;

namespace Assets.Scripts.Components
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _cooldown;

        private float _timeIsUp;

        public float Value
        {
            get => _cooldown;
            set => _cooldown = value; 
        }

        public void Reset()
        {
            _timeIsUp = Time.time + _cooldown;
        }

        public bool IsReady => _timeIsUp <=Time.time;
    }
}