using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components
{
    public class HealthComponent : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private UnityEvent _onHeal;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] public UnityEvent _onDeath;
        [SerializeField] public HealthChangeEvent _OnChange;

        public int Health => _health;
        public void ModifyHealth(int damageValue)
        {
            _health += damageValue;
            _OnChange?.Invoke(_health);
            if (damageValue > 0)
            {
                _onHeal?.Invoke();
            }
            if (damageValue < 0)
            {
                _onDamage?.Invoke();
            }
            if (_health <= 0)
            {
                _onDeath?.Invoke();
            }
        }

        private void OnDestroy()
        {
            _onDeath.RemoveAllListeners();
        }

#if UNITY_EDITOR
        [ContextMenu("UpdateHealth")]

        private void UpdateHealth()
        {
            _OnChange?.Invoke(_health);
        }

#endif

        [Serializable]
        public class HealthChangeEvent : UnityEvent<int>
        {

        }

        public void SetHealth(int health)
        {
            _health = health;
        }
    }
}
