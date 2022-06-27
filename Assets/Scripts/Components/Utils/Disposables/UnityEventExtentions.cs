using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Assets.Scripts.Components.Utils.Disposables
{
    public static class UnityEventExtentions
    {
        public static IDisposable Subscribe(this UnityEvent unityEvent, UnityAction action)
        {
            unityEvent.AddListener(action);
            return new ActionDisposable(() => unityEvent.RemoveListener(action));
        }

        public static IDisposable Subscribe<TType>(this UnityEvent<TType> unityEvent, UnityAction<TType> call)
        {
            unityEvent.AddListener(call);
            return new ActionDisposable(() => unityEvent.RemoveListener(call));
        }
    }
}
