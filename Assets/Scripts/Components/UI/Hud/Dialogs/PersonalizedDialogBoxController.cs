using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Components.Model.Data;

namespace Assets.Scripts.Components.UI.Hud.Dialogs
{
    public class PersonalizedDialogBoxController : DialogBoxController
    {
        [SerializeField] private DialogContent _right;

        protected override DialogContent CurrentContent => CurrentSentence.Side == Side.Left ? _dialogContent : _right;

        protected override void OnStartDialogAnimation()
        {
            _right.gameObject.SetActive(CurrentSentence.Side == Side.Right);
            _dialogContent.gameObject.SetActive(CurrentSentence.Side == Side.Left);
            base.OnStartDialogAnimation();
        }
    }
}
