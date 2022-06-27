using Assets.Scripts.Components.Model.Data;
using Assets.Scripts.Components.Model.Definitions;
using Assets.Scripts.Components.UI.Hud.Dialogs;
using UnityEngine;
using System;

namespace Assets.Scripts.Components.Dialogs
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] private Mode _mode;
        [SerializeField] private DialogData _bound;
        [SerializeField] private DialogDef _external;

        private DialogBoxController _dialogBox;

        public void Show()
        {
            _dialogBox = FindDialogBoxController();
            _dialogBox.ShowDialog(Data);
        }

        public void Show(DialogDef def)
        {
            _external = def;
            Show();
        }


        private DialogBoxController FindDialogBoxController()
        {
            if (_dialogBox != null) return _dialogBox;

            GameObject controllerGo = null;

            switch (Data.Type)
            {
                case DialogType.Simple:
                    controllerGo = GameObject.FindWithTag("SimpleDialog");
                    break;
                case DialogType.Personalized:
                    controllerGo = GameObject.FindWithTag("PersonalizedDialog");
                    break;
                default:
                    throw new ArgumentException("Undefined dialog type");
            }

            return controllerGo.GetComponent<DialogBoxController>();
        }

        public DialogData Data
        {
            get
            {
                switch (_mode)
                {
                    case Mode.Bound:
                        return _bound;
                    case Mode.External:
                        return _external.Data;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

     

        public enum Mode
        {
            Bound,
            External
        }

    }
}
