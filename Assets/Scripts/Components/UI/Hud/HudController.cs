using Assets.Scripts.Components.Model;
using Assets.Scripts.Components.Model.Definitions;
using Assets.Scripts.Components.Utils.Disposables;
using Assets.Scripts.UI.InGameMenuWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.UI.Hud
{
    public class HudController :MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _progressBar;
        [SerializeField] private GameObject _canvas;
        public GameObject _target;
        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _session.Data.Hp.OnChanged += OnHealthChanged;

            OnHealthChanged(_session.Data.Hp.Value, 0);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefsFacade.Instance.Player.MaxHealth;
            var healthProgress = (float) newValue / maxHealth;

            _progressBar.SetProgress(healthProgress);
        }

        public void OnGameWindow()
        {
            var window = Resources.Load<GameObject>("UI/InGameMenuWindow");
            Instantiate(window, _canvas.transform);
        }

        private void OnDestroy()
        {
            _session.Data.Hp.OnChanged -= OnHealthChanged;
        }
    }
}
