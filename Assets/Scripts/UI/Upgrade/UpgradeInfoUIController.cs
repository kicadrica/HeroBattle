using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInfoUIController : MonoBehaviour
{
    public static event Action OnLittleMoneyToUpgrade;
    [SerializeField] private TextMeshProUGUI UpgradeInfoText;
    [SerializeField] private TextMeshProUGUI UpgradePriceText;
    [SerializeField] private Button UpgradeButton;
    [SerializeField] private UpgradeType Type;

    private UpgradeInfo _upgradeInfo;
    
    private void Start()
    {
        _upgradeInfo = UpgradeManager.GetUpgradeInfo(Type);
        UpdateInfo();
        
        UpgradeButton.onClick.AddListener(MakeUpUpgrade);
    }
    private void MakeUpUpgrade()
    {
        if (_upgradeInfo.GetNextUpgradePrice() > PlayerMoney.TotalMoney) { 
            AudioManager.Instance.PlaySound(TypeOfSound.UpgradeButtonInactive);
            OnLittleMoneyToUpgrade?.Invoke();
        } else {
            PlayerMoney.SpendCoins(_upgradeInfo.GetNextUpgradePrice());
            _upgradeInfo.UpgradeToNextLevel();
            UpdateInfo();
            AudioManager.Instance.PlaySound(TypeOfSound.UpgradeButtonActive);
        }
    }

    private void UpdateInfo()
    {
        if (_upgradeInfo.IsUpgradeMax) {
            UpgradeInfoText.text = _upgradeInfo.GetCurrentUpgradeValue() + " => MAX";
            UpgradePriceText.gameObject.SetActive(false);
            UpgradeButton.gameObject.SetActive(false);
        } else {
            UpgradeInfoText.text = _upgradeInfo.GetCurrentUpgradeValue() + " => " + _upgradeInfo.GetNextUpgradeValue();
            UpgradePriceText.text = _upgradeInfo.GetNextUpgradePrice().ToString();
        }
    }

}
