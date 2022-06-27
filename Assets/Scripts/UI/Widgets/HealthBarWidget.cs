using Assets.Scripts.Components;
using Assets.Scripts.Components.UI.Hud;
using Assets.Scripts.Components.Utils.Disposables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.Widgets
{
    public class HealthBarWidget : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _bar;
        [SerializeField] private HealthComponent _health;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private int _maxHp;
        private void Start()
        {
            if(_health == null)
                 _health = GetComponentInParent<HealthComponent>();


            _maxHp = _health.Health;

            _trash.Retain(_health._OnChange.Subscribe(OnHealthChanged));
            _trash.Retain(_health._onDeath.Subscribe(OnDie));
        }

        private void OnDie()
        {
            Destroy(gameObject);
        }

        private void OnHealthChanged(int health)
        {
            var progress = (float) health / _maxHp;
            _bar.SetProgress(progress);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
