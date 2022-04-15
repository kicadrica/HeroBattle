using DG.Tweening;
using TMPro;
using UnityEngine;

public class TotalMoneyUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TotalMoneyText;
    private void Start()
    {
        PlayerMoney.OnTotalMoneyChange += DisplayTotalMoney;
        UpgradeInfoUIController.OnLittleMoneyToUpgrade += DoAnim;
        DisplayTotalMoney();
    }
    private void DisplayTotalMoney()
    {
        TotalMoneyText.text = PlayerMoney.TotalMoney.ToString();
    }

    private void DoAnim()
    {
        TotalMoneyText.DOColor(Color.red, 0.3f).SetLoops(4, LoopType.Yoyo);
        TotalMoneyText.transform.DOScale(1.2f, 0.3f).SetLoops(4, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        PlayerMoney.OnTotalMoneyChange -= DisplayTotalMoney;
        UpgradeInfoUIController.OnLittleMoneyToUpgrade -= DoAnim;
    }
}
