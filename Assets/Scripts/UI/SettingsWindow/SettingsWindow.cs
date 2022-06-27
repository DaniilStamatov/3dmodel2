using Assets.Scripts.Components.Model.Data;
using Assets.Scripts.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI.SettingsWindow
{
    public class SettingsWindow : AnimatedWindow
    {
        [SerializeField] private AudioSettingsWidget _music;
        [SerializeField] private AudioSettingsWidget _sfx;


        protected override void Start()
        {
            base.Start();
            _music.SetModel(GameSettings.Instance.Music);
            _sfx.SetModel(GameSettings.Instance.Sfx);
        }
    }
}
