using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.MainMenuWindow
{
    public class MainMenuWindow : AnimatedWindow
    {
        private Action _closeAction;

        public void OnStartGame()
        {
            _closeAction = () => { SceneManager.LoadScene("Level1"); };
            Close();
        }

        public void OnSettings()
        {
            var window = Resources.Load<GameObject>("UI/SettingsWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnLanguages()
        {
            var window = Resources.Load<GameObject>("UI/LocalizationWindow");
            var canvas = FindObjectOfType<Canvas>();
            Instantiate(window, canvas.transform);
        }

        public void OnExit()
        {
            _closeAction = () => 
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            };
            Close();
        }

        public override void OnCloseAnimationComplete()
        {
            base.OnCloseAnimationComplete();
            _closeAction?.Invoke();
        }
    }
}
