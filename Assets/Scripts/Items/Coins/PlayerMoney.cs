using System;
using UnityEngine;

public static class PlayerMoney
{
    public static event Action OnTotalMoneyChange;
    public static int TotalMoney {
        get => PlayerPrefs.GetInt("TotalMoney");
        private set => PlayerPrefs.SetInt("TotalMoney", value);
    }

    public static void AddCoins(int coins)
    {
        TotalMoney += coins;
        OnTotalMoneyChange?.Invoke();
    }

    public static void SpendCoins(int coins)
    {
        if (coins > TotalMoney) return;
        TotalMoney -= coins;
        OnTotalMoneyChange?.Invoke();
    }
}
