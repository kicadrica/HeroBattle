using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public int LevelNumber;
    [SerializeField] private TextMeshProUGUI NumberOfLevelText;

    [SerializeField] private Sprite ActiveStar;
    [SerializeField] private Sprite InactiveStar;
    [SerializeField] private Image StarImage;
    
    private void Start()
    {
        NumberOfLevelText.text = LevelNumber.ToString();
        
        var button = GetComponent<Button>();
        button.onClick.AddListener(PassValues);

        var curLevel = LevelManager.CurLevelIndex;
        if (curLevel >= LevelNumber || LevelNumber == 1) {
            button.interactable = true;
            StarImage.sprite = ActiveStar;
        } else {
            button.interactable = false;
            StarImage.sprite = InactiveStar;
            NumberOfLevelText.color = Color.white;
        }
    }
    private void PassValues()
    {
        LevelManager.SetCurLevel(LevelNumber);
        SceneManager.LoadScene("Game");
    }

}
