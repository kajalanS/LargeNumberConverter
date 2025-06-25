namespace com.ksoftm.largenumberconverter.Sample.LargeNumberPlayerManager
{
    using System;
    using Ksoftm.LargeNumberConverter;
    using TMPro;
    using UnityEngine;
    using static System.Int32;

    [Serializable]
    public class PlayerStates
    {
        public LargeNumber Coins = LargeNumber.Zero;
    }


    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshCoins;

        [SerializeField] private PlayerStates playerStates = new();


        private void Start()
        {
            playerStates.Coins.Value += 100;
        }

        private void LateUpdate()
        {
            RandomChange();
        }

        private void RandomChange()
        {
            var random = UnityEngine.Random.Range(ulong.MaxValue, MaxValue);
            var rint = UnityEngine.Random.Range(1000000, MaxValue);
            var input =
                // $"{ulong.MaxValue}{ulong.MaxValue}{ulong.MaxValue}{ulong.MaxValue}{ulong.MaxValue}{ulong.MaxValue}{ulong.MaxValue}{ulong.MaxValue}{ulong.MaxValue} {LargeNumberConverter.Suffixes[(rint % LargeNumberConverter.Suffixes.Count)]}";
                // $"{1000} {LargeNumberConverter.Suffixes[(rint % LargeNumberConverter.Suffixes.Count)]}";
                $"{1000}";
            var val = input.Parse();
            playerStates.Coins.Value += val;
            Debug.Log(playerStates.Coins.Value.ToString());
            textMeshCoins.text = playerStates.Coins.ToString();
        }
    }
}