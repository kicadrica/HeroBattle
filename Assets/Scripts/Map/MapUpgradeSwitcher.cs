using UnityEngine;
using UnityEngine.UI;

public class MapUpgradeSwitcher : MonoBehaviour
{
    public static bool ForceToUpgrade;
    
    [SerializeField] private GameObject MapHolder;
    [SerializeField] private GameObject UpgradeHolder;
    [SerializeField] private Button MapButton;
    [SerializeField] private Button UpgradeButton;
    private void Start()
    {
        MapButton.onClick.AddListener(SwitchMap);
        UpgradeButton.onClick.AddListener(SwitchUpgrade);
        
        if (ForceToUpgrade) {
            ForceToUpgrade = false;
            SwitchUpgrade();
        }
    }
    
    private void SwitchMap()
    {
        MapHolder.SetActive(true);
        UpgradeHolder.SetActive(false);
        MapButton.interactable = false;
        UpgradeButton.interactable = true;

    }
    
    private void SwitchUpgrade()
    {
        MapHolder.SetActive(false);
        UpgradeHolder.SetActive(true);
        MapButton.interactable = true;
        UpgradeButton.interactable = false;
    }
    
}
