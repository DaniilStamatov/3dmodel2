using Assets.Scripts.Components.UI.Hud.Dialogs;
using Assets.Scripts.UI.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Components.Dialogs
{
    public class OptionDialogController : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private Text _dialogText;
        [SerializeField] private Transform _optionsContainer;
        [SerializeField] private OptionsItemWidget _prefab;

        private DataGroup<OptionData, OptionsItemWidget> _dataGroup;

        private void Start()
        {
           _dataGroup = new DataGroup<OptionData, OptionsItemWidget>(_prefab, _optionsContainer); 
        }

        public void Show(OptionDialogData data)
        {
            _content.SetActive(true);

            _dialogText.text = data.DialogText;
            _dataGroup.SetData(data.Options);
        }

        public void OnOptionSelected(OptionData selectedOption)
        {
            selectedOption.OnSelect.Invoke();
            _content.SetActive(false);
        }
    }


    [Serializable]
    public class OptionDialogData
    {
        public string DialogText;
        public OptionData[] Options;
    }
    
    [Serializable]
    public class OptionData
    {
        public string Text;
        public UnityEvent OnSelect;
    }
}
