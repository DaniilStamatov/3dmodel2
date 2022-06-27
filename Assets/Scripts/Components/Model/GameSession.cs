using Assets.Scripts.Components.Model.Data;
using Assets.Scripts.Components.Utils.Disposables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Components.Model
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField] private PlayerData _data;
        public PlayerData Data => _data;

        private PlayerData _save;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        public QuickInventoryModel QuickInventoryModel {get; private set;}

        private void Awake()
        {
            LoadHud();
            if (IsSessionExists())
            {
                Destroy(gameObject);
            }
            else
            {
                Save();
                InitModels();
                DontDestroyOnLoad(this);
            }
        }

        private void InitModels()
        {
            QuickInventoryModel = new QuickInventoryModel(Data);
            _trash.Retain(QuickInventoryModel);
        }

        private bool IsSessionExists()
        {
            var session = FindObjectsOfType<GameSession>();
            foreach (var gameSession in session)
            {
                if (gameSession != this)
                    return true;
               
            }
             return false;
        }

        private void LoadHud()
        {
            SceneManager.LoadScene("Hud", LoadSceneMode.Additive);
        }

        public void Save()
        {
            _save = _data.Clone();
        }

        private void LoadLastSave()
        {
            _data = _save.Clone();
        }


        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}
