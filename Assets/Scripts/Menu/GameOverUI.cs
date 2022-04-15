using System.Collections;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup GameOverPanel;
    [SerializeField] private TextMeshProUGUI GameOverText;
    private void Start()
    {
        GameOverPanel.DOFade(0, 0);
        PlayerController.OnPlayerDead += ShowGameOverInfo;
    }

    private void OnDestroy()
    {
        PlayerController.OnPlayerDead -= ShowGameOverInfo;
    }
    private void ShowGameOverInfo()
    {
        StartCoroutine(GameOverInfo());
    }
    
    private IEnumerator GameOverInfo()
    {
        yield return new WaitForSeconds(Global.PlayerDestroyTime);
        
        GameOverPanel.DOFade(1, 0.5f);
        GameOverText.text = "You lose!";

        yield return new WaitForSeconds(2f);
        AudioManager.Instance.PlaySound(TypeOfSound.PressButton);
        SceneManager.LoadScene("MainScene");
        
    }
}
