using Assets.Scripts.Components.Model.Data.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Data
{
    [CreateAssetMenu(menuName = "Data/GameSettings", fileName = "GameSettings")]

    public class GameSettings : ScriptableObject
    {
        [SerializeField] private FloatPersistentProperty _music;
        [SerializeField] private FloatPersistentProperty _sfx;

        public FloatPersistentProperty Music => _music;
        public FloatPersistentProperty Sfx => _sfx;

        private static GameSettings _instance;
        public static GameSettings Instance => _instance == null ? LoadGameSettings() : _instance;

        private static GameSettings LoadGameSettings()
        {
            return _instance = Resources.Load<GameSettings>("GameSettings");
        }

        private void OnEnable()
        {
            _music = new FloatPersistentProperty(1, SoundSettings.Music.ToString());
            _sfx = new FloatPersistentProperty(1, SoundSettings.Sfx.ToString());
        }

        private void OnValidate()
        {
            _music.Validate();
            _sfx.Validate();
        }
    }

    public enum SoundSettings
    { 
        Music,
        Sfx
    }

}
