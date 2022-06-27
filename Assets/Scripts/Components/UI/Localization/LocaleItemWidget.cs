using Assets.Scripts.Components.Model.Definitions.Localization;
using Assets.Scripts.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Components.UI.Localization
{
    public class LocaleItemWidget : MonoBehaviour, ItemRenderer<LocaleInfo>
    {
        [SerializeField] private Text _text;
        [SerializeField] private GameObject _selector;
        [SerializeField] private SelectLocale _onSelect;

        private LocaleInfo _data;

        private void Start()
        {
            LocalizationManager.Instance.OnLocaleChanged += UpdateSelectedLocale;
        }

        public void SetData(LocaleInfo localeInfo, int index)
        {
            _data = localeInfo;
            UpdateSelectedLocale();
            _text.text = localeInfo.LocaleId.ToUpper();
         }


        private void UpdateSelectedLocale()
        {
            var isSelected = LocalizationManager.Instance.LocaleKey == _data.LocaleId;
            _selector.SetActive(isSelected);
        }

       
        public void OnSelected()
        {
            _onSelect?.Invoke(_data.LocaleId);
        }

        private void OnDestroy()
        {
            LocalizationManager.Instance.OnLocaleChanged -= UpdateSelectedLocale;

        }

    }

    [Serializable]
    public class SelectLocale : UnityEvent<string>
    {

    }

    public class LocaleInfo
    {
        public string LocaleId;
    }
}
