namespace Ksoftm.LargeNumberConverter
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Attribute to mark numeric string fields that should use a suffix selector in the Inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class LargeNumberSelectorAttribute : PropertyAttribute
    {
        public readonly bool ShowIllionForm;

        public LargeNumberSelectorAttribute(bool showIllionForm = false)
        {
            ShowIllionForm = showIllionForm;
        }
    }
}