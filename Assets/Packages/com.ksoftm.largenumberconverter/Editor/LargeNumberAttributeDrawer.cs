namespace Ksoftm.LargeNumberConverter.Editors
{
#if UNITY_EDITOR
    using System;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Custom property drawer for LargeNumberAttribute.
    /// Displays input string and formatted values in the Inspector.
    /// </summary>
    [CustomPropertyDrawer(typeof(LargeNumberAttribute))]
    public class LargeNumberAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = (LargeNumberAttribute)attribute;

            // Draw the string field
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();
            var input = EditorGUI.TextField(position, property.stringValue);
            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = input;
            }

            // Below the field, show parsed formats
            if (!string.IsNullOrEmpty(input))
            {
                try
                {
                    var bigInt = input.Parse();
                    var lineHeight = EditorGUIUtility.singleLineHeight;
                    var shortRect = new Rect(position.x, position.y + lineHeight + 2, position.width, lineHeight);
                    if (attr.ShowShortForm)
                        EditorGUI.LabelField(shortRect, "Short:", bigInt.ToShortString());

                    if (attr.ShowIllionForm)
                    {
                        var illionRect = new Rect(position.x,
                            position.y + (attr.ShowShortForm ? 2 * (lineHeight + 2) : lineHeight + 2), position.width,
                            lineHeight);
                        EditorGUI.LabelField(illionRect, "Illion:", bigInt.ToIllionText());
                    }
                }
                catch (Exception ex)
                {
                    var errorRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2,
                        position.width, EditorGUIUtility.singleLineHeight);
                    EditorGUI.LabelField(errorRect, "Error:", ex.Message);
                }
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var attr = (LargeNumberAttribute)attribute;
            float baseHeight = EditorGUIUtility.singleLineHeight;
            if (string.IsNullOrEmpty(property.stringValue))
                return baseHeight;

            int extraLines = (attr.ShowShortForm ? 1 : 0) + (attr.ShowIllionForm ? 1 : 0);
            return baseHeight + extraLines * (baseHeight + 2);
        }
    }
#endif
}