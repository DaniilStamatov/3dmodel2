using Assets.Scripts.Components.Model.Definitions.Localization;
using UnityEngine;

namespace Assets.Scripts.Components.UI.Localization
{
    public abstract class AbstractLocalizeComponent :MonoBehaviour
    {
        protected virtual void Awake()
        {
            LocalizationManager.Instance.OnLocaleChanged += OnLocaleChanged;
            Localize();
        }

        private void OnLocaleChanged()
        {
            Localize();
        }

        protected abstract void Localize();
        

        private void OnDestroy()
        {
            LocalizationManager.Instance.OnLocaleChanged -= OnLocaleChanged;
        }
    }
}
