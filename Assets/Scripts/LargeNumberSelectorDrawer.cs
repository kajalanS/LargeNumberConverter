
#if UNITY_EDITOR
using UnityEditor;
using System;
using UnityEngine;
using System.Linq;
/// <summary>
/// Custom drawer for LargeNumberSelectorAttribute:
/// Splits input into number + suffix dropdown, with optional "Illion" preview.
/// </summary>
[CustomPropertyDrawer(typeof(LargeNumberSelectorAttribute))]
public class LargeNumberSelectorDrawer : PropertyDrawer
{
  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
  {
    var attr = (LargeNumberSelectorAttribute)attribute;

    EditorGUI.BeginProperty(position, label, property);
    // Split position into two: number field and suffix popup
    float fullWidth = position.width;
    float fieldWidth = fullWidth * 0.6f;
    float popupWidth = fullWidth * 0.4f;
    Rect numberRect = new Rect(position.x, position.y, fieldWidth, EditorGUIUtility.singleLineHeight);
    Rect suffixRect = new Rect(position.x + fieldWidth + 4, position.y, popupWidth - 4, EditorGUIUtility.singleLineHeight);

    // Parse existing value
    string current = property.stringValue;
    var parts = current.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    string numPart = parts.Length > 0 ? parts[0] : string.Empty;
    string sufPart = parts.Length > 1 ? parts[1] : string.Empty;
    int sufIndex = Mathf.Max(0, LargeNumberConverter.Suffixes.ToList().IndexOf(sufPart));

    // Draw number as text field
    numPart = EditorGUI.TextField(numberRect, label, numPart);
    // Draw suffix as popup
    sufIndex = EditorGUI.Popup(suffixRect, sufIndex, LargeNumberConverter.Suffixes.ToArray());
    string selectedSuffix = LargeNumberConverter.Suffixes[sufIndex];

    // Combine and assign
    property.stringValue = string.IsNullOrEmpty(selectedSuffix) ? numPart : $"{numPart} {selectedSuffix}";

    // Optional Illion preview
    if (attr.ShowIllionForm)
    {
      try
      {
        var big = LargeNumberConverter.Parse(property.stringValue);
        Rect previewRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2,
            fullWidth, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(previewRect, "Illion:", LargeNumberConverter.ToIllionText(big));
      }
      catch (Exception ex)
      {
        Rect errorRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2,
            fullWidth, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(errorRect, "Error:", ex.Message);
      }
    }

    EditorGUI.EndProperty();
  }

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
  {
    var attr = (LargeNumberSelectorAttribute)attribute;
    float height = EditorGUIUtility.singleLineHeight;
    if (attr.ShowIllionForm) height += EditorGUIUtility.singleLineHeight + 2;
    return height;
  }
}
#endif
