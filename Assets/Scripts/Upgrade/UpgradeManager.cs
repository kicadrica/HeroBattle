using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private static UpgradeManager _instance;
    [SerializeField] private List<UpgradeInfo> AllUpgrades;
    private void Awake()
    {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        foreach (var upgrade in AllUpgrades) {
          upgrade.Init();  
        }
    }

    public static UpgradeInfo GetUpgradeInfo(UpgradeType upgradeType)
    {
        foreach (var upgradeInfo in _instance.AllUpgrades) {
            if (upgradeInfo.Type == upgradeType) {
                return upgradeInfo;
            }
        }
        throw new Exception("Type is not in list");
    }
    
}
