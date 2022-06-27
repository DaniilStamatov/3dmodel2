using Assets.Scripts.Components.Model.Data;
using Assets.Scripts.Components.Model.Data.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Widgets
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSettingsComponent : MonoBehaviour
    {
        [SerializeField] private SoundSettings _mode;
        private AudioSource _source;

        private FloatPersistentProperty _model;

        private void Start()
        {
            _source = GetComponent<AudioSource>();
            _model = FindProperty();
            _model.OnChanged += OnSoundSettingsChanged;

            OnSoundSettingsChanged(_model.Value, _model.Value);
        }

        private void OnSoundSettingsChanged(float oldValue, float newValue)
        {
            _source.volume = newValue;
        }

        private FloatPersistentProperty FindProperty()
        {
            switch (_mode)
            {
                case SoundSettings.Music:
                    return GameSettings.Instance.Music;
                case SoundSettings.Sfx:
                    return GameSettings.Instance.Sfx;
            }

            throw new ArgumentException("Undefined mode");
        }

        private void OnDestroy()
        {
            _model.OnChanged -= OnSoundSettingsChanged;
        }
    }
}
