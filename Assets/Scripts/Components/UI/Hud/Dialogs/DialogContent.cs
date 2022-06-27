using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components.UI.Hud.Dialogs
{
    public class DialogContent : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _icon;

        public Text Text=> _text;
        public Image Icon=> _icon;

        public void TrySetIcon(Sprite icon)
        {
            if(_icon!=null)
                _icon.sprite = icon;
        }
     }
}
