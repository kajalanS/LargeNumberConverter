namespace Ksoftm.LargeNumberConverter.Sample
{
  using Ksoftm.LargeNumberConverter;
  using UnityEngine;

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
}
