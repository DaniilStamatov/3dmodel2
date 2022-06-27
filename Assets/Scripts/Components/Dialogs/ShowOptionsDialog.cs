using Assets.Scripts.Components.UI.Hud.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Dialogs
{
    public class ShowOptionsDialog : MonoBehaviour
    {
        private OptionDialogController _dialogBox;
        [SerializeField] private OptionDialogData _dialogData;

        public void Show()
        {
            if (_dialogBox == null)
                _dialogBox = FindObjectOfType<OptionDialogController>();

            _dialogBox.Show(_dialogData);
        }
    }
}
