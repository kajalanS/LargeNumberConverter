namespace com.ksoftm.largenumberconverter.Sample.LargeNumberPlayerManager
{
  using System;
  using System.Numerics;
  using Ksoftm.LargeNumberConverter;
  using UnityEngine;

  [Serializable]
  public class PlayerStates
  {
    [LargeNumberSelector]
    public string coins = "1000";
  }




  public class PlayerManager : MonoBehaviour
  {
    [SerializeField] private PlayerStates playerStates = new();

    private void Start()
    {
      playerStates.coins = BigInteger.Add(playerStates.coins.Parse(), 1000.ToBigInteger()).ToShortString();

    }

    private void Update()
    {

    }
  }
}
