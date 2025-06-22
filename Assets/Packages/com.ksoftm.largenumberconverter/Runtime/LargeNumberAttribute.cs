namespace Ksoftm.LargeNumberConverter
{
  using System;
  using UnityEngine;
  /// <summary>
  /// Attribute to mark numeric string fields that should be parsed/formatted by LargeNumberConverter.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
  public class LargeNumberAttribute : PropertyAttribute
  {
    public bool ShowShortForm;
    public bool ShowIllionForm;

    public LargeNumberAttribute(bool showShortForm = true, bool showIllionForm = false)
    {
      ShowShortForm = showShortForm;
      ShowIllionForm = showIllionForm;
    }
  }
}
