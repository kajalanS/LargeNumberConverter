using Ksoftm.LargeNumberConverter;
using UnityEngine;

/// <summary>
/// Example MonoBehaviour demonstrating usage of [LargeNumber].
/// </summary>
public class LargeNumberExample : MonoBehaviour
{
  [LargeNumber(showShortForm: true, showIllionForm: true)]
  public string ValueText = "1.23 M";

  // At runtime, you can also parse/format in code
  void Start()
  {
    var big = LargeNumberConverter.Parse(ValueText);
    Debug.Log($"Parsed BigInteger: {big}");
  }
}
