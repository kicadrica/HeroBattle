using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WinController : MonoBehaviour
{
    private const float PosY = 0;
    private const float PanelAnimDuration = 0.7f;
    private const float LevelHolderAnimDuration = 0.5f;
    private const float CoinsAnimDuration = 1.4f;
    private const float IconsScaleDuration = 0.5f;
    private const float IconsScaleCoeff = 1.2f;
    private const float DelayBetweenSteps = 0.5f;
    private const float HeadingTextDuration = 0.3f;

    [SerializeField] private GameObject WinHolder;
    [SerializeField] private GameObject Shade;
    
    [SerializeField] private GameObject LevelHolder;
    [SerializeField] private TextMeshProUGUI NumberOfLevelText;
    [SerializeField] private GameObject Confetti;

    [SerializeField] private TextMeshProUGUI CoinsPerLevelText;
    [SerializeField] private Transform CoinIcon;
    
    [SerializeField] private TextMeshProUGUI TimerText;
    [SerializeField] private Transform TimerIcon;

    [SerializeField] private TextMeshProUGUI HeadingText;
    [SerializeField] private Transform PlayButton;
    
    private void Awake()
    {
        WinHolder.SetActive(false);
        Shade.SetActive(false);
        Confetti.SetActive(false);
        LevelHolder.SetActive(false);
    }

    private void Start()
    {
        MonsterSpawner.OnLevelComplete += CompleteLevel;
    }

    private void OnDestroy()
    {
        MonsterSpawner.OnLevelComplete -= CompleteLevel;
    }
    private void CompleteLevel()
    {
        StartCoroutine(WinAnimation());
        if (LevelManager.CurLevelIndex <= LevelManager.CurLevel.NumberOfLevel) {
            LevelManager.CurLevelIndex++;
        }
    }

    private IEnumerator WinAnimation()
    {
        yield return new WaitForSeconds(Global.WinDelay);
        AudioManager.Instance.StopMusic();
        ShowWinPanel();

        yield return new WaitForSeconds(LevelHolderAnimDuration + DelayBetweenSteps);
        TimerAnim();
        
        yield return new WaitForSeconds(IconsScaleDuration + DelayBetweenSteps);
        CoinsAnim();
        
        yield return new WaitForSeconds(CoinsAnimDuration + IconsScaleDuration);
        AudioManager.Instance.PlaySound(TypeOfSound.LevelComplete);
        HeadingAnim();

        yield return new WaitForSeconds(HeadingTextDuration + DelayBetweenSteps);
        PlayButtonAnim();
    }

    private void ShowWinPanel()
    {
        Shade.SetActive(true);
        WinHolder.SetActive(true);
        NumberOfLevelText.text = "Level " + LevelManager.CurLevel.NumberOfLevel;
        
        AudioManager.Instance.PlaySound(TypeOfSound.WinPanel);
        WinHolder.transform.DOMoveY(PosY, PanelAnimDuration).SetEase(Ease.OutBack).OnComplete(LevelHolderAnim);
    }
    private void LevelHolderAnim()
    {
        LevelHolder.SetActive(true);
        AudioManager.Instance.PlaySound(TypeOfSound.LevelHolder);
        LevelHolder.transform.DOScale(Vector3.one, LevelHolderAnimDuration).OnComplete(ShowConfetti);
    }
    private void ShowConfetti()
    {
        AudioManager.Instance.PlaySound(TypeOfSound.PartyPoppers);
        Confetti.SetActive(true);
    }
    
    private void TimerAnim()
    {
        var minutes = Mathf.FloorToInt(LevelTimer.LevelTime / 60).ToString("D2");
        var seconds = Mathf.FloorToInt(LevelTimer.LevelTime % 60).ToString("D2");
        TimerText.text = minutes + ":" + seconds;
        
        TimerIcon.DOScale(IconsScaleCoeff, IconsScaleDuration).SetLoops(2, LoopType.Yoyo);
        AudioManager.Instance.PlaySound(TypeOfSound.ClockScale);
    }

    private void CoinsAnim()
    {
        AudioManager.Instance.PlaySound(TypeOfSound.Counting);
        
        DOVirtual.Int(0, GameController.CoinsPerLevel, CoinsAnimDuration, value => {
            CoinsPerLevelText.text = value.ToString();
        }).OnComplete(() => {
            CoinIcon.DOScale(IconsScaleCoeff, IconsScaleDuration).SetLoops(2, LoopType.Yoyo);
            AudioManager.Instance.PlaySound(TypeOfSound.CoinScale);
        });
    }

    private void HeadingAnim()
    {
        HeadingText.DOFade(0, HeadingTextDuration).SetLoops(2, LoopType.Yoyo);
    }

    private void PlayButtonAnim()
    {
        PlayButton.DOScale(IconsScaleCoeff, IconsScaleDuration).SetLoops(6, LoopType.Yoyo);
    }
    

}
