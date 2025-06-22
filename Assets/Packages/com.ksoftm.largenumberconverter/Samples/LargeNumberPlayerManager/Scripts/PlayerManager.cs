namespace com.ksoftm.largenumberconverter.Sample.LargeNumberPlayerManager
{
  using System;
  using System.Collections;
  using System.Numerics;
  using Ksoftm.LargeNumberConverter;
  using TMPro;
  using UnityEngine;

  [Serializable]
  public class PlayerStates
  {
    [LargeNumberSelector(true)]
    [SerializeField] private string coins;
    public BigInteger Coins
    {
      get => LargeNumberConverter.Parse(coins);
      set => coins = value.ToString();
    }

    public PlayerStates()
    {
      Coins = BigInteger.Zero;
    }
  }


  public class PlayerManager : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI textMeshCoins;

    [SerializeField] private PlayerStates playerStates = new();


    private void Start()
    {
      playerStates.Coins += "100".Parse();
    }

    private void Update()
    {
    }

    private void LateUpdate()
    {
      var random = UnityEngine.Random.Range(100, 10000);
      playerStates.Coins += random;
      Debug.Log($"random: {random}");
      textMeshCoins.text = playerStates.Coins.ToShortForm();
      // Debug.Log($"playerStates.coins: {playerStates.Coins.ToShortForm()}");
    }
  }
}
