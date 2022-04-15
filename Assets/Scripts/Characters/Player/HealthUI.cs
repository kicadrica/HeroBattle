using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private const int AlarmWorkTime = 5;
    private const float HealthForAlarm = 0.3f;
    private const float BlinkDelay = 0.48f;
    
    [SerializeField] private PlayerController Player;
    [SerializeField] private Image HpImage;
    [SerializeField] private Image AlarmLight;
    [SerializeField] private Sprite RedLight;
    [SerializeField] private SpriteRenderer HurtSpriteRenderer;
    
    private Sprite _greenLight;
    private bool _isBlinked;
    private void Start()
    {
        _greenLight = AlarmLight.sprite;
        ResetHurtSprite();
        Player.Health.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        HpImage.fillAmount = Player.Health.LerpCurrent;
        ResetHurtSprite();

        if (Player.Health.Current <= 0) {
            AudioManager.Instance.StopSound(TypeOfSound.LowHp);
            return;
        }
        
        HurtSpriteRenderer.DOFade(1, 0.1f).SetLoops(4, LoopType.Yoyo).SetEase(Ease.OutBounce);
        
        if (Player.Health.LerpCurrent <= HealthForAlarm) {
            if (!_isBlinked) {
                AlarmLight.sprite = RedLight;
                StartCoroutine(Alarm());
            }
            _isBlinked = true;
        } else {
            _isBlinked = false;
            AlarmLight.sprite = _greenLight;
        }
    }

    private void ResetHurtSprite()
    {
        HurtSpriteRenderer.DOKill();
        HurtSpriteRenderer.DOFade(0, 0);
    }
    
    private IEnumerator Alarm()
    {
        AudioManager.Instance.PlaySound(TypeOfSound.LowHp);

        var blinkCount = AlarmWorkTime;
        while (blinkCount > 0) {
            AlarmLight.enabled = false;
            yield return new WaitForSeconds(BlinkDelay);
            AlarmLight.enabled = true;
            yield return new WaitForSeconds(BlinkDelay);
            blinkCount--;
        }
    }
}
