using System;
using UnityEditor;

namespace Assets.Scripts.Components.Utils.Editor
{
    public static class SerializedPropertyExtentions
    {
        public static bool GetEnum<TEnumType>(this SerializedProperty property, out TEnumType returnValue)
            where TEnumType : Enum
        {
            returnValue = default;
            var names = property.enumNames;
            if (names == null || names.Length == 0)
                return false;


            var enumName = names[property.enumValueIndex];
            returnValue = (TEnumType)Enum.Parse(typeof(TEnumType), enumName);
            return true;
        }
    }
}
