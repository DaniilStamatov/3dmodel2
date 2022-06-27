using Assets.Scripts.Components.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.InGameMenuWindow
{
    public class InGameMenuWindow : AnimatedWindow
    {
        private float defaultTime;
        protected override void Start()
        {
            base.Start();

            defaultTime = Time.timeScale;
            Time.timeScale = 0;
        }

        public void OnShowSettings()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnExit()
        {
            SceneManager.LoadScene("MainMenu");
            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);
        }

        
        private void OnDestroy()
        {
            Time.timeScale = defaultTime;
        }
    }
}
