using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components.UI.Hud
{
    public class ProgressBarWidget : MonoBehaviour
    {
        [SerializeField] private Image _bar;

        public void SetProgress(float amount)
        {
            _bar.fillAmount = amount;
        }
    }
}
