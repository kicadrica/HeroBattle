using System;
using UnityEngine;

[Serializable]
public class UpgradeInfo
{
    public UpgradeType Type;
    public int UpgradePrice;
    public int MaxUpgradeLevel;
    public int BaseValue;
    public int ValuePerLevel;

    public bool IsUpgradeMax => _curUpgradeLevel >= MaxUpgradeLevel;
    private int _curUpgradeLevel;
    
    public void Init()
    {
        _curUpgradeLevel = PlayerPrefs.GetInt("upg_" + Type);
    }
    public int GetCurrentUpgradeValue()
    {
        return BaseValue + ValuePerLevel * _curUpgradeLevel;
    }

    public int GetNextUpgradeValue()
    {
        if (!IsUpgradeMax) {
            return BaseValue + ValuePerLevel * (_curUpgradeLevel + 1);
        }
        return GetCurrentUpgradeValue();
    }

    public int GetNextUpgradePrice()
    {
        return UpgradePrice * _curUpgradeLevel + UpgradePrice * (_curUpgradeLevel + 1);
    }

    public void UpgradeToNextLevel()
    {
        if (IsUpgradeMax) return;
        _curUpgradeLevel++;
        PlayerPrefs.SetInt("upg_" + Type, _curUpgradeLevel);
    }

}
