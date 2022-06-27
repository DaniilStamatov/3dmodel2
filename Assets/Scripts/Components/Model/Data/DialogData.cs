using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Data
{
    [Serializable]
    public struct DialogData
    {
        [SerializeField] private Sentence[] _sentence;
        [SerializeField] private DialogType _type;

        public Sentence[] Sentence => _sentence;
        public DialogType Type => _type;
    }


    [Serializable]
    public struct Sentence
    {
        [SerializeField] private string _value;
        [SerializeField] private Sprite _icon;
        [SerializeField] private Side _side;

        public string Value => _value;
        public Sprite Icon => _icon;
        public Side Side => _side;
    }

    public enum Side
    {
        Left,
        Right
    }

    public enum DialogType 
    {
        Simple,
        Personalized
    }

}
