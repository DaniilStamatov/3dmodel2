using Assets.Scripts.Components.Model.Data.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Definitions.Localization
{
    public class LocalizationManager
    {
        public readonly static LocalizationManager Instance;

        private StringPersistentProperty _localeKey = new StringPersistentProperty("ru", "localization/current");

        public event Action OnLocaleChanged;

        private Dictionary<string, string> _localization;
        public string LocaleKey => _localeKey.Value;

        static LocalizationManager()
        {
            Instance = new LocalizationManager();
        }

        public LocalizationManager()
        {
            LoadLocale(_localeKey.Value);
        }

        private void LoadLocale(string localeToLoad)
        {
            var def = Resources.Load<LocaleDef>($"Locale/{localeToLoad}");
            _localization = def.GetData();
            _localeKey.Value = localeToLoad;
            OnLocaleChanged?.Invoke();
        }

        public string Localize(string key)
        {
            return _localization.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";
        }

        internal void SetLocale(string selectedLocale)
        {
            LoadLocale(selectedLocale);
        }
    }
}
