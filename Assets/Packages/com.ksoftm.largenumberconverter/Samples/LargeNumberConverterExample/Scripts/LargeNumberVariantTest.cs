namespace Ksoftm.LargeNumberConverter.Sample
{
  using UnityEngine;
  using Ksoftm.LargeNumberConverter;

  public class LargeNumberVariantTest : MonoBehaviour
  {
    [SerializeField] string[] tests = { "12.5 aa", "100 M", "1.2B", "999.999 k", "1.23 ac", "1.23aa", "1.23ab" };

    void Start()
    {
      foreach (var t in tests)
      {
        var val = LargeNumberConverter.Parse(t);
        Debug.Log($"{t} → {val}");

        var shortStr = LargeNumberConverter.ToShortString(val);
        Debug.Log($"Short format: {shortStr}");

        var wordStr = LargeNumberConverter.ToIllionText(val);
        Debug.Log($"In words: {wordStr}");
      }
    }
  }
}
