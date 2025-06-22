namespace Ksoftm.LargeNumberConverter.Sample.LargeNumberConverterExample
{
  using Ksoftm.LargeNumberConverter;
  using UnityEngine;

  public class LargeNumberExample : MonoBehaviour
  {
    [LargeNumber(showShortForm: true, showIllionForm: true)]
    public string ValueText = "1.23 M";

    void Start()
    {
      var big = LargeNumberConverter.Parse(ValueText);
      // big = ValueText.Parse(); // another option
      Debug.Log($"Parsed BigInteger: {big}");
    }
  }
}
