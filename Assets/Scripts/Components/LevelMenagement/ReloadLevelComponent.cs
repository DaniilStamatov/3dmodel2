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
    public class ReloadLevelComponent : MonoBehaviour
    {
        

        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            Destroy(session.gameObject);
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}
