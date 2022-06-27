using Assets.Scripts.Components.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Components.LevelMenagement
{
    public class ExitLevelComponent: MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
            session.Save();

            SceneManager.LoadScene(_sceneName);
        }
    }
}
