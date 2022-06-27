using Assets.Scripts.Components.Model.Definitions.Localization;
using Assets.Scripts.UI;
using Assets.Scripts.UI.Widgets;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components.UI.Localization
{
    public class LocaleWindow : AnimatedWindow
    {

        [SerializeField] private Transform _container;
        [SerializeField] private LocaleItemWidget _prefab;
        private DataGroup<LocaleInfo, LocaleItemWidget> _dataGroup;

        private readonly string[] _locales = new string[] { "en", "ru"};
        protected override void Start()
        {
            base.Start();

            _dataGroup = new DataGroup<LocaleInfo, LocaleItemWidget>(_prefab, _container);

            _dataGroup.SetData(ComposeData());
        }

        private List<LocaleInfo> ComposeData()
        {
            var data = new List<LocaleInfo>();
            foreach (var locale in _locales)
            {
                data.Add(new LocaleInfo { LocaleId = locale });
            }

            return data;
        }

        public void OnSelected(string selectedLocale)
        {
            LocalizationManager.Instance.SetLocale(selectedLocale);
        }
    }
}
