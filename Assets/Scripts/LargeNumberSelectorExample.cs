using Ksoftm.LargeNumberConverter;
using UnityEngine;
/// <summary>
/// Example MonoBehaviour using the selector:
/// </summary>
public class LargeNumberSelectorExample : MonoBehaviour
{
  [LargeNumberSelector(showIllionForm: true)]
  public string ValueText = "1 k";

  void Start()
  {
    var big = LargeNumberConverter.Parse(ValueText);
    Debug.Log($"Parsed: {big}");
  }
}
