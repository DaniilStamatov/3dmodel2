﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.UI;
using UnityEditor;
using Assets.Scripts.UI;

namespace Assets.Scripts.Components.UI
{
    [CustomEditor(typeof(CustomButton), true)]

    [CanEditMultipleObjects]
    public class CustomButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_normal"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_pressed"));

            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();

        }
    }
}
