using Assets.Scripts.Components.Model.Definitions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components.UI.Localization
{
    [RequireComponent(typeof(Text))]
    public class LocalizeText : AbstractLocalizeComponent
    {
        [SerializeField] private string _key;
        [SerializeField] private bool _capitalize;
        private Text _text;

        protected override void Awake()
        {
            _text = GetComponent<Text>();
            base.Awake();
        }


        protected override void Localize()
        {
            var localized = LocalizationManager.Instance.Localize(_key);
            _text.text = _capitalize? localized.ToUpper() : localized;
        }

    }
}
