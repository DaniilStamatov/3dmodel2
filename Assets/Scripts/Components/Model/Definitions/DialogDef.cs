using Assets.Scripts.Components.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Components.Model.Definitions
{
    [CreateAssetMenu(fileName = "Defs/Dialog", menuName = "Dialog")]
    public class DialogDef : ScriptableObject
    {
        [SerializeField] private DialogData _data;
        public DialogData Data => _data;
    }
}
